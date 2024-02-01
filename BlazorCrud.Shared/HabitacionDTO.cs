using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class HabitacionDTO
    {
        [Display(Name = "Codigo Habitacion")]
        public int IdHabitacion { get; set; }

        [Display(Name = "Codigo Hotel")]
        public int IdHotel { get; set; }

        [Display(Name = "Numero de Habitacion")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string NumeroHabitacion { get; set; } = null!;

        [Required (ErrorMessage = "El campo {0} es requerido.")]
        public string Estado { get; set; } = null!;

        // Conexion a tabla Hotel
        public HotelDTO Hotel { get; set; }
    }
}
