using Microsoft.EntityFrameworkCore;
using NutriCare.Models;
using System.Security.Claims;

namespace NutriCare.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;

        public AccountService(IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public bool VerifyRequest(int accountId)
        {

            var accountById = _context.Accounts.Where(a => a.AccountId == accountId).FirstOrDefault();
            var email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (accountById.Email == email)
            {
                return true;
            }

            return false;
        }
    }
}
