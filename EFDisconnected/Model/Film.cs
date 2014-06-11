using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDisconnectedSample.Model
{
    public class Film : IStateTracker
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Film() { }

        public Film(string name) : this()
        {
            Name = name;
        }

        [NotMapped]
        public List<string> ModifiedProperties { get; set; }
        [NotMapped]
        public State State { get; set; }
    }
}
