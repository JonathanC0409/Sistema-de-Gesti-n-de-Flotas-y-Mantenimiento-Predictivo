using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLogs.Modelos
{
    public class LogAuditoria
    {
        [Key]
        public int Codigo { get; set; }
        public string Entidad { get; set; }
        public string Accion { get; set; }
        public DateTime FechaAccion { get; set; }
        public string Usuario { get; set; }
        public string Detalles { get; set; }

    }
}
