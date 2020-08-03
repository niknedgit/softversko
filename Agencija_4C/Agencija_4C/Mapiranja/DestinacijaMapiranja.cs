using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agencija_4C.Entiteti;
using FluentNHibernate.Mapping;

namespace Agencija_4C.Mapiranja
{
    public class DestinacijaMapiranja : ClassMap<Destinacija>
    {
        public DestinacijaMapiranja()
        {
            Table("destinacija");
            
            Id(x => x.Id, "iddestinacija").GeneratedBy.Increment();

            Map(x => x.Naziv, "naziv");
            Map(x => x.Opis, "opis");

            HasMany(x => x.ImaSlike).KeyColumn("idDestinacije").LazyLoad().Cascade.All().Inverse();
            HasMany(x => x.ImaSmestaje).KeyColumn("idDestinacijeS").LazyLoad().Cascade.All().Inverse();
        }
    }
}
