namespace DatingApp.API.BLL.Auth
{
    public class HMACSHA512Algorithm : IAuthCrypto
    {
        
        public Password Encrypt(string password)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                return new Password(
                    hmac.Key,
                    hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
                );
            }
        }

        public bool IsPassWordMatch(Password password, string passwordString)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(password.Salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordString));
                for (int i=0; i< computedHash.Length; i++)
                {
                    if (computedHash[i] != password.Hash[i]) return false;
                }
            }
            return true;
        }
    }
}