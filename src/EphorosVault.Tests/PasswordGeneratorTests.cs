using EphorosVault.Business.Modules.Vault;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EphorosVault.Tests;

[TestClass]
public class PasswordGeneratorTests
{
    [TestMethod]
    public void GenerateIncludesEveryRequiredCharacterType()
    {
        PasswordGenerator generator = new();
        string password = generator.Generate(20, true, true, true, true);

        Assert.AreEqual(20, password.Length);
        Assert.IsTrue(ContainsAny(password, "ABCDEFGHJKLMNPQRSTUVWXYZ"));
        Assert.IsTrue(ContainsAny(password, "abcdefghijkmnopqrstuvwxyz"));
        Assert.IsTrue(ContainsAny(password, "23456789"));
        Assert.IsTrue(ContainsAny(password, "!@#$%^&*()-_=+"));
    }

    [TestMethod]
    public void GenerateUsesOnlyEnabledCharacterTypes()
    {
        PasswordGenerator generator = new();
        string password = generator.Generate(20, false, false, true, false);

        Assert.AreEqual(20, password.Length);
        Assert.IsTrue(ContainsOnly(password, "23456789"));
    }

    [TestMethod]
    public void GenerateRejectsNoCharacterTypes()
    {
        PasswordGenerator generator = new();
        Assert.ThrowsException<ArgumentException>(() => generator.Generate(20, false, false, false, false));
    }

    [TestMethod]
    public void GenerateRejectsLengthOutsideSupportedRange()
    {
        PasswordGenerator generator = new();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => generator.Generate(7));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => generator.Generate(129));
    }

    private static bool ContainsAny(string value, string characters)
    {
        foreach (char character in value)
        {
            if (characters.IndexOf(character) >= 0) return true;
        }

        return false;
    }

    private static bool ContainsOnly(string value, string characters)
    {
        foreach (char character in value)
        {
            if (characters.IndexOf(character) < 0) return false;
        }

        return true;
    }
}
