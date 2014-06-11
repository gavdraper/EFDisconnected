using System;
using System.Collections.Generic;
using EFDisconnectedSample.Data;
using EFDisconnectedSample.Model;

namespace EFDisconnectedSample
{
    class Program
    {
        public static Cinema BuildDisconnectedCinema()
        {
            var films = new List<Film>()
            {
                new Film("Jurassic Park"){State=State.Added},
                new Film("Star Wars"){State = State.Added}
            };

            var showings = new List<Showing>()
            {
                new Showing() {Film = films[0], Start = new DateTime(2001, 01, 01, 15, 20, 00), State = State.Added},
                new Showing() {Film = films[0], Start = new DateTime(2001, 01, 01, 18, 00, 00), State = State.Added},
                new Showing() {Film = films[1], Start = new DateTime(2001, 01, 01, 15, 00, 00), State = State.Added},
                new Showing() {Film = films[1], Start = new DateTime(2001, 01, 01, 17, 30, 00), State = State.Added},
            };

            var locations = new List<Location>()
            {
                new Location("Brighton"){State = State.Added, Showings = new List<Showing>(){showings[0],showings[1]}},
                new Location("London"){State = State.Added, Showings = new List<Showing>(){showings[2],showings[3]}},
            };

            var cinema = new Cinema("Cinemagic!") {State = State.Added, Locations = locations};
            return cinema;
        }

        public static Cinema LoadDisconnectAndModifyCinema()
        {
            var repo = new GlobalRepository();
            var cinema = repo.GetCinema();
            cinema.Name = "Cinemagic";
            cinema.ModifiedProperties.Add("Name");
            cinema.Locations[0].Showings[0].Film.Name = "Jurassic Park";
            cinema.Locations[0].Showings[0].Film.ModifiedProperties.Add("Name");
            cinema.Locations.Add(new Location("Crawley") { State = State.Added });
            cinema.Locations.Add(new Location("Worthing") { State = State.Added });
            return cinema;
        }

        static void Save(Cinema cinema, Action<Cinema> action, CinemaContext ctx)
        {
            try
            {
                action(cinema);
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("------------------------");
                Console.WriteLine("Press Enter To Continue......");
                Console.ReadLine();
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1) Add new");
                Console.WriteLine("2) Add update existing");
                Console.WriteLine("3) Attach New");
                Console.WriteLine("4) Attach and update existing");
                Console.WriteLine("q) Quit");
                var input = Console.ReadLine();

                if (input == "1")
                    using (var ctx = new CinemaContext()) { Save(BuildDisconnectedCinema(), ctx.AddDisconnectedEntity, ctx); }
                else if (input == "2")
                    using (var ctx = new CinemaContext()) { Save(LoadDisconnectAndModifyCinema(), ctx.AddDisconnectedEntity, ctx); }
                if (input == "3")
                    using (var ctx = new CinemaContext()) { Save(BuildDisconnectedCinema(), ctx.AttachDisconnectedEntity, ctx); }
                else if (input == "4")
                    using (var ctx = new CinemaContext()) { Save(LoadDisconnectAndModifyCinema(), ctx.AttachDisconnectedEntity, ctx); }
                else if (input == "q")
                    break;
            }
        }
    }
}
