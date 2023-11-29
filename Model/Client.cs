using System;
using System.Collections.Generic;

namespace midterm_project.Model;

public partial class Client
{
    public int ClientId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Username { get; set; }

    public string? ClientPassword { get; set; }

    public string? Token { get; set; }

    public virtual ICollection<ClientFlight> ClientFlights { get; } = new List<ClientFlight>();
}
