using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencija_4C.DataWrapper
{
    public class PutSmestaj
    {
        public int Id { get; set; }
        public String Naziv { get; set; }
        public String Opis { get; set; }
        public int CenaPoKrevetu { get; set; }
        public int CenaPrevoza { get; set; }
    }
}
