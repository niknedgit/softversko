using Agencija_4C.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencija_4C.DTOs
{
    public class DestinacijaView
    {
        public int Id { get; set; }
        public String Naziv { get; set; }
        public String Opis { get; set; }

        //public IList<SmestajView> ImaSmestaje { get; set; }
        //public IList<SlikeView> ImaSlike { get; set; }
        public IList<String> ImaSlike { get; set; }

        public DestinacijaView(Destinacija d)
        {
            this.Id = d.Id;
            this.Naziv = d.Naziv;
            this.Opis = d.Opis;

            //this.ImaSlike = new List<SlikeView>();
            //foreach (Slike slike in d.ImaSlike)
            //{
            //    this.ImaSlike.Add(new SlikeView(slike));
            //}
            this.ImaSlike = new List<String>();
            foreach (Slike slike in d.ImaSlike)
            {
                this.ImaSlike.Add(slike.UrlSlike);
            }
        }
    }
}
