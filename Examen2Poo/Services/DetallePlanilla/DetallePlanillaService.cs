using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Examen2Poo.Constants;
using Examen2Poo.Database;
using Examen2Poo.Dtos.Common;
using Examen2Poo.Dtos.DetallesPlanilla;
using Examen2Poo.Entities;

namespace Examen2Poo.Services.DetallesPlanilla
{
    public class DetallePlanillaService : IDetallePlanillaService
    {
        private readonly PlanillaDbContext _context;

        public DetallePlanillaService(PlanillaDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<List<DetallePlanillaDto>>> GetByPlanillaIdAsync(int planillaId)
        {
            var detalles = await _context.DetallesPlanilla
                .Include(d => d.Empleado)
                .Where(d => d.PlanillaId == planillaId)
                .ToListAsync();

            return new ResponseDto<List<DetallePlanillaDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = detalles.Select(d => MapToDto(d)).ToList()
            };
        }

        public async Task<ResponseDto<DetallePlanillaDto>> GetOneByIdAsync(int id)
        {
            var detalle = await _context.DetallesPlanilla
                .Include(d => d.Empleado)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (detalle is null)
                return new ResponseDto<DetallePlanillaDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };

            return new ResponseDto<DetallePlanillaDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Data = MapToDto(detalle)
            };
        }

        public async Task<ResponseDto<DetallePlanillaDto>> CreateAsync(DetallePlanillaCreateDto dto)
        {
            // Calcular salario neto automaticamente
            var salarioNeto = dto.SalarioBase + dto.MontoHorasExtra + dto.Bonificaciones - dto.Deducciones;

            var detalle = new DetallePlanillaEntity
            {
                PlanillaId = dto.PlanillaId,
                EmpleadoId = dto.EmpleadoId,
                SalarioBase = dto.SalarioBase,
                HorasExtra = dto.HorasExtra,
                MontoHorasExtra = dto.MontoHorasExtra,
                Bonificaciones = dto.Bonificaciones,
                Deducciones = dto.Deducciones,
                SalarioNeto = salarioNeto, // calculado automaticamente
                Comentarios = dto.Comentarios
            };

            _context.DetallesPlanilla.Add(detalle);
            await _context.SaveChangesAsync();

            return new ResponseDto<DetallePlanillaDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_CREATED,
                Data = MapToDto(detalle)
            };
        }

        public async Task<ResponseDto<DetallePlanillaDto>> EditAsync(int id, DetallePlanillaCreateDto dto)
        {
            var detalle = await _context.DetallesPlanilla
                .FirstOrDefaultAsync(d => d.Id == id);

            if (detalle is null)
                return new ResponseDto<DetallePlanillaDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };

            // Recalcular salario neto
            detalle.SalarioBase = dto.SalarioBase;
            detalle.HorasExtra = dto.HorasExtra;
            detalle.MontoHorasExtra = dto.MontoHorasExtra;
            detalle.Bonificaciones = dto.Bonificaciones;
            detalle.Deducciones = dto.Deducciones;
            detalle.SalarioNeto = dto.SalarioBase + dto.MontoHorasExtra + dto.Bonificaciones - dto.Deducciones;
            detalle.Comentarios = dto.Comentarios;

            _context.DetallesPlanilla.Update(detalle);
            await _context.SaveChangesAsync();

            return new ResponseDto<DetallePlanillaDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_UPDATED,
                Data = MapToDto(detalle)
            };
        }

        public async Task<ResponseDto<DetallePlanillaDto>> DeleteAsync(int id)
        {
            var detalle = await _context.DetallesPlanilla.FirstOrDefaultAsync(d => d.Id == id);
            if (detalle is null)
                return new ResponseDto<DetallePlanillaDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };

            _context.DetallesPlanilla.Remove(detalle);
            await _context.SaveChangesAsync();

            return new ResponseDto<DetallePlanillaDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_DELETED
            };
        }

        public async Task<ResponseDto<List<DetallePlanillaDto>>> GetByEmpleadoIdAsync(int empleadoId)
        {
            var detalles = await _context.DetallesPlanilla
                .Include(d => d.Empleado)
                .Where(d => d.EmpleadoId == empleadoId)
                .ToListAsync();

            return new ResponseDto<List<DetallePlanillaDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = detalles.Select(d => MapToDto(d)).ToList()
            };
        }

        private DetallePlanillaDto MapToDto(DetallePlanillaEntity d)
        {
            return new DetallePlanillaDto
            {
                Id = d.Id,
                PlanillaId = d.PlanillaId,
                EmpleadoId = d.EmpleadoId,
                NombreEmpleado = d.Empleado != null ? $"{d.Empleado.Nombre} {d.Empleado.Apellido}" : "",
                SalarioBase = d.SalarioBase,
                HorasExtra = d.HorasExtra,
                MontoHorasExtra = d.MontoHorasExtra,
                Bonificaciones = d.Bonificaciones,
                Deducciones = d.Deducciones,
                SalarioNeto = d.SalarioNeto,
                Comentarios = d.Comentarios
            };
        }
    }
}