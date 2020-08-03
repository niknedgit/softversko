using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencija_4C.Entiteti
{
    public class Rezervacija
    {
        public virtual int Id { get; set; }

        public virtual Termini TerminiR { get; set; }
        public virtual Klijent KlijentR { get; set; }
        public virtual Smestaj SmestajR { get; set; }
        public virtual String Soba { get; set; }
        public virtual String Cekanje { get; set; }

        public Rezervacija()
        { }
    }
}
