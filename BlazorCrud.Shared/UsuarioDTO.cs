using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class UsuarioDTO
    {
        [Display(Name = "Codigo Usuario")]                           // Id Usuario
        public int IdUsuario { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es requerido.")]      // Nombre
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido.")]      // Apellido
        public string Apellidos { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Formato de correo invalido.")] // Mail
        public string Mail { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido.")]      // Direccion
        public string Direccion { get; set; } = null!;
    }
}
