using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriCare.DTOs;
using NutriCare.Models;

namespace NutriCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntolerancesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public IntolerancesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Intolerances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IntoleranceDTO>>> GetIntolerances()
        {
            return await _context.Intolerances.ProjectTo<IntoleranceDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
