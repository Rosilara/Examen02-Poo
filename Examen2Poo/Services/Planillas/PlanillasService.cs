using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Examen2Poo.Constants;
using Examen2Poo.Database;
using Examen2Poo.Dtos.Common;
using Examen2Poo.Dtos.Planillas;
using Examen2Poo.Entities;

namespace Examen2Poo.Services.Planillas
{
    public class PlanillaService : IPlanillaService
    {
        private readonly PlanillaDbContext _context;

        public PlanillaService(PlanillaDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<List<PlanillaDto>>> GetAllAsync()
        {
            var planillas = await _context.Planillas.ToListAsync();
            return new ResponseDto<List<PlanillaDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = planillas.Select(p => MapToDto(p)).ToList()
            };
        }

        public async Task<ResponseDto<PlanillaDto>> GetOneByIdAsync(int id)
        {
            var planilla = await _context.Planillas.FirstOrDefaultAsync(p => p.Id == id);
            if (planilla is null)
                return new ResponseDto<PlanillaDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };

            return new ResponseDto<PlanillaDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Data = MapToDto(planilla)
            };
        }

        public async Task<ResponseDto<PlanillaDto>> CreateAsync(PlanillaCreateDto dto)
        {
            // Validar periodo unico
            var existing = await _context.Planillas
                .FirstOrDefaultAsync(p => p.Periodo == dto.Periodo);
            if (existing is not null)
                return new ResponseDto<PlanillaDto>
                {
                    StatusCode = HttpStatusCode.CONFLICT,
                    Status = false,
                    Message = HttpMessageResponse.PERIODO_EXISTS
                };

            var planilla = new PlanillaEntity
            {
                Periodo = dto.Periodo,
                FechaCreacion = DateTime.Now,
                FechaPago = dto.FechaPago,
                Estado = "Pendiente"
            };

            _context.Planillas.Add(planilla);
            await _context.SaveChangesAsync();

            return new ResponseDto<PlanillaDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_CREATED,
                Data = MapToDto(planilla)
            };
        }

        public async Task<ResponseDto<PlanillaDto>> EditAsync(int id, PlanillaCreateDto dto)
        {
            var planilla = await _context.Planillas.FirstOrDefaultAsync(p => p.Id == id);
            if (planilla is null)
                return new ResponseDto<PlanillaDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };

            planilla.Periodo = dto.Periodo;
            planilla.FechaPago = dto.FechaPago;

            _context.Planillas.Update(planilla);
            await _context.SaveChangesAsync();

            return new ResponseDto<PlanillaDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_UPDATED,
                Data = MapToDto(planilla)
            };
        }

        public async Task<ResponseDto<PlanillaDto>> DeleteAsync(int id)
        {
            var planilla = await _context.Planillas.FirstOrDefaultAsync(p => p.Id == id);
            if (planilla is null)
                return new ResponseDto<PlanillaDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };

            // No permitir eliminar planilla Pagada
            if (planilla.Estado == "Pagada")
                return new ResponseDto<PlanillaDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = HttpMessageResponse.PLANILLA_PAGADA
                };

            _context.Planillas.Remove(planilla);
            await _context.SaveChangesAsync();

            return new ResponseDto<PlanillaDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_DELETED
            };
        }

        public async Task<ResponseDto<List<PlanillaDto>>> GetByPeriodoAsync(string periodo)
        {
            var planillas = await _context.Planillas
                .Where(p => p.Periodo == periodo)
                .ToListAsync();

            return new ResponseDto<List<PlanillaDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = planillas.Select(p => MapToDto(p)).ToList()
            };
        }

        public async Task<ResponseDto<PlanillaDto>> UpdateEstadoAsync(int id, PlanillaEstadoDto dto)
        {
            var planilla = await _context.Planillas.FirstOrDefaultAsync(p => p.Id == id);
            if (planilla is null)
                return new ResponseDto<PlanillaDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };

            planilla.Estado = dto.Estado;
            _context.Planillas.Update(planilla);
            await _context.SaveChangesAsync();

            return new ResponseDto<PlanillaDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_UPDATED,
                Data = MapToDto(planilla)
            };
        }

        public async Task<ResponseDto<PlanillaDto>> GenerarPlanillaAsync(string periodo)
        {
            // Validar periodo unico
            var existing = await _context.Planillas
                .FirstOrDefaultAsync(p => p.Periodo == periodo);
            if (existing is not null)
                return new ResponseDto<PlanillaDto>
                {
                    StatusCode = HttpStatusCode.CONFLICT,
                    Status = false,
                    Message = HttpMessageResponse.PERIODO_EXISTS
                };

            // Crear la planilla
            var planilla = new PlanillaEntity
            {
                Periodo = periodo,
                FechaCreacion = DateTime.Now,
                FechaPago = DateTime.Now.AddDays(5),
                Estado = "Pendiente"
            };

            _context.Planillas.Add(planilla);
            await _context.SaveChangesAsync();

            // Obtener todos los empleados activos
            var empleadosActivos = await _context.Empleados
                .Where(e => e.Activo == true)
                .ToListAsync();

            // Crear un detalle por cada empleado activo
            foreach (var empleado in empleadosActivos)
            {
                var detalle = new DetallePlanillaEntity
                {
                    PlanillaId = planilla.Id,
                    EmpleadoId = empleado.Id,
                    SalarioBase = empleado.SalarioBase,
                    HorasExtra = 0,
                    MontoHorasExtra = 0,
                    Bonificaciones = 0,
                    Deducciones = 0,
                    SalarioNeto = empleado.SalarioBase // sin extras aun
                };
                _context.DetallesPlanilla.Add(detalle);
            }

            await _context.SaveChangesAsync();

            return new ResponseDto<PlanillaDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = $"Planilla generada para {empleadosActivos.Count} empleados activos",
                Data = MapToDto(planilla)
            };
        }

        private PlanillaDto MapToDto(PlanillaEntity p)
        {
            return new PlanillaDto
            {
                Id = p.Id,
                Periodo = p.Periodo,
                FechaCreacion = p.FechaCreacion,
                FechaPago = p.FechaPago,
                Estado = p.Estado
            };
        }
    }
}