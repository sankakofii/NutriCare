using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriCare.Models;

namespace NutriCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IntoleranceIngredientsController : ControllerBase
    {
        private readonly DataContext _context;

        public IntoleranceIngredientsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/IntoleranceIngredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IntoleranceIngredient>>> GetIntoleranceIngredient()
        {
            return await _context.IntoleranceIngredient.ToListAsync();
        }

        // GET: api/IntoleranceIngredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IntoleranceIngredient>> GetIntoleranceIngredient(int id)
        {
            var intoleranceIngredient = await _context.IntoleranceIngredient.FindAsync(id);

            if (intoleranceIngredient == null)
            {
                return NotFound();
            }

            return intoleranceIngredient;
        }

        // PUT: api/IntoleranceIngredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIntoleranceIngredient(int id, IntoleranceIngredient intoleranceIngredient)
        {
            if (id != intoleranceIngredient.IntoleranceIngredientId)
            {
                return BadRequest();
            }

            _context.Entry(intoleranceIngredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IntoleranceIngredientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/IntoleranceIngredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IntoleranceIngredient>> PostIntoleranceIngredient(IntoleranceIngredient intoleranceIngredient)
        {
            _context.IntoleranceIngredient.Add(intoleranceIngredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIntoleranceIngredient", new { id = intoleranceIngredient.IntoleranceIngredientId }, intoleranceIngredient);
        }

        // DELETE: api/IntoleranceIngredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIntoleranceIngredient(int id)
        {
            var intoleranceIngredient = await _context.IntoleranceIngredient.FindAsync(id);
            if (intoleranceIngredient == null)
            {
                return NotFound();
            }

            _context.IntoleranceIngredient.Remove(intoleranceIngredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IntoleranceIngredientExists(int id)
        {
            return _context.IntoleranceIngredient.Any(e => e.IntoleranceIngredientId == id);
        }
    }
}
