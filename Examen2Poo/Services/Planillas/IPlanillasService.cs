using System.Collections.Generic;
using System.Threading.Tasks;
using Examen2Poo.Dtos.Common;
using Examen2Poo.Dtos.Planillas;

namespace Examen2Poo    .Services.Planillas
{
    public interface IPlanillaService
    {
        Task<ResponseDto<List<PlanillaDto>>> GetAllAsync();
        Task<ResponseDto<PlanillaDto>> GetOneByIdAsync(int id);
        Task<ResponseDto<PlanillaDto>> CreateAsync(PlanillaCreateDto dto);
        Task<ResponseDto<PlanillaDto>> EditAsync(int id, PlanillaCreateDto dto);
        Task<ResponseDto<PlanillaDto>> DeleteAsync(int id);
        Task<ResponseDto<List<PlanillaDto>>> GetByPeriodoAsync(string periodo);
        Task<ResponseDto<PlanillaDto>> UpdateEstadoAsync(int id, PlanillaEstadoDto dto);
        Task<ResponseDto<PlanillaDto>> GenerarPlanillaAsync(string periodo);
    }
}