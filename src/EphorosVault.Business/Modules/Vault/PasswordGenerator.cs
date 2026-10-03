using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
using System;
using System.Text;

namespace EphorosVault.Business.Modules.Vault;

public sealed class PasswordGenerator
{
    private const string Characters = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789!@#$%^&*()-_=+";

    public string Generate(int length)
    {
        if (length < 8 || length > 128) throw new ArgumentOutOfRangeException(nameof(length));

        byte[] random = CryptographyUtility.GetRandomBytes(length);
        StringBuilder password = new(length);
        try
        {
            for (int i = 0; i < random.Length; i++) password.Append(Characters[random[i] % Characters.Length]);
            return password.ToString();
        }
        finally
        {
            Array.Clear(random, 0, random.Length);
        }
    }
}