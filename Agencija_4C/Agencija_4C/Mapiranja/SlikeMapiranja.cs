using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Agencija_4C.Entiteti;


namespace Agencija_4C.Mapiranja
{
    public class SlikeMapiranja : ClassMap<Slike>
    {
        public SlikeMapiranja()
        {

            Table("slike");

            Id(x => x.Id, "idslike").GeneratedBy.Increment();

            Map(x => x.UrlSlike, "urlSlike");

            References(x => x.DestinacijaS).Column("idDestinacije");
            References(x => x.SmestajS).Column("idSmestaja");
        }
    }
}
