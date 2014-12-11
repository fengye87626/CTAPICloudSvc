using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiCTCombinaiton.Models
{
    public class Error
    {
        public string EventId { get; set; }
        public string Message { get; set; }
        public DateTimeOffset OccurredOn { get; set; }
        public object Parameters { get; set; }
    }
}
