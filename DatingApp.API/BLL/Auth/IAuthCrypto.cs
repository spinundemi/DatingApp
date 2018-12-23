namespace DatingApp.API.BLL.Auth
{
    public interface IAuthCrypto
    {
         bool IsPassWordMatch(Password password, string passwordString);
         Password Encrypt(string password);
    }
}