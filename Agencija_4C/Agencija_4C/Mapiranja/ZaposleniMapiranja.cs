using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Agencija_4C.Entiteti;

namespace Agencija_4C.Mapiranja
{
    public class ZaposleniMapiranja : ClassMap<Zaposleni>
    {
        public ZaposleniMapiranja()
        {
            Table("zaposleni");

            Id(x => x.Id, "idzaposleni").GeneratedBy.Increment();

            Map(x => x.Ime, "ime");
            Map(x => x.Password, "sifra");
            Map(x => x.Username, "username");
            Map(x => x.Mail, "mail");

            //   HasMany(x => x.Nalozi).KeyColumn("idzaposlenog").LazyLoad().Cascade.All().Inverse();
        }
    }
}
