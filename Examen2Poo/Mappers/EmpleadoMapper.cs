using Examen2Poo.Entities;
using Examen2Poo.Dtos.Empleados;

namespace Examen2Poo.Mappers
{
public class EmpleadoMapper
{
 public static EmpleadoEntity ToEmpleadoEntity(EmpleadoCreateDto empleado)
        {
              return new EmpleadoEntity  
 {
               Id = empleado.Id,
                Nombre = empleado.Nombre,
                Apellido = empleado.Apellido,
                Documento = empleado.Documento,
                FechaContratacion = empleado.FechaContratacion,
                Departamento = empleado.Departamento,
                PuestoTrabajo= empleado.PuestoTrabajo,
                SalarioBase = empleado.SalarioBase,
                Activo = empleado.Activo

 };
 } }
}