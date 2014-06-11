using System.Collections.Generic;

namespace EFDisconnectedSample.Dto
{
    public class FilmDto : IStateTracker
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<string> ModifiedProperties { get; set; }
        public State State { get; set; }
    }
}
