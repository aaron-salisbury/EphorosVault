using DotNetFrameworkToolkit.Modules.UserAccess;

namespace EphorosVault.Business.Modules.Access;

public interface IUserCredentialRepository
{
    bool HasUser();
    CryptographyCredential Get();
    void Save(CryptographyCredential credential);
}
