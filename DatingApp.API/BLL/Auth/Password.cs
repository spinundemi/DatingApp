namespace DatingApp.API.BLL.Auth
{
    public class Password
    {
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public Password(byte[] salt, byte[] hash)
        {
            Salt = salt;
            Hash = hash;
        }
    }
}