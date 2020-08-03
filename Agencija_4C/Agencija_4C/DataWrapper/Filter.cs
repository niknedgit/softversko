using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencija_4C.DataWrapper
{
    public class Filter
    {
        public int idDest { get; set; }
        public String D_od { get; set; }
        public String D_do { get; set; }
        public int C_min { get; set; }
        public int C_max { get; set; }
    }
}
