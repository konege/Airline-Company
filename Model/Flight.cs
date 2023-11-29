using System;
using System.Collections.Generic;

namespace midterm_project.Model;

public partial class Flight
{
    public int FlightId { get; set; }

    public DateTime? FlightDate { get; set; }

    public string? Departure { get; set; }

    public string? Destination { get; set; }

    public string? FlightNumber { get; set; }

    public int? Price { get; set; }

    public int? TotalSeat { get; set; }

    public int? AvailableSeat { get; set; }

    public virtual ICollection<ClientFlight> ClientFlights { get; } = new List<ClientFlight>();
}
