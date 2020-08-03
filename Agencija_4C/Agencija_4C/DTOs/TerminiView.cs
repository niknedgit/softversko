using Agencija_4C.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencija_4C.DTOs
{
    public class TerminiView
    {
        public int Id { get; set; }
        public DateTime Od { get; set; }
        public DateTime Do { get; set; }
        public int BrTrokrevetnih { get; set; }
        public int BrCetvorokrevetnih { get; set; }
        public int BrDuplex { get; set; }

        //public IList<TerminiView> ListaTermina { get; set; }

        public TerminiView(Termini t)
        {
            //this.ListaTermina = new List<TerminiView>();
            this.Id = t.Id;
            this.Od = t.Od;
            this.Do = t.Do;
            this.BrTrokrevetnih = t.BrTrokrevetnih;
            this.BrCetvorokrevetnih = t.BrCetvorokrevetnih;
            this.BrDuplex = t.BrDuplex;

            //foreach (ProdajeSe odProdajeSe in t.ProdajeSeOdeljenja)
            //{
            //    this.Odeljenja.Add(new OdeljenjeView(odProdajeSe.ProdajeOdeljenje));
            //}
        }

        public TerminiView()
        {

        }
    }
}
