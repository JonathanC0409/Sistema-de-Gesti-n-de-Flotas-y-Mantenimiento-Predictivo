using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGFYMP.Modelos
{
    public class Camion
    {
        [Key]
        public int Codigo { get; set; } // Identificador único del camión
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Año { get; set; }
        public string Placa { get; set; }
        public int Kilometraje { get; set; }
        public string Estado { get; set; }
        public double NivelGasolina { get; set; }  // Nivel de gasolina (por ejemplo, 5%)
        public double NivelAceite { get; set; }    // Nivel de aceite (por ejemplo, 12%)
        public string EstadoMotor { get; set; } // Estado del motor (por ejemplo, "En marcha", "Detenido")

        //Claves foráneas
        public int ConductorCodigo { get; set; } // Código del conductor asignado al camión

        //Navegación a las entidades relacionadas
        public List<MantenimientoProgramado>? MantenimientosProgramados { get; set; } // Lista de mantenimientos programados asociados al camión
        public Conductor? Conductor { get; set; } // Conductor asignado al camión

    }
}
