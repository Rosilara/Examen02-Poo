using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Examen2Poo.Dtos.DetallePlanillas;
using Examen2Poo.Services.DetallePlanilla;

namespace Examen2Poo.Controllers
{
    [Route("api/detallesplanilla")]
    [ApiController]
    public class DetallePlanillaController : ControllerBase
    {
        private readonly IDetallePlanillaService _detalleService;

        public DetallePlanillaController(IDetallePlanillaService detalleService)
        {
            _detalleService = detalleService;
        }

        [HttpGet("planilla/{planillaId}")]
        public async Task<ActionResult> GetByPlanilla(int planillaId)
        {
            var result = await _detalleService.GetByPlanillaIdAsync(planillaId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(int id)
        {
            var result = await _detalleService.GetOneByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(DetallePlanillaCreateDto dto)
        {
            var result = await _detalleService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, DetallePlanillaCreateDto dto)
        {
            var result = await _detalleService.EditAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _detalleService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("empleado/{empleadoId}")]
        public async Task<ActionResult> GetByEmpleado(int empleadoId)
        {
            var result = await _detalleService.GetByEmpleadoIdAsync(empleadoId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
