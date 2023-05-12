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
    public class AllergiesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AllergiesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //GET: get allergies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllergyDTO>>> GetAllergies()
        {
            return await _context.Allergies.ProjectTo<AllergyDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
