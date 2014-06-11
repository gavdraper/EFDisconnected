using System.Collections.Generic;

namespace EFDisconnectedSample.Model
{
    public class Cinema : IStateTracker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Location> Locations { get; set; }

        public Cinema() { }

        public Cinema(string name) : this()
        {
            Name = name;
        }

        public List<string> ModifiedProperties { get; set; }
        public State State { get; set; }
    }
}
