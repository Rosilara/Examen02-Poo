using Microsoft.AspNetCore.Mvc;
using Examen2Poo.Dtos.Planillas;
namespace Examen2Poo.Controllers
{
    [Route("api/planillas")]
    [ApiController]
    public class PlanillaController : ControllerBase
    {
        private readonly IPlanillaService _planillaService;

        public PlanillaController(IPlanillaService planillaService)
        {
            _planillaService = planillaService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _planillaService.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(int id)
        {
            var result = await _planillaService.GetOneByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(PlanillaCreateDto dto)
        {
            var result = await _planillaService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, PlanillaCreateDto dto)
        {
            var result = await _planillaService.EditAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _planillaService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("periodo/{periodo}")]
        public async Task<ActionResult> GetByPeriodo(string periodo)
        {
            var result = await _planillaService.GetByPeriodoAsync(periodo);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}/estado")]
        public async Task<ActionResult> UpdateEstado(int id, PlanillaEstadoDto dto)
        {
            var result = await _planillaService.UpdateEstadoAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("generar")]
        public async Task<ActionResult> Generar([FromQuery] string periodo)
        {
            var result = await _planillaService.GenerarPlanillaAsync(periodo);
            return StatusCode(result.StatusCode, result);
        }
    }
}