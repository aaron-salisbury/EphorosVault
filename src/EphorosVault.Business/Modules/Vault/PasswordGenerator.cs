using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
using System;
using System.Text;

namespace EphorosVault.Business.Modules.Vault;

public sealed class PasswordGenerator
{
    private const string LowercaseCharacters = "abcdefghijkmnopqrstuvwxyz";
    private const string NumberCharacters = "23456789";
    private const string SpecialCharacters = "!@#$%^&*()-_=+";
    private const string UppercaseCharacters = "ABCDEFGHJKLMNPQRSTUVWXYZ";

    public string Generate(int length)
    {
        return Generate(length, true, true, true, true);
    }

    public string Generate(int length, bool requireUppercase, bool requireLowercase, bool requireNumbers, bool requireSpecialCharacters)
    {
        if (length < 8 || length > 128)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        StringBuilder characters = new();
        int requiredCount = 0;
        if (requireUppercase) { characters.Append(UppercaseCharacters); requiredCount++; }
        if (requireLowercase) { characters.Append(LowercaseCharacters); requiredCount++; }
        if (requireNumbers) { characters.Append(NumberCharacters); requiredCount++; }
        if (requireSpecialCharacters) { characters.Append(SpecialCharacters); requiredCount++; }

        if (requiredCount == 0)
        {
            throw new ArgumentException("At least one character type must be enabled.");
        }

        if (length < requiredCount)
        {
            throw new ArgumentException("The password length is too short for the required character types.");
        }

        char[] password = new char[length];
        byte[] random = CryptographyUtility.GetRandomBytes(length * 2);
        try
        {
            int index = 0;
            int randomIndex = 0;
            if (requireUppercase) password[index++] = Pick(UppercaseCharacters, random[randomIndex++]);
            if (requireLowercase) password[index++] = Pick(LowercaseCharacters, random[randomIndex++]);
            if (requireNumbers) password[index++] = Pick(NumberCharacters, random[randomIndex++]);
            if (requireSpecialCharacters) password[index++] = Pick(SpecialCharacters, random[randomIndex++]);

            string pool = characters.ToString();
            while (index < password.Length)
            {
                password[index++] = Pick(pool, random[randomIndex++]);
            }

            for (int i = password.Length - 1; i > 0; i--)
            {
                int swapIndex = random[randomIndex++] % (i + 1);
                char temporary = password[i];
                password[i] = password[swapIndex];
                password[swapIndex] = temporary;
            }

            return new string(password);
        }
        finally
        {
            Array.Clear(random, 0, random.Length);
            Array.Clear(password, 0, password.Length);
        }
    }

    private static char Pick(string characters, byte value)
    {
        return characters[value % characters.Length];
    }
}
