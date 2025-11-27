using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Context;
using ProyectoFinal.Dtos;
using ProyectoFinal.Models;
using System.Linq;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public MedicamentoController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicamentoDto>>> GetMedicamentos()
        {
            var lista = await _context.Medicamentos
                .Include(m => m.Laboratorio)
                .Include(m => m.Activos)
                .ToListAsync();
            var medicamento = _mapper.Map<List<MedicamentoDto>>(lista);
            return Ok(medicamento);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicamentoDto>> GetMedicamento(int id)
        {
            var medicamento = await _context.Medicamentos   
                .Include(m => m.Laboratorio)
                .Include (m => m.Activos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medicamento == null) return NotFound();
            var  medicamentoDto = _mapper.Map<MedicamentoDto>(medicamento);
            return Ok(medicamentoDto);

            }

        [HttpPost] 
        public async Task<ActionResult> PostMedicamento(MedicamentoCreateDto dto)
        {
            var laboratorio =await _context.Laboratorios.FindAsync(dto.LaboratorioId);
            if (laboratorio == null)
                return BadRequest("El laboratorio no existe");

            var medicamento = _mapper.Map<Medicamento>(dto);

            medicamento.Activos = await _context.Activos
                .Where(a => dto.ActivosIds.Contains(a.Id)) 
                .ToListAsync();

            _context.Medicamentos.Add(medicamento);
            await _context.SaveChangesAsync();

            var dtoFinal = _mapper.Map<MedicamentoDto>(medicamento);

            // RETORNAR: 201 Created + Location + objeto creado
            return CreatedAtAction(
                nameof(GetMedicamento),
                new { id = medicamento.Id },
                dtoFinal
            );

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarMedicamento(int id, MedicamentoCreateDto dto)
        {
            var medicamento = await _context.Medicamentos
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medicamento == null)
                return NotFound();

            // Actualizar propiedades simples
            medicamento.NombreComercial = dto.NombreComercial;
            medicamento.Precio = dto.Precio;
            medicamento.LaboratorioId = dto.LaboratorioId;

            // CARGAR los activos nuevos desde la base solo por su ID
            var nuevosActivos = await _context.Activos
                .Where(a => dto.ActivosIds.Contains(a.Id))
                .ToListAsync();

            // Cargar navegación antes de modificarla (sin JOIN gigante)
            await _context.Entry(medicamento)
                .Collection(m => m.Activos)
                .LoadAsync();

            // LIMPIAR relación N-N
            medicamento.Activos.Clear();

            // AGREGAR los nuevos activos
            foreach (var activo in nuevosActivos)
                medicamento.Activos.Add(activo);

            await _context.SaveChangesAsync();

            var dtoFinal = _mapper.Map<MedicamentoDto>(medicamento);

            return Ok(dtoFinal);
        }




    }

}
