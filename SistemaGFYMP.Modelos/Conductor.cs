using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGFYMP.Modelos
{
    public class Conductor
    {
        [Key]
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Licencia { get; set; }
        public DateTime FechaVencimientoLicencia { get; set; }


        // Navegación a las entidades relacionadas
        public List<Camion>? Camiones { get; set; } // Lista de camiones asignados al conductor
    }
}
