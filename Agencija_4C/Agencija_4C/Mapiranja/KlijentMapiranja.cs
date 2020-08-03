using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agencija_4C.Entiteti;
using FluentNHibernate.Mapping;

namespace Agencija_4C.Mapiranja
{
    public class KlijentMapiranja : ClassMap<Klijent>
    {
        public KlijentMapiranja()
        {
            Table("klijent");

            Id(x => x.Id, "idklijent").GeneratedBy.Increment();

     //       Map(x => x.Datum, "datum");
            Map(x => x.PunoIme, "puno_ime");
            Map(x => x.Mail, "mail");
            Map(x => x.Password, "sifra");
            Map(x => x.Username, "username");
           
            HasManyToMany(x => x.Smestaji)
                .Table("rezervacija")
                .ParentKeyColumn("idklijentaRezervacija")
                .ChildKeyColumn("idsmestajaRezervacija")
                .Cascade.All();
           HasMany(x => x.RezervacijeK).KeyColumn("idklijentaRezervacija").LazyLoad().Cascade.All().Inverse();
         //   HasMany(x => x.Nalozi).KeyColumn("idklijenta").LazyLoad().Cascade.All().Inverse();
        }
    }
}
