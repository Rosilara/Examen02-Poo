
using System.Collections.Generic;
using System.Linq;  
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Examen2Poo.Constants;
using Examen2Poo.Database;
using Examen2Poo.Dtos.Common;
using Examen2Poo.Dtos.Empleados;
using Examen2Poo.Entities;

namespace Examen2Poo.Services.Empleados
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly PlanillaDbContext _context;

        public EmpleadoService(PlanillaDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<List<EmpleadoDto>>> GetAllAsync()
        {
            var empleados = await _context.Empleados.ToListAsync();
            return new ResponseDto<List<EmpleadoDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = empleados.Select(e => MapToDto(e)).ToList()
            };
        }

        public async Task<ResponseDto<EmpleadoDto>> GetOneByIdAsync(int id)
        {
            var empleado = await _context.Empleados.FirstOrDefaultAsync(e => e.Id == id);
            if (empleado is null)
                return new ResponseDto<EmpleadoDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };

            return new ResponseDto<EmpleadoDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Data = MapToDto(empleado)
            };
        }

        public async Task<ResponseDto<EmpleadoDto>> CreateAsync(EmpleadoCreateDto dto)
        {
            // Validar documento unico
            var existing = await _context.Empleados
                .FirstOrDefaultAsync(e => e.Documento == dto.Documento);
            if (existing is not null)
                return new ResponseDto<EmpleadoDto>
                {
                    StatusCode = HttpStatusCode.CONFLICT,
                    Status = false,
                    Message = HttpMessageResponse.DOCUMENT_EXISTS
                };

            var empleado = new EmpleadoEntity
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Documento = dto.Documento,
                FechaContratacion = dto.FechaContratacion,
                Departamento = dto.Departamento,
                PuestoTrabajo = dto.PuestoTrabajo,
                SalarioBase = dto.SalarioBase,
                Activo = true
            };

            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            return new ResponseDto<EmpleadoDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_CREATED,
                Data = MapToDto(empleado)
            };
        }

        public async Task<ResponseDto<EmpleadoDto>> EditAsync(int id, EmpleadoCreateDto dto)
        {
            var empleado = await _context.Empleados.FirstOrDefaultAsync(e => e.Id == id);
            if (empleado is null)
                return new ResponseDto<EmpleadoDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };

            // Validar documento unico (que no sea el mismo empleado)
            var existing = await _context.Empleados
                .FirstOrDefaultAsync(e => e.Documento == dto.Documento && e.Id != id);
            if (existing is not null)
                return new ResponseDto<EmpleadoDto>
                {
                    StatusCode = HttpStatusCode.CONFLICT,
                    Status = false,
                    Message = HttpMessageResponse.DOCUMENT_EXISTS
                };

            empleado.Nombre = dto.Nombre;
            empleado.Apellido = dto.Apellido;
            empleado.Documento = dto.Documento;
            empleado.FechaContratacion = dto.FechaContratacion;
            empleado.Departamento = dto.Departamento;
            empleado.PuestoTrabajo = dto.PuestoTrabajo;
            empleado.SalarioBase = dto.SalarioBase;

            _context.Empleados.Update(empleado);
            await _context.SaveChangesAsync();

            return new ResponseDto<EmpleadoDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_UPDATED,
                Data = MapToDto(empleado)
            };
        }

        public async Task<ResponseDto<EmpleadoDto>> DeleteAsync(int id)
        {
            var empleado = await _context.Empleados.FirstOrDefaultAsync(e => e.Id == id);
            if (empleado is null)
                return new ResponseDto<EmpleadoDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };

            // Baja logica: solo cambia Activo a false
            empleado.Activo = false;
            _context.Empleados.Update(empleado);
            await _context.SaveChangesAsync();

            return new ResponseDto<EmpleadoDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_DELETED,
                Data = MapToDto(empleado)
            };
        }

        public async Task<ResponseDto<List<EmpleadoDto>>> GetActivosAsync()
        {
            var empleados = await _context.Empleados
                .Where(e => e.Activo == true)
                .ToListAsync();

            return new ResponseDto<List<EmpleadoDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = empleados.Select(e => MapToDto(e)).ToList()
            };
        }

        // Metodo privado para convertir Entity a Dto
        private EmpleadoDto MapToDto(EmpleadoEntity e)
        {
            return new EmpleadoDto
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Apellido = e.Apellido,
                Documento = e.Documento,
                FechaContratacion = e.FechaContratacion,
                Departamento = e.Departamento,
                PuestoTrabajo = e.PuestoTrabajo,
                SalarioBase = e.SalarioBase,
                Activo = e.Activo
            };
        }
    }
}