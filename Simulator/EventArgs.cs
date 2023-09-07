using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

public class ProcessEventArgs : EventArgs
{
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}
