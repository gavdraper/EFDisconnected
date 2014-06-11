using System.Collections.Generic;
using System.Security;

namespace EFDisconnectedSample.Model
{
    public class Location : IStateTracker
    {
        public int Id { get; set; }     
        public string Name { get; set; }
        public virtual List<Showing> Showings { get; set; }
        public int CinemaId { get; set; }
        public virtual Cinema Cinema{get;set;}

        public Location(){}

        public Location(string name) : this()
        {
            Name = name;
        }

        public List<string> ModifiedProperties { get; set; }
        public State State { get; set; }
    }
}
