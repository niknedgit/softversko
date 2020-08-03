using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencija_4C.Entiteti
{
    public class Klijent
    {
        public virtual int Id { get; set; }
        public virtual String PunoIme { get; set; }
        public virtual String Mail { get; set; }
        public virtual String Password { get; set; }
        public virtual String Username { get; set; }
      //  public virtual DateTime Datum { get; set; }

        public virtual IList<Smestaj> Smestaji { get; set; }
        public virtual IList<Rezervacija> RezervacijeK { get; set; }
     //   public virtual IList<Nalog> Nalozi { get; set; }

        public Klijent()
        {
            Smestaji = new List<Smestaj>();
            RezervacijeK = new List<Rezervacija>();
        //    Nalozi = new List<Nalog>();
        }
    }
}
