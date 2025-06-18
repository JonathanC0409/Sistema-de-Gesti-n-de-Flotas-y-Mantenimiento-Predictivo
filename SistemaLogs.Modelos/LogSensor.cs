using SistemaGFYMP.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLogs.Modelos
{
    public class LogSensor
    {
        [Key]
        public int Codigo { get; set; } // Identificador único del log de sensor
        public int CamionCodigo { get; set; } // Clave foránea del camión
        public int Kilometraje { get; set; } // Kilometraje reportado por el sensor
        public string EstadoMotor { get; set; } // Estado del motor reportado por el sensor (Ej: "Operativo", "Fallo")
        public DateTime FechaLectura { get; set; } // Fecha y hora de la lectura del sensor

    }
}
