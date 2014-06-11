using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped]
        public List<string> ModifiedProperties { get; set; }
        [NotMapped]
        public State State { get; set; }
    }
}
