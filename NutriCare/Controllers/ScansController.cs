using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text.Json.Serialization.Metadata;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NutriCare.DTOs;
using NutriCare.Models;

namespace NutriCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ScansController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ScansController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Scans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Scan>>> GetScanHistories()
        {
            return await _context.Scans.ToListAsync();
        }

        //GET: api/scan_histories_of_account
        [HttpGet("scan_histories_of_account")]
        public async Task<ActionResult<IEnumerable<ScanDTO>>> GetScanHistoriesByAccountId(int account_id)
        {
            return await _context.Scans
                .AsNoTracking()
                .Where(i => i.AccountId == account_id)
                .Include(i => i.Product)
                .ProjectTo<ScanDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        // GET: api/ScanHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Scan>> GetScanHistory(int id)
        {
            var scanHistory = await _context.Scans.FindAsync(id);

            if (scanHistory == null)
            {
                return NotFound();
            }

            return scanHistory;
        }

        //GET: api/scans/get_product_info
        [HttpGet("get_product_info")]
        public async Task<ActionResult<ResponseDTO>> GetProductInfoByScannedBarcode(string barcode, int accountId)
        {
            ResponseDTO res = new();
            bool inDB = true;
            if (_context.Products.Any(a => a.Barcode == barcode) == false)
            {
                inDB = false;
                try
                {
                    using HttpClient client = new();
                    var response = await client.GetFromJsonAsync<ResponseDTO>($"https://world.openfoodfacts.org/api/v0/product/" + barcode + ".json");
                    if (response == null)
                    {
                        return NotFound();
                    }
                    else if (response.status == 0)
                    {
                        return response;
                    }
                    res = response;
                }
                catch (HttpRequestException e)
                {
                    throw e;
                }

                //Product newProduct = new()
                //{
                //    Barcode = res.code,
                //    Allergens = res.product.allergens,
                //    AllergensFromIngredients = res.product.allergens_from_ingredients,
                //    ProductName = res.product.product_name,
                //    ImageFrontUrl = res.product.image_front_url,
                //    ImageNutritionUrl = res.product.image_nutrition_url,
                //    IngredientsText = res.product.ingredients_text
                //};

                Product newProduct = _mapper.Map<Product>(res);

                _context.Products.Add(newProduct);
                _context.SaveChanges();
            }

            var acc = await _context.Accounts.Where(i => i.AccountId == accountId).Include(i => i.Allergies).FirstOrDefaultAsync();

            if (acc == null)
            {
                return NotFound("Account could not be found.");
            }

            var product = await _context.Products.Where(a => a.Barcode == barcode).FirstAsync();

            Scan newScan = new()
            {
                ScanTime = DateTime.Now,
                Account = acc,
                AccountId = acc.AccountId,
                Product = product,
                ProductId = product.ProductId
            };

            _context.Scans.Add(newScan);
            await _context.SaveChangesAsync();

            if (inDB == true)
            {   
                ResponseDTO result = new()
                {   
                    code = product.Barcode,
                    product = _mapper.Map<ProductDTO>(product),
                    harm = string.Join(" ", CheckIfHarmful(accountId, product.Allergens, product.AllergensFromIngredients, product.IngredientsText).Split(' ').Distinct())
                };
                return result;
            } else
            {
                res.harm = string.Join(" ", CheckIfHarmful(accountId, product.Allergens, product.AllergensFromIngredients, product.IngredientsText).Split(' ').Distinct());
                return res;
            }
        }

        // POST: api/ScanHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Scan>> PostScan(CreateScanHistoryDTO scanHistory)
        {
            var scan = _mapper.Map<Scan>(scanHistory);
            _context.Scans.Add(scan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScanHistory", new { id = scan.ScanId }, scan);
        }

        // DELETE: api/ScanHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScan(int id)
        {
            var scanHistory = await _context.Scans.FindAsync(id);
            if (scanHistory == null)
            {
                return NotFound();
            }

            _context.Scans.Remove(scanHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string CheckIfHarmful(int accountId, string allergen1, string allergen2, string ingredients)
        {
            Regex reg = new Regex("[*'\",_&#^@]");
            allergen1 = reg.Replace(allergen1, " ");
            allergen2 = reg.Replace(allergen2, " ");
            ingredients = reg.Replace(ingredients, " ");
            string harm = string.Empty;
            var x = _context.Accounts.Where(i => i.AccountId == accountId).Include(i => i.Allergies).Include(i => i.Intolerances).ThenInclude(i => i.IntoleranceIngredients).FirstOrDefault();
            //var accountAllergyList = _context.Accounts.Where(i => i.AccountId == accountId).Select(i => i.Allergies);
            if (x == null)
            {
                return harm;
            }

            if (x.Allergies != null && x.Allergies.Any())
            {
                foreach (var a in x.Allergies)
                {
                    if (allergen1.Contains(a.Type.ToLower()) || allergen2.Contains(a.Type.ToLower()))
                    {
                        harm += a.Type + " ";
                    }
                }
            }
            //var accountIntoleranceList = _context.Accounts.Where(i => i.AccountId == accountId).Select(i => i.Intolerances);

            if (x.Intolerances != null && x.Intolerances.Any())
            {
                foreach (var a in x.Intolerances)
                {
                    foreach (var b in a.IntoleranceIngredients)
                    {
                        if (b != null && ingredients.Contains(b.IntoleranceIngredientName.ToLower()))
                        {
                            harm += b.IntoleranceIngredientName + " ";
                        }
                    }
                }
            }
 
            return harm;
        }
        private bool ScanExists(int id)
        {
            return _context.Scans.Any(e => e.ScanId == id);
        }
    }
}
