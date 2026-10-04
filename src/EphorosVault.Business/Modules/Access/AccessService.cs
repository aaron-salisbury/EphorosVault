using DotNetFrameworkToolkit.Modules.UserAccess;
using Microsoft.Practices.Unity.Utility;
using System;

namespace EphorosVault.Business.Modules.Access;

public sealed class AccessService
{
    private readonly IUserAuthenticator _authenticator;
    private readonly IUserCredentialRepository _repository;

    public AccessService(IUserAuthenticator authenticator, IUserCredentialRepository repository)
    {
        Guard.ArgumentNotNull(authenticator, nameof(authenticator));
        Guard.ArgumentNotNull(repository, nameof(repository));

        _authenticator = authenticator;
        _repository = repository;
    }

    public bool RequiresSetup => !_repository.HasUser();

    public void CreateMasterPassword(string password)
    {
        if (!RequiresSetup)
        {
            throw new InvalidOperationException("A master password has already been created.");
        }

        if (password == null || password.Length < 8)
        {
            throw new ArgumentException("The master password must be at least eight characters.", nameof(password));
        }

        _repository.Save(_authenticator.CreateUserCredentials(password));
    }

    public bool Authenticate(string password)
    {
        if (RequiresSetup)
        {
            return false;
        }

        return _authenticator.VerifyCredentials(_repository.Get(), password);
    }
}
