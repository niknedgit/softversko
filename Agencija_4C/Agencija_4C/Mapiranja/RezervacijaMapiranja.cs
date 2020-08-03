using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agencija_4C.Entiteti;
using FluentNHibernate.Mapping;

namespace Agencija_4C.Mapiranja
{
    public class RezervacijaMapiranja : ClassMap<Rezervacija>
    {
        public RezervacijaMapiranja()
        {
            Table("rezervacija");
            Id(x => x.Id, "idrezervacija").GeneratedBy.Increment();

            Map(x => x.Cekanje, "cekanje");
            Map(x => x.Soba, "soba");

            References(x => x.TerminiR).Column("idterminiRezervacija");
            References(x => x.KlijentR).Column("idklijentaRezervacija");
            References(x => x.SmestajR).Column("idsmestajaRezervacija");
        }
    }
}
