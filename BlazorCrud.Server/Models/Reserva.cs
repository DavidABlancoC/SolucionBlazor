using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public int IdUsuario { get; set; }

    public int IdHotel { get; set; }

    public int IdHabitacion { get; set; }

    public DateTime FechaEntrada { get; set; }

    public DateTime FechaSalida { get; set; }

    public DateTime FechaReserva { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Habitacion IdHabitacionNavigation { get; set; } = null!;

    public virtual Hotel IdHotelNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
