using System.Linq;
using EFDisconnectedSample.Model;

namespace EFDisconnectedSample.Data
{
    class GlobalRepository
    {
        public Cinema GetCinema()
        {
            using (var ctx = new CinemaContext())
            {
                return ctx.Cinemas
                    .Include("Locations")
                    .Include("Locations.Showings")
                    .Include("Locations.Showings.Film").OrderByDescending(x=>x.Id).First();
            }
        }
    }
}
