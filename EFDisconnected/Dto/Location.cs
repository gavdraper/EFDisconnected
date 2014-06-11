using System.Collections.Generic;
using EFDisconnectedSample.Model;

namespace EFDisconnectedSample.Dto
{
    public class LocationDto : IStateTracker
    {
        public int Id { get; set; }     
        public string Name { get; set; }
        public virtual List<ShowingDto> Showings { get; set; }
        public int CinemaId { get; set; }
        public virtual CinemaDto Cinema{get;set;}

        public List<string> ModifiedProperties { get; set; }
        public State State { get; set; }
    }
}
