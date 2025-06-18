using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGFYMP.Modelos
{
    public class Usuario
    {
        [Key]
        public int Codigo { get; set; } // Identificador único del usuario
        public string Nombre { get; set; } // Nombre del usuario
        public string Email { get; set; } // Correo electrónico del usuario 
        public string Contrasena { get; set; } // Contraseña del usuario
    }
}
