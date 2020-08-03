using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agencija_4C.Entiteti;
using FluentNHibernate.Mapping;

namespace Agencija_4C.Mapiranja
{
    public class SmestajMapiranja : ClassMap<Smestaj>
    {
        public SmestajMapiranja()
        {
            Table("smestaj");

            Id(x => x.Id, "idsmestaj").GeneratedBy.Increment();

            Map(x => x.CenaPoKrevetu, "cenaKreveta");
            Map(x => x.Opis, "opis");
            Map(x => x.Naziv, "naziv");
            Map(x => x.CenaPrevoza, "cenaPrevoza");
            Map(x => x.BrGlasova, "brGlasova");
            Map(x => x.Ocena, "ocena");

            References(x => x.PripadaDestinaciji).Column("idDestinacijeS");

            HasManyToMany(x => x.Klijenti)
                .Table("rezervacija")
                .ParentKeyColumn("idsmestajaRezervacija")
                .ChildKeyColumn("idklijentaRezervacija")
                .Inverse()
                .Cascade.All();
            HasMany(x => x.RezervacijeS).KeyColumn("idsmestajaRezervacija").LazyLoad().Cascade.All().Inverse();

            HasMany(x => x.SlikeSmestaja).KeyColumn("idSmestaja").LazyLoad().Cascade.All().Inverse();

            HasMany(x => x.ImaTermine).KeyColumn("idsmestajaTermini").LazyLoad().Cascade.All().Inverse();
        }
    }
}
