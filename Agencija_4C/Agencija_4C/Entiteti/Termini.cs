using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencija_4C.Entiteti
{
    public class Termini
    {
        public virtual int Id { get; set; }
        public virtual DateTime Od { get; set; }
        public virtual DateTime Do { get; set; }
         
        public virtual int BrTrokrevetnih { get; set; }
        public virtual int BrCetvorokrevetnih { get; set; }
        public virtual int BrDuplex { get; set; }

        public virtual Smestaj VezanZaSmestaj { get; set; }
    }
}
