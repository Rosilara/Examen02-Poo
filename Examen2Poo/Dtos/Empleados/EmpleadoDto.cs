using System;

namespace Examen2Poo.Dtos.Empleados
{
    public class EmpleadoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public DateTime FechaContratacion { get; set; }
        public string Departamento { get; set; }
        public string PuestoTrabajo { get; set; }
        public decimal SalarioBase { get; set; }
        public bool Activo { get; set; }
    }
}