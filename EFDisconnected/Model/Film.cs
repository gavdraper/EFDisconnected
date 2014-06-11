using System.Collections.Generic;

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

        public List<string> ModifiedProperties { get; set; }
        public State State { get; set; }
    }
}
