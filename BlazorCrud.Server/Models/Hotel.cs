using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Hotel
{
    public int IdHotel { get; set; }

    public string Nombre { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public decimal Latitud { get; set; }

    public decimal Longitud { get; set; }

    public string? Descripcion { get; set; }

    public string Activo { get; set; } = null!;

    public string CantHabitaciones { get; set; } = null!;

    public virtual ICollection<Habitacion> Habitaciones { get; } = new List<Habitacion>();

    public virtual ICollection<Reserva> Reservas { get; } = new List<Reserva>();
}
