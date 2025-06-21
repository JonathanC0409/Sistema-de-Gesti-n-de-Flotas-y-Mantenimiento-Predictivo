using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLogs.Modelos
{
    public class LecturaSensor
    {
        [Key]
        public int Codigo { get; set; }  // Identificador único de la lectura de sensor
        public int CamionCodigo { get; set; }  // Referencia al camión (relación con la tabla Camion)
        public double Kilometraje { get; set; }  // Kilometraje del camión
        public string EstadoMotor { get; set; }  // Estado del motor ("Normal", "Alerta")
        public DateTime Fecha { get; set; }  // Fecha de la lectura
        public string? Comentarios { get; set; }  // Comentarios adicionales sobre la lectura
        public double NivelGasolina { get; set; }  // Nivel de gasolina (opcional)
        public double NivelAceite { get; set; }  // Nivel de aceite (opcional)
    }
}
