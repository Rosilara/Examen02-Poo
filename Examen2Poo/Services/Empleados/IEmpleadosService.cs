using System.Collections.Generic;
using System.Threading.Tasks;
using Examen2Poo.Dtos.Common;
using Examen2Poo.Dtos.Empleados;

namespace Examen2Poo.Services.Empleados
{
    public interface IEmpleadoService
    {
        Task<ResponseDto<List<EmpleadoDto>>> GetAllAsync();
        Task<ResponseDto<EmpleadoDto>> GetOneByIdAsync(int id);
        Task<ResponseDto<EmpleadoDto>> CreateAsync(EmpleadoCreateDto dto);
        Task<ResponseDto<EmpleadoDto>> EditAsync(int id, EmpleadoCreateDto dto);
        Task<ResponseDto<EmpleadoDto>> DeleteAsync(int id);
        Task<ResponseDto<List<EmpleadoDto>>> GetActivosAsync();
    }
}