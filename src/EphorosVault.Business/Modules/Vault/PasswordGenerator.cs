using System;
using System.Security.Cryptography;
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
        if (requireUppercase)
        {
            characters.Append(UppercaseCharacters);
            requiredCount++;
        }
        if (requireLowercase)
        {
            characters.Append(LowercaseCharacters);
            requiredCount++;
        }
        if (requireNumbers)
        {
            characters.Append(NumberCharacters);
            requiredCount++;
        }
        if (requireSpecialCharacters)
        {
            characters.Append(SpecialCharacters);
            requiredCount++;
        }

        if (requiredCount == 0)
        {
            throw new ArgumentException("At least one character type must be enabled.");
        }

        if (length < requiredCount)
        {
            throw new ArgumentException("The password length is too short for the required character types.");
        }

        char[] password = new char[length];
        RNGCryptoServiceProvider random = new();
        int index = 0;
        if (requireUppercase)
        {
            password[index++] = Pick(UppercaseCharacters, random);
        }

        if (requireLowercase)
        {
            password[index++] = Pick(LowercaseCharacters, random);
        }

        if (requireNumbers)
        {
            password[index++] = Pick(NumberCharacters, random);
        }

        if (requireSpecialCharacters)
        {
            password[index++] = Pick(SpecialCharacters, random);
        }

        string pool = characters.ToString();
        while (index < password.Length)
        {
            password[index++] = Pick(pool, random);
        }

        for (int i = password.Length - 1; i > 0; i--)
        {
            int swapIndex = NextIndex(random, i + 1);
            char temporary = password[i];
            password[i] = password[swapIndex];
            password[swapIndex] = temporary;
        }

        string result = new(password);
        Array.Clear(password, 0, password.Length);
        return result;
    }

    private static char Pick(string characters, RNGCryptoServiceProvider random)
    {
        return characters[NextIndex(random, characters.Length)];
    }

    private static int NextIndex(RNGCryptoServiceProvider random, int exclusiveMaximum)
    {
        byte[] value = new byte[1];
        int upperBound = 256 - (256 % exclusiveMaximum);
        do
        {
            random.GetBytes(value);
        }
        while (value[0] >= upperBound);

        return value[0] % exclusiveMaximum;
    }
}
