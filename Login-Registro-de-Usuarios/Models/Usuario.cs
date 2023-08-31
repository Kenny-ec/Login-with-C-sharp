using System.ComponentModel.DataAnnotations;

namespace Login_Registro_de_Usuarios.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Clave { get; set; }
    }
}
