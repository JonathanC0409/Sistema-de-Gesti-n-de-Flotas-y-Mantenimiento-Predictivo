using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLogs.Modelos
{
    public class AlertaMantenimiento
    {
        [Key]
        public int Codigo { get; set; }
        public int CamionCodigo { get; set; }  // Referencia al camión (relación con la tabla Camion)
        public string TipoMantenimiento { get; set; }  // Tipo de mantenimiento (por ejemplo, 'Revisión', 'Cambio de aceite')
        public string Descripcion { get; set; }  // Descripción de la alerta
        public DateTime FechaGeneracion { get; set; }  // Fecha de generación de la alerta
        public bool Resuelta { get; set; }  // Indica si la alerta ha sido resuelta
        public DateTime? FechaResolucion { get; set; }  // Fecha de resolución de la alerta (si corresponde)
    }
}

