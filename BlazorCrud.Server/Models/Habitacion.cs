using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Habitacion
{
    public int IdHabitacion { get; set; }

    public int IdHotel { get; set; }

    public string NumeroHabitacion { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual Hotel IdHotelNavigation { get; set; } = null!;

    public virtual ICollection<Reserva> Reservas { get; } = new List<Reserva>();
}
