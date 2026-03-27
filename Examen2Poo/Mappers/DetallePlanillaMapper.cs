using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen2Poo.Dtos.DetallePlanillas;
using Examen2Poo.Dtos.DetallesPlanilla;

namespace Examen2Poo.Mappers
{
 public class DetallePlanillaMapper
 {
 public static DetallePlanillaDto ToDetallePlanillaDto(DetallePlanillaCreateDto detallePlanilla)
 {
 return new DetallePlanillaDto
 {
EmpleadoId = detallePlanilla.EmpleadoId,
PlanillaId = detallePlanilla.PlanillaId,
SalarioBase = detallePlanilla.SalarioBase,
HorasExtras = detallePlanilla.HorasExtras,
MontoHorasExtras = detallePlanilla.MontoHorasExtras,
Bonificaviones = detallePlanilla.Bonificaciones,
Deducciones = detallePlanilla.Deducciones,
SalarioNeto = detallePlanilla.SalarioNeto,
Comentarios = detallePlanilla.Comentarios
 };

 }
 }}