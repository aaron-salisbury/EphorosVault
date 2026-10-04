using Microsoft.Practices.Unity.Utility;
using System;
using System.IO;
using System.Security.Cryptography;

namespace EphorosVault.Integrations.Cryptography;

public sealed class DpapiVaultKeyStore : IVaultKeyStore
{
    private static readonly byte[] Entropy = { 0x45, 0x70, 0x68, 0x6F, 0x72, 0x6F, 0x73, 0x56, 0x61, 0x75, 0x6C, 0x74 };
    private readonly string _keyFilePath;

    public DpapiVaultKeyStore(string keyFilePath)
    {
        Guard.ArgumentNotNull(keyFilePath, nameof(keyFilePath));

        _keyFilePath = keyFilePath;
    }

    public bool Exists => File.Exists(_keyFilePath);

    public byte[] Load()
    {
        if (!Exists)
        {
            throw new FileNotFoundException("The vault key file could not be found.", _keyFilePath);
        }

        return ProtectedData.Unprotect(File.ReadAllBytes(_keyFilePath), Entropy, DataProtectionScope.CurrentUser);
    }

    public void Save(byte[] key)
    {
        Guard.ArgumentNotNull(key, nameof(key));

        if (key.Length != 32)
        {
            throw new ArgumentException("The vault key must be 32 bytes.", nameof(key));
        }

        File.WriteAllBytes(_keyFilePath, ProtectedData.Protect(key, Entropy, DataProtectionScope.CurrentUser));
    }

    public void EnsureCreated()
    {
        if (Exists)
        {
            return;
        }

        byte[] key = new byte[32];
        RNGCryptoServiceProvider random = new();
        random.GetBytes(key);
        try
        {
            Save(key);
        }
        finally { Array.Clear(key, 0, key.Length); }
    }
}
