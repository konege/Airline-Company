using midterm_project.Model;

namespace Repos
{
    public class ClientFlightRepo 
    {
       public bool IsAvailableToFlight(int clientId, int flightId)
        {
            using var context = new KonegeContext();
            try
            {
                var clientFlight = context.ClientFlights.First(u => u.ClientId == clientId && u.FlightId == flightId);
                return clientFlight != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}