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
    public class ActivoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ActivoController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivoDto>>> GetActivos()
        {
            var lista = await _context.Activos.ToListAsync();
            var activos = _mapper.Map<IEnumerable<ActivoDto>>(lista);
            return Ok(activos);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivoDto>> GetActivo(int id)
        {
            var activos1 = await _context.Activos.FindAsync(id);
            if (activos1 == null)
            {
                return NotFound();
            }
            var activo = _mapper.Map<ActivoDto>(activos1);
            return Ok(activo);
        }

        [HttpPost]
        public async Task<ActionResult<ActivoDto>> PostActivo(ActivosCreateDto activoCreacion)
        {
            var activo = _mapper.Map<Activo>(activoCreacion);
            _context.Activos.Add(activo);
            await _context.SaveChangesAsync();
            var activoDto = _mapper.Map<ActivoDto>(activo);
            return CreatedAtAction(
                nameof(GetActivo),
                new { id = activo.Id },
                activoDto
            );

        }
    }


}
