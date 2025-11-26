using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Context;
using ProyectoFinal.Dtos;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratorioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public LaboratorioController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LaboratorioDto>>> GetLaboratorios()
        {
            var lista = await _context.Laboratorios.ToListAsync();
            var laboratorios = _mapper.Map<IEnumerable<LaboratorioDto>>(lista);
            return Ok(laboratorios);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LaboratorioDto>> GetLaboratorio(int id)
        {
            var laboratorio1 = await _context.Laboratorios.FindAsync(id);
            if (laboratorio1 == null)
            {
                return NotFound();
            }
            var laboratorio =_mapper.Map<LaboratorioDto>(laboratorio1);
            return Ok(laboratorio);
        }

        [HttpPost]
        public async Task<ActionResult<LaboratorioDto>> PostLaboratiorio(LaboratorioCreateDto laboratorioCreacion)
        {
            var laboratorio = _mapper.Map<Laboratorio>(laboratorioCreacion);        
            _context.Laboratorios.Add(laboratorio);
            await _context.SaveChangesAsync();
            var laboratorioDto = _mapper.Map<LaboratorioDto>(laboratorio);
            return CreatedAtAction(
                nameof(GetLaboratorio),           
                new { id = laboratorio.Id },           
                laboratorioDto                         
            );

        }
    }

    
}
