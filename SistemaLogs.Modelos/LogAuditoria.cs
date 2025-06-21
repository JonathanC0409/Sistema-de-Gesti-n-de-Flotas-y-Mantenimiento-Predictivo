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
        public int Codigo { get; set; }  // Identificador único del log de auditoría
        public string Accion { get; set; }  // Acción realizada (por ejemplo, 'Creación', 'Modificación', 'Eliminación')
        public string Entidad { get; set; }  // Nombre de la entidad (por ejemplo, 'LecturaSensor', 'AlertaMantenimiento')
        public int EntidadCodigo { get; set; }  // ID de la entidad afectada
        public string Usuario { get; set; }  // Usuario que realizó la acción
        public DateTime Fecha { get; set; }  // Fecha y hora de la acción
        public string Detalles { get; set; }  // Detalles adicionales (por ejemplo, descripción del cambio)
    }
}
