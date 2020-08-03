//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Agencija_4C.Entiteti;
//using FluentNHibernate.Mapping;

//namespace Agencija_4C.Mapiranja
//{
//    public class NalogMapiranja : ClassMap<Nalog>
//    {
//        public NalogMapiranja()
//        {
//            Table("nalog");

//            Id(x => x.Id, "idnalog").GeneratedBy.Increment();

//            Map(x => x.Password, "sifra");
//            Map(x => x.Username, "username");

//            //HasMany(x => x.NaloziKlijent).KeyColumn("idklijenta").LazyLoad().Cascade.All();
//            //HasMany(x => x.NaloziZaposleni).KeyColumn("idzaposlenog").LazyLoad();
//            References(x => x.NalogKlijent).Column("idklijenta");
//            References(x => x.NalogZaposleni).Column("idzaposlenog");
//        }
//    }
//}
