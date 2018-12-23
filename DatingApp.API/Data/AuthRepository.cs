using System;
using System.Threading.Tasks;
using DatingApp.API.BLL.Auth;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IAuthCrypto _authCrypto;
        public AuthRepository(DataContext context, IAuthCrypto authCrypto)
        {
            _authCrypto = authCrypto;
            _context = context;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null) 
                return null;
            if (!_authCrypto.IsPassWordMatch(new Password(user.PasswordSalt, user.PasswordHash), password))
                return null;
            return user;
        }

        public async Task<User> Resister(User user, string password)
        {
            var password_ = _authCrypto.Encrypt(password);
            user.PasswordSalt = password_.Salt;
            user.PasswordHash = password_.Hash;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(x => x.Username == username))
                return true;
            return false;
        }
    }
}