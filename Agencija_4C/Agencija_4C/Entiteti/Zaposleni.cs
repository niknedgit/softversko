using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencija_4C.Entiteti
{
    public class Zaposleni
    {
        public virtual int Id { get; set; }
        public virtual String Ime { get; set; }
        public virtual String Password { get; set; }
        public virtual String Mail { get; set; }
        public virtual String Username { get; set; }


        public Zaposleni()
        {
        }
    }
}
