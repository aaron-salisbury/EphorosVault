using EphorosVault.Business.Modules.Vault;
using Microsoft.Practices.Unity.Utility;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EphorosVault.Integrations.Cryptography;

public sealed class VaultRecoveryService : IVaultRecoveryService
{
    private const byte CurrentVersion = 1;
    private const string Magic = "EPHOROSVAULT-RECOVERY";
    private const string VerificationText = "Ephoros Vault recovery verification";
    private readonly IVaultKeyStore _keyStore;
    private readonly IVaultRecoveryMetadataRepository _metadataRepository;

    public VaultRecoveryService(IVaultKeyStore keyStore, IVaultRecoveryMetadataRepository metadataRepository)
    {
        Guard.ArgumentNotNull(keyStore, nameof(keyStore));
        Guard.ArgumentNotNull(metadataRepository, nameof(metadataRepository));

        _keyStore = keyStore;
        _metadataRepository = metadataRepository;
    }

    public void EnsureInitialized()
    {
        VaultRecoveryMetadata metadata = _metadataRepository.Get();
        if (metadata != null)
        {
            return;
        }

        byte[] key = _keyStore.Load();
        try
        {
            _metadataRepository.Save(new VaultRecoveryMetadata
            {
                VaultId = Guid.NewGuid(),
                VerificationValue = CreateVerificationValue(key)
            });
        }
        finally
        {
            Array.Clear(key, 0, key.Length);
        }
    }

    public void Export(string filePath)
    {
        Guard.ArgumentNotNull(filePath, nameof(filePath));
        EnsureInitialized();

        VaultRecoveryMetadata metadata = _metadataRepository.Get();
        byte[] key = _keyStore.Load();
        try
        {
            using FileStream stream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            using BinaryWriter writer = new(stream, Encoding.UTF8);
            writer.Write(Magic);
            writer.Write(CurrentVersion);
            writer.Write(metadata.VaultId.ToByteArray());
            writer.Write(key.Length);
            writer.Write(key);
            writer.Write(ComputeChecksum(metadata.VaultId, key));
        }
        finally
        {
            Array.Clear(key, 0, key.Length);
        }
    }

    public void Import(string filePath)
    {
        Guard.ArgumentNotNull(filePath, nameof(filePath));
        EnsureInitialized();

        VaultRecoveryMetadata metadata = _metadataRepository.Get();
        byte[] candidate = ReadRecoveryKey(filePath, metadata.VaultId);
        try
        {
            string verification = CreateVerificationValue(candidate);
            if (!FixedTimeEquals(verification, metadata.VerificationValue))
            {
                throw new InvalidDataException("The recovery key does not belong to this Ephoros Vault.");
            }

            _keyStore.Save(candidate);
        }
        finally
        {
            Array.Clear(candidate, 0, candidate.Length);
        }
    }

    private static byte[] ReadRecoveryKey(string filePath, Guid expectedVaultId)
    {
        byte[] file = File.ReadAllBytes(filePath);
        if (file.Length == 32)
        {
            return file;
        }

        try
        {
            using MemoryStream stream = new(file, false);
            using BinaryReader reader = new(stream, Encoding.UTF8);
            if (reader.ReadString() != Magic || reader.ReadByte() != CurrentVersion)
            {
                throw new InvalidDataException("The file is not a supported Ephoros Vault recovery key.");
            }

            Guid vaultId = new(reader.ReadBytes(16));
            if (vaultId != expectedVaultId)
            {
                throw new InvalidDataException("This recovery key belongs to a different Ephoros Vault.");
            }

            int keyLength = reader.ReadInt32();
            if (keyLength != 32)
            {
                throw new InvalidDataException("The recovery key contains an invalid key.");
            }

            byte[] key = reader.ReadBytes(keyLength);
            byte[] checksum = reader.ReadBytes(32);
            if (key.Length != keyLength || checksum.Length != 32 || stream.Position != stream.Length)
            {
                Array.Clear(key, 0, key.Length);
                throw new InvalidDataException("The recovery key file is incomplete or corrupt.");
            }

            byte[] expected = ComputeChecksum(vaultId, key);
            try
            {
                if (!FixedTimeEquals(checksum, expected))
                {
                    Array.Clear(key, 0, key.Length);
                    throw new InvalidDataException("The recovery key file failed its integrity check.");
                }
            }
            finally
            {
                Array.Clear(checksum, 0, checksum.Length);
                Array.Clear(expected, 0, expected.Length);
            }

            return key;
        }
        catch (EndOfStreamException exception)
        {
            throw new InvalidDataException("The recovery key file is incomplete or corrupt.", exception);
        }
        finally
        {
            Array.Clear(file, 0, file.Length);
        }
    }

    private static string CreateVerificationValue(byte[] key)
    {
        using HMACSHA256 hmac = new(key);
        return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(VerificationText)));
    }

    private static byte[] ComputeChecksum(Guid vaultId, byte[] key)
    {
        using SHA256Managed hash = new();
        byte[] magic = Encoding.UTF8.GetBytes(Magic);
        byte[] id = vaultId.ToByteArray();
        byte[] data = new byte[magic.Length + 1 + id.Length + key.Length];
        Buffer.BlockCopy(magic, 0, data, 0, magic.Length);
        data[magic.Length] = CurrentVersion;
        Buffer.BlockCopy(id, 0, data, magic.Length + 1, id.Length);
        Buffer.BlockCopy(key, 0, data, magic.Length + 1 + id.Length, key.Length);
        try
        {
            return hash.ComputeHash(data);
        }
        finally
        {
            Array.Clear(data, 0, data.Length);
        }
    }

    private static bool FixedTimeEquals(string left, string right)
    {
        byte[] a = Encoding.UTF8.GetBytes(left ?? string.Empty);
        byte[] b = Encoding.UTF8.GetBytes(right ?? string.Empty);
        try
        {
            return FixedTimeEquals(a, b);
        }
        finally
        {
            Array.Clear(a, 0, a.Length);
            Array.Clear(b, 0, b.Length);
        }
    }

    private static bool FixedTimeEquals(byte[] left, byte[] right)
    {
        if (left.Length != right.Length)
        {
            return false;
        }

        int difference = 0;
        for (int i = 0; i < left.Length; i++)
        {
            difference |= left[i] ^ right[i];
        }

        return difference == 0;
    }
}
