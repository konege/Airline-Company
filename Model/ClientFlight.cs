using System;
using System.Collections.Generic;

namespace midterm_project.Model;

public partial class ClientFlight
{
    public int Id { get; set; }

    public int? ClientId { get; set; }

    public int? FlightId { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Flight? Flight { get; set; }
}
