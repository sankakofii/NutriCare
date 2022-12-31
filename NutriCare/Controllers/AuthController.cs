using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriCare.DTOs;
using NutriCare.Models;
using System.Security.Cryptography;
using System.Text;

namespace NutriCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AuthController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // POST: api/Auth/register
        [HttpPost("register")]
        public async Task<ActionResult<AccountDTO>> RegisterAccount(RegisterAccountDTO account)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'MSO_devContext.Accounts'  is null.");
            }

            if (AccountsEmailExists(account.Email))
            {
                return BadRequest("User with this email address already exists!");
            }

            CreatePasswordHash(account.Password, out byte[] passwordHash);

            Account newAccount = new()
            {
                Email = account.Email,
                FirstName = account.FirstName,
                LastName = account.LastName,
                PhoneNumber = account.PhoneNumber,
                Password = passwordHash
            };

            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();

            var acc = await _context.Accounts.Where(i => i.AccountId == newAccount.AccountId).ProjectTo<AccountDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

            return Ok(acc);
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAccount(LoginAccountDTO credentials)
        {
            Account? acc = await _context.Accounts
                .Where(a => a.Email == credentials.Email)
                .Include(a => a.Allergies)
                .Include(a => a.Intolerances)
                .FirstOrDefaultAsync();

            if (acc != null && VerifyPassword(credentials.Password, acc.Password))
            {
               AccountDTO accountDto = _mapper.Map<AccountDTO>(acc);

                return Ok(accountDto);
            }

            return BadRequest("Account with given email address does not exist or the inserted password is incorrect.");
        }


        private bool AccountsEmailExists(string email)
        {
            return (_context.Accounts?.Any(e => e.Email == email)).GetValueOrDefault();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash)
        {
            using var hmac = new HMACSHA256();
            var passwordSalt = hmac.Key;
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            passwordHash = Combine(hash, passwordSalt);

        }

        private static bool VerifyPassword(string password, byte[] passwordHash)
        {
            byte[] salt = passwordHash.TakeLast(64).ToArray();
            byte[] oldPasswordHash = passwordHash.Take(32).ToArray();

            using var hmac = new HMACSHA256(salt);
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));


                Console.WriteLine(Encoding.Default.GetString(computedHash));

                return computedHash.SequenceEqual(oldPasswordHash);
            }
        }

        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] bytes = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, bytes, 0, first.Length);
            Buffer.BlockCopy(second, 0, bytes, first.Length, second.Length);
            return bytes;
        }
    }
}
