
using Microsoft.EntityFrameworkCore;
using midterm_project.Model;

namespace Repos
{
    public class FlightRepo
    {
        public List<Flight> GetFlights(QueryTicket model)
        {
            using var context = new KonegeContext();
            var filter = new PaginationManager(model.PageNumber, model.PageSize);

            var pagedData = context.Flights
                .Where(flight => flight.FlightDate == model.Date && flight.Departure == model.Departure && flight.Destination == model.Destination && flight.AvailableSeat >= model.SoldTicket)
                .Skip((filter.minNumber - 1) * filter.maxNumber)
                .Take(filter.maxNumber)
                .ToList();

            return pagedData;
        }

        public Flight? UpdateFlightClients(int id)
        {
            using var context = new KonegeContext();
            try
            {
                var flight = context.Flights.Find(id);
                if (flight != null)
                {
                    if (flight.AvailableSeat >= 1)
                    {
                        flight.AvailableSeat--;
                        context.Flights.Update(flight);
                        context.SaveChanges();
                    }
                }
                return flight;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Flight CreateFlight(Flight f)
        {
            var flight = new Flight { FlightDate = f.FlightDate, FlightNumber = f.FlightNumber, Departure = f.Departure, Destination = f.Destination, AvailableSeat = f.AvailableSeat, TotalSeat = f.TotalSeat, Price = f.Price };
            using var context = new KonegeContext();
            context.Flights.Add(flight);
            context.SaveChanges();
            return f;
        }

        public string BuyTicket(BuyTicket ticket, Client client)
        {
            var status = "";
            using var context = new KonegeContext();

            // Use FirstOrDefault instead of First
            var flight = context.Flights.FirstOrDefault(x => x.FlightNumber == ticket.FlightNo);
            if (flight != null)
            {
                // Use FirstOrDefault instead of First
                var flightClient = context.ClientFlights.FirstOrDefault(x => x.FlightId == flight.FlightId && x.ClientId == client.ClientId);

                if (flightClient != null)
                {
                    status = "Client has already on the flight";
                }
                else
                {
                    var entity = context.ClientFlights.Add(new ClientFlight { ClientId = client.ClientId, FlightId = flight.FlightId });
                    context.SaveChanges();

                    if (entity != null)
                    {
                        var updatedFlight = UpdateFlightClients(flight.FlightId);
                        if (updatedFlight != null && updatedFlight.AvailableSeat < flight.AvailableSeat)
                        {
                            status = "Client assigned to flight.";
                        }
                        else
                        {
                            status = "There are no available seats :( ";
                        }
                    }
                    else
                    {
                        status = "Client can't be assigned to the flight";
                    }
                }
            }
            else
            {
                status = "Flight cannot be found!";
            }
            return status;
        }

    }
}