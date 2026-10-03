using DotNetFrameworkToolkit.Modules.UserAccess;
using System;

namespace EphorosVault.Business.Modules.Access;

public sealed class AccessService
{
    private readonly IUserAuthenticator _authenticator;
    private readonly IUserCredentialRepository _repository;

    public AccessService(IUserAuthenticator authenticator, IUserCredentialRepository repository)
    {
        _authenticator = authenticator ?? throw new ArgumentNullException(nameof(authenticator));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public bool RequiresSetup => !_repository.HasUser();

    public void CreateMasterPassword(string password)
    {
        if (!RequiresSetup) throw new InvalidOperationException("A master password has already been created.");
        if (password == null || password.Length < 8) throw new ArgumentException("The master password must be at least eight characters.", nameof(password));
        _repository.Save(_authenticator.CreateUserCredentials(password));
    }

    public bool Authenticate(string password)
    {
        if (RequiresSetup) return false;
        return _authenticator.VerifyCredentials(_repository.Get(), password);
    }
}