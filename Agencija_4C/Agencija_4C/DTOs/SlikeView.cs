using Agencija_4C.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencija_4C.DTOs
{
    public class SlikeView
    {
        public int Id { get; set; }
        public String UrlSlike { get; set; }

        public SlikeView(Slike s)
        {
            this.Id = s.Id;
            this.UrlSlike = s.UrlSlike;
        }
    }
}
