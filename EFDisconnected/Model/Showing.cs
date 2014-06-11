using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDisconnectedSample.Model
{
    public class Showing : IStateTracker
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public virtual Film Film { get; set; }
        public int FilmId { get; set; }
        public virtual Location Location { get; set; }
        public int LocationId { get; set; }

        [NotMapped]
        public List<string> ModifiedProperties { get; set; }
        [NotMapped]
        public State State { get; set; }
    }
}
