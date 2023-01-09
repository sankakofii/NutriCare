using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NutriCare.DTOs;
using NutriCare.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        private readonly IConfiguration _configuration;

        public AuthController(DataContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }


        // POST: api/Auth/register
        [HttpPost("register")]
        public async Task<ActionResult<AccountDTO>> RegisterAccount(RegisterAccountDTO account)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'MSO_devContext.Accounts'  is null.");
            }

            if (AccountsEmailExists(account.Email) || AccountsPhoneNumberExists(account.PhoneNumber))
            {
                return BadRequest("User with this email address or phone number already exists!");
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

            var acc = await _context.Accounts.Where(i => i.AccountId == newAccount.AccountId).Include(a => a.RefreshToken).FirstOrDefaultAsync();

            if (acc != null)
            {
                string token = CreateToken(newAccount);
                var refreshToken = GenerateRefreshToken();

                //save refresh token to DB
                acc.RefreshToken = refreshToken;
                _context.SaveChanges();

                SetRefreshToken(refreshToken);

                AuthAccountDTO authAccountDTO = new()
                {
                    Token = token,
                    RefreshToken = refreshToken.Token,
                    Account = _mapper.Map<AccountDTO>(acc)
                };

                return Ok(authAccountDTO);
            }

            return BadRequest("Something went wrong. Please try again later.");
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAccount(LoginAccountDTO credentials)
        {
            Account? acc = await _context.Accounts
                .Where(a => a.Email == credentials.Email)
                .Include(a => a.Allergies)
                .Include(a => a.Intolerances)
                .Include(a => a.RefreshToken)
                .FirstOrDefaultAsync();

            if (acc != null && VerifyPassword(credentials.Password, acc.Password))
            {
                AccountDTO accountDto = _mapper.Map<AccountDTO>(acc);

                string token = CreateToken(acc);

                var refreshToken = GenerateRefreshToken();

                //save refresh token to DB
                acc.RefreshToken.Token = refreshToken.Token;
                acc.RefreshToken.Expires = refreshToken.Expires;
                acc.RefreshToken.Created = refreshToken.Created;
                _context.SaveChanges();


                SetRefreshToken(refreshToken);

                AuthAccountDTO authAccountDTO = new()
                {
                    Token = token,
                    RefreshToken = refreshToken.Token,
                    Account = accountDto
                };

                return Ok(authAccountDTO);
            }

            return BadRequest("Account with given email address does not exist or the inserted password is incorrect.");
        }

        [HttpPost("refresh_token")]
        public async Task<ActionResult<string>> RefreshToken(int accountId)
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var acc = await _context.Accounts.Where(a => a.AccountId == accountId).Include(a => a.RefreshToken).FirstOrDefaultAsync();

            if (acc == null)
            {
                return NotFound("Account not found.");
            }

            if ( acc.RefreshToken.Token != refreshToken)
            {
                return Unauthorized("Unauthirized.");
            } else if (acc.RefreshToken.Expires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }



            string token = CreateToken(acc);
            var newRefreshToken = GenerateRefreshToken();

            //save refresh token to DB
            acc.RefreshToken = newRefreshToken;
            _context.SaveChanges();

            SetRefreshToken(newRefreshToken);

            return Ok(token);

        }


        private bool AccountsEmailExists(string email)
        {
            return (_context.Accounts?.Any(e => e.Email == email)).GetValueOrDefault();
        }

        private bool AccountsPhoneNumberExists(string phoneNumber)
        {
            return (_context.Accounts?.Any(e => e.PhoneNumber == phoneNumber)).GetValueOrDefault();
        }

        private static RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };

            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
        }

        private string CreateToken(Account account)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, account.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("JWT:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
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
