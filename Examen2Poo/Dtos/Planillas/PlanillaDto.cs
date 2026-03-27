using System;

namespace Examen2Poo.Dtos.Planillas
{
    public class PlanillaDto
    {
        public int Id { get; set; }
        public string Periodo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaPago { get; set; }
        public string Estado { get; set; }
    }
}