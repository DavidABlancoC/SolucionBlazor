using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
   


    public class HotelDTO
    {
        // Valores minimos y maximos de latitud y longitud

        const double MINLATITUDE = -90.000000;
        const double MAXLATITUDE = 90.000000;
        const double MINLONGITUDE = -180.000000;
        const double MAXLONGITUDE = 180.000000;

        // Campos de vista de cliente para Hotel, con respectivas restricciones

        [Display(Name = "Codigo Hotel")]
        public int IdHotel { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Pais { get; set; } = null!;

        [Required]
        [Range(MINLATITUDE, MAXLATITUDE, ErrorMessage = "El campo {0} es requerido.")]
        public decimal Latitud { get; set; }

        [Required]
        [Range(MINLONGITUDE, MAXLONGITUDE, ErrorMessage = "El campo {0} es requerido.")]
        public decimal Longitud { get; set; }

        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Activo { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string CantHabitaciones { get; set; } = null!;
    }
}
