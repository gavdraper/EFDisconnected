using System;
using System.Collections.Generic;

namespace EFDisconnectedSample
{
    public interface IStateTracker
    {
        List<String> ModifiedProperties { get; set; }
        State State { get; set; }
    }


}
