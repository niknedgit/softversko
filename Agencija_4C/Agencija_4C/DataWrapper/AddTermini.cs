using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencija_4C.DataWrapper
{
    public class AddTermini
    {
        public DateTime Od { get; set; }
        public DateTime Do { get; set; }

        public int BrTrokrevetnih { get; set; }
        public int BrCetvorokrevetnih { get; set; }
        public int BrDuplex { get; set; }

        public int IdHotela { get; set; }
    }
}
