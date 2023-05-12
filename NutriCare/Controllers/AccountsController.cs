using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriCare.DTOs;
using NutriCare.Models;

namespace NutriCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AccountsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAccounts()
        {
            return Ok(await _context.Accounts.ProjectTo<AccountDTO>(_mapper.ConfigurationProvider).ToListAsync());
        }

        // GET: api/Accounts/5
        [HttpGet("{id}", Name = "GetAccountById")]
        public async Task<ActionResult<AccountDTO>> GetAccountById(int id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.Where(i => i.AccountId == id).ProjectTo<AccountDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutAccount(EditAccountDTO newAccountInfo)
        {
            var acc = await _context.Accounts.Where(a => a.AccountId == newAccountInfo.AccountId).FirstOrDefaultAsync();

            if (acc == null)
            {
                return BadRequest();
            }

            acc.FirstName = newAccountInfo.FirstName;
            acc.LastName = newAccountInfo.LastName;

            _context.Entry(acc).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.AccountId }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/accounts/set_allergy
        [HttpPost("allergies/set_allergy")]
        public async Task<IActionResult> SetAllergyForAccountByAccountId(AccountAllergyDTO request)
        {
            var account = await _context.Accounts
                .Where(i => i.AccountId == request.AccountId)
                .Include(e => e.Allergies)
                .FirstOrDefaultAsync();
            if (account == null)
            {
                return NotFound();
            }

            var allergen = await _context.Allergies.FindAsync(request.AllergyId);
            if (allergen == null)
            {
                return NotFound();
            }

            account.Allergies.Add(allergen);
            await _context.SaveChangesAsync();

            return Ok(await _context.Accounts.AsNoTracking().Where(i => i.AccountId == account.AccountId).ProjectTo<AccountDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync());
        }

        // DELETE: /api/accounts/allergies/remove_allergy
        [HttpDelete("allergies/remove_allergy")]
        public async Task<IActionResult> RemoveAllergyFromAccountByAccountId(int AllergyId, int AccountId)
        {
            var allergen = await _context.Allergies.FindAsync(AllergyId);
            if (allergen == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.Where(i => i.AccountId == AccountId && i.Allergies.Contains(allergen)).Include(e => e.Allergies).FirstOrDefaultAsync();
            if (account == null)
            {
                return NotFound();
            }

            account.Allergies.Remove(allergen);
            await _context.SaveChangesAsync();

            return Ok(await _context.Accounts.AsNoTracking().Where(i => i.AccountId == account.AccountId).ProjectTo<AccountDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync());
        }

        // POST: api/accounts/set_intolerance
        [HttpPost("intolerances/set_intolerance")]
        public async Task<IActionResult> SetIntoleranceForAccountByAccountId(AccountIntoleranceDTO request)
        {
            var account = await _context.Accounts
                .Where(i => i.AccountId == request.AccountId)
                .Include(e => e.Intolerances)
                .FirstOrDefaultAsync();
            if (account == null)
            {
                return NotFound();
            }

            var intolerance = await _context.Intolerances.FindAsync(request.IntoleranceId);
            if (intolerance == null)
            {
                return NotFound();
            }

            account.Intolerances.Add(intolerance);
            await _context.SaveChangesAsync();

            return Ok(await _context.Accounts.AsNoTracking().Where(i => i.AccountId == account.AccountId).ProjectTo<AccountDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync());
        }

        // DELETE: /api/accounts/intolerances/remove_intolerance
        [HttpDelete("intolerances/remove_intolerance")]
        public async Task<IActionResult> RemoveIntoleranceFromAccountByAccountId(int IntoleranceId, int AccountId)
        {
            var intolerance = await _context.Intolerances.FindAsync(IntoleranceId);
            if (intolerance == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.Where(i => i.AccountId == AccountId && i.Intolerances.Contains(intolerance)).Include(e => e.Intolerances).FirstOrDefaultAsync();
            if (account == null)
            {
                return NotFound();
            }

            account.Intolerances.Remove(intolerance);
            await _context.SaveChangesAsync();

            return Ok(await _context.Accounts.AsNoTracking().Where(i => i.AccountId == account.AccountId).ProjectTo<AccountDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync());
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }
    }
}
