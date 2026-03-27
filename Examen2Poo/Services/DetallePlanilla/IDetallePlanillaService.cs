using System.Collections.Generic;
using System.Threading.Tasks;
using Examen2Poo.Dtos.Common;
using Examen2Poo.Dtos.DetallesPlanilla;

namespace Examen2Poo.Services.DetallesPlanilla
{
    public interface IDetallePlanillaService
    {
        Task<ResponseDto<List<DetallePlanillaDto>>> GetByPlanillaIdAsync(int planillaId);
        Task<ResponseDto<DetallePlanillaDto>> GetOneByIdAsync(int id);
        Task<ResponseDto<DetallePlanillaDto>> CreateAsync(DetallePlanillaCreateDto dto);
        Task<ResponseDto<DetallePlanillaDto>> EditAsync(int id, DetallePlanillaCreateDto dto);
        Task<ResponseDto<DetallePlanillaDto>> DeleteAsync(int id);
        Task<ResponseDto<List<DetallePlanillaDto>>> GetByEmpleadoIdAsync(int empleadoId);
    }
}