using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGFYMP.Modelos
{
    public class Taller
    {
        [Key]
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public int CapacidadMaximaReparaciones { get; set; }

        // Navegación a las entidades relacionadas
        public List<MantenimientoProgramado>? MantenimientosProgramados { get; set; } // Lista de mantenimientos programados asociados al taller
    }
}
