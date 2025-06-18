using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLogs.Modelos
{
    public class AlertaPredictiva
    {
        [Key]
        public int Codigo { get; set; }
        public int CamionCodigo { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public string TipoAlerta { get; set; } // Ejemplo: "Kilometraje", "Mantenimiento Programado", etc.
    }
}
