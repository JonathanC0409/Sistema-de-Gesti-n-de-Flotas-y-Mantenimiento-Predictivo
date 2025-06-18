using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGFYMP.Modelos
{
    public class MantenimientoProgramado
    {
        [Key]
        public int Codigo { get; set; } // Identificador único del mantenimiento programado
        public DateTime Fecha { get; set; }
        public string TipoMantenimiento { get; set; } // Ejemplo: "Cambio de aceite", "Revisión de motor", etc.

        //Claves foraneas 
        public int TallerCodigo { get; set; }
        public int CamionCodigo { get; set; }

        // Navegación a las entidades relacionadas
        public Taller? Taller { get; set; } // Taller donde se realizará el mantenimiento
        public Camion? Camion { get; set; } // Camión que recibirá el mantenimiento
    }
}
