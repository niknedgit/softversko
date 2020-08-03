using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agencija_4C.Entiteti;
using FluentNHibernate.Mapping;

namespace Agencija_4C.Mapiranja
{
    public class TerminiMapiranja : ClassMap<Termini>
    {
        public TerminiMapiranja()
        {
            Table("termini");

            Id(x => x.Id, "idtermini").GeneratedBy.Increment();

            Map(x => x.BrCetvorokrevetnih, "brCetvorokrevetnih");
            Map(x => x.BrDuplex, "brDuplex");
            Map(x => x.BrTrokrevetnih, "brTrokrevetnih");
            Map(x => x.Od, "od");
            Map(x => x.Do, "do");

            References(x => x.VezanZaSmestaj).Column("idsmestajaTermini").LazyLoad();
        }
    }
}
