using Agencija_4C.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencija_4C.DTOs
{
    public class SmestajView
    {
        public int Id { get; set; }
        public int CenaPoKrevetu { get; set; }
        public String Opis { get; set; }
        public String Naziv { get; set; }
        public int CenaPrevoza { get; set; }
        //public int BrGlasova { get; set; }
        public int Ocena { get; set; }
        //public IList<SlikeView> SlikeSmestaja { get; set; }  
        public IList<String> SlikeSmestaja { get; set; }
        public IList<TerminiView> ImaTermine { get; set; }

        public SmestajView(Smestaj s)
        {
            this.Id = s.Id;
            this.CenaPoKrevetu = s.CenaPoKrevetu;
            this.Opis = s.Opis;
            this.Naziv = s.Naziv;
            this.CenaPrevoza = s.CenaPrevoza;
            //this.BrGlasova = s.BrGlasova;
            this.Ocena = s.Ocena;

            //this.SlikeSmestaja = new List<SlikeView>();
            //foreach (Slike slike in s.SlikeSmestaja)
            //{
            //    this.SlikeSmestaja.Add(new SlikeView(slike));
            //}
            this.SlikeSmestaja = new List<String>();
            foreach (Slike slike in s.SlikeSmestaja)
            {
                this.SlikeSmestaja.Add(slike.UrlSlike);
            }
            this.ImaTermine = new List<TerminiView>();
            foreach (Termini termini in s.ImaTermine)
            {
                this.ImaTermine.Add(new TerminiView(termini));
            }
        }
    }
}
