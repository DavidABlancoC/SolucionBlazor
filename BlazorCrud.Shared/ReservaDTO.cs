using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class ReservaDTO
    {
        [Display(Name = "Codigo Reserva")]                      // Id Reserva
        public int IdReserva { get; set; }                     

        [Display(Name = "Codigo Usuario")]                      // Id Usuario
        public int IdUsuario { get; set; }                      

        [Display(Name = "Codigo Hotel")]                        // Id Hotel
        public int IdHotel { get; set; }

        [Display(Name = "Codigo Habitacion")]                   // Id Habitacion
        public int IdHabitacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")] // Fecha Entrada
        public DateTime FechaEntrada { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")] // Fecha Salida
        public DateTime FechaSalida { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")] // Fecha Reserva
        public DateTime FechaReserva { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")] // Estado
        public string Estado { get; set; } = null!;


        // Conexion a tablas externas.

        public UsuarioDTO Usuario { get; set; }

        public HotelDTO Hotel { get; set; }

        public HabitacionDTO Habitacion { get; set; }
                
    }
}
