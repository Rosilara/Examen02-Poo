using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen2Poo.Dtos.Planillas;
using Examen2Poo.Dtos.Planillas;

namespace Examen2Poo.Mappers
{
 public class PlanillasMapper
{
 public static PlanillaDto ToPlanillaDto(PlanillaCreateDto planilla)
{
 return new PlanillaDto
 {
 Periodo = planilla.Periodo,
 FechaCreacion = DateTime.FechaCreacion,
 FechaPago = DateTime.FechaPago,
Estado = planilla.Estado


 };
 }
}}
