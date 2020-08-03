using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencija_4C.Entiteti
{
    public class Smestaj
    {
        public virtual int Id { get; set; }
        public virtual int CenaPoKrevetu { get; set; }
        public virtual String Opis { get; set; }
        public virtual String Naziv { get; set; }
        public virtual int CenaPrevoza { get; set; }
        public virtual int BrGlasova { get; set; }
        public virtual int Ocena { get; set; }
        public virtual Destinacija PripadaDestinaciji { get; set; }

        public virtual IList<Slike> SlikeSmestaja { get; set; }

        public virtual IList<Klijent> Klijenti { get; set; }
        public virtual IList<Rezervacija> RezervacijeS { get; set; }
        public virtual IList<Termini> ImaTermine { get; set; }

        public Smestaj()
        {
            SlikeSmestaja = new List<Slike>();
            Klijenti = new List<Klijent>();
            RezervacijeS = new List<Rezervacija>();
        }
    }
}
