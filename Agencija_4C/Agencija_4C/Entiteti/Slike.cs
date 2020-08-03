using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencija_4C.Entiteti
{
    public class Slike
    {
        public virtual int Id { get; set; }
        public virtual String UrlSlike { get; set; }

        public virtual Smestaj SmestajS { get; set; }
        public virtual Destinacija DestinacijaS{ get; set; }
    }
}
