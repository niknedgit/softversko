using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencija_4C.Entiteti
{
    public class Destinacija
    {
        public virtual int Id { get; set; }
        public virtual String Naziv { get; set; }
        public virtual String Opis { get; set; }

        public virtual IList<Smestaj> ImaSmestaje { get; set; }
        public virtual IList<Slike> ImaSlike { get; set; }

        public Destinacija()
        {
            ImaSlike = new List<Slike>();
            ImaSmestaje = new List<Smestaj>();
        }
    }
}
