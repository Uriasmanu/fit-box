using fit_box.Data;
using fit_box.DTOs;
using fit_box.Models;
using Microsoft.EntityFrameworkCore;

namespace fit_box.Service
{
    public class LoginService
    {
        private readonly AppDbContext _context;

        public LoginService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Logins.AnyAsync(l => l.Username == username);
        }

        public async Task<Login> CreateUserAsync(Login login)
        {
            login.Password = SenhaHasher.HashSenha(login.Password);
            _context.Logins.Add(login);
            await _context.SaveChangesAsync();
            return login;
        }

        public async Task<Login?> AuthenticateUserAsync(string username, string password)
        {
            var user = await _context.Logins.FirstOrDefaultAsync(l => l.Username == username);

            if (user != null && SenhaHasher.VerifyPassword(user.Password, password))
            {
                return user;
            }

            return null;
        }

        public async Task<LoginDTO?> GetUserByIdAsync(Guid id)
        {
            var login = await _context.Logins.FindAsync(id);

            if (login == null) return null;

            return new LoginDTO
            {
                Id = login.Id,
                Username = login.Username
            };
        }

        public async Task<IEnumerable<LoginDTO>> GetAllUsersAsync()
        {
            return await _context.Logins
                .Select(login => new LoginDTO
                {
                    Id = login.Id,
                    Username = login.Username
                })
                .ToListAsync();
        }
    }
}
