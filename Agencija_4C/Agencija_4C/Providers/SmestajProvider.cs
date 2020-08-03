using Agencija_4C.DataWrapper;
using Agencija_4C.DTOs;
using Agencija_4C.Entiteti;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Agencija_4C.Providers
{
    public class SmestajProvider
    {
        public IEnumerable<Smestaj> GetSmestaji()
        {
            ISession s = DataLayer.GetSession();

            IEnumerable<Smestaj> smestaji = s.Query<Smestaj>()
                                                .Select(p => p);
            return smestaji;
        }

        public IEnumerable<SmestajView> GetSmestaje()
        {
            ISession s = DataLayer.GetSession();

            IEnumerable<SmestajView> smestaj = s.Query<Smestaj>()
                                                .Select(p => new SmestajView(p));
            return smestaj;
        }

        public List<Smestaj> ListaSmestaja()
        {
            ISession s = DataLayer.GetSession();

            List<Smestaj> smestaji = s.Query<Smestaj>()
                                                .Select(p => p).ToList();
            return smestaji;
        }

        public SmestajView GetSmestaj(int id)
        {
            ISession s = DataLayer.GetSession();

            return s.Query<Smestaj>()
                .Where(v => v.Id == id).Select(p => new SmestajView(p)).FirstOrDefault();
        }

        public List<SmestajView> LastMinute()
        {
            ISession s = DataLayer.GetSession();

            //sviSmestaji ima duplikate smestaja 

            List<Smestaj> sviSmestaji = s.Query<Termini>()
                                       .Where(v => v.Od.Month == DateTime.Now.Month|| v.Od.Month == DateTime.Now.Month+1)
                                 //      .Where(v => DateTime.Now.Day - v.Od.Day > 1)
                                       .Where(v => v.BrDuplex > 0 || v.BrTrokrevetnih>0 ||v.BrCetvorokrevetnih>0)
                                       .Select(p => p.VezanZaSmestaj).OrderByDescending(n => n.Id).ToList();

            if (sviSmestaji.Count != 0)
            {
                int pok;
                List<SmestajView> smestaji = new List<SmestajView>();
                for (int i = 0; i < sviSmestaji.Count; i++)
                {
                    pok = 1; //koliko puta se pojavio do sada
                    for (int j = i + 1; j < sviSmestaji.Count && sviSmestaji[i].Id == sviSmestaji[j].Id; j++)
                        pok++;
                    SmestajView smestaj = new SmestajView(sviSmestaji[i]);

                    smestaji.Add(smestaj);
                    i += pok - 1;
                }
                return smestaji;
            }
            return null;
        }
        public List<SmestajView> FirstMinute()
        {
            ISession s = DataLayer.GetSession();
            //sviSmestaji ima duplikate smestaja 

            List<Smestaj> sviSmestaji = s.Query<Termini>()
                                       .Where(v =>  v.Od.Month- DateTime.Now.Month >=6 || v.Od.Year == DateTime.Now.Year + 1)
                                       .Where(v => v.BrDuplex > 0 || v.BrTrokrevetnih > 0 || v.BrCetvorokrevetnih > 0)
                                       .Select(p => p.VezanZaSmestaj).OrderByDescending(n => n.Id).ToList();
            if (sviSmestaji.Count!= 0)
            {
                int pok;
                List<SmestajView> smestaji = new List<SmestajView>();
                for (int i = 0; i < sviSmestaji.Count; i++)
                {
                    pok = 1; //koliko puta se pojavio do sada
                    for (int j = i + 1; j < sviSmestaji.Count && sviSmestaji[i].Id == sviSmestaji[j].Id; j++)
                        pok++;
                    SmestajView smestaj = new SmestajView(sviSmestaji[i]);

                    smestaji.Add(smestaj);
                    i += pok - 1;
                }
                return smestaji;
            }
            return null;
           
        }

        public List<Statistika> vratiStatistiku()
        {
            ISession s = DataLayer.GetSession();
            List<Smestaj> smestajiId = new List<Smestaj>();
            List<Statistika> statistika = new List<Statistika>();
           
            smestajiId = s.Query<Rezervacija>()
              .Select(p => p.SmestajR).OrderByDescending(n => n.Id).ToList();

            int pok = 0;
            for (int i=0; i<smestajiId.Count; i++)
            {
                pok = 1;
                for (int j=i+1 ; j<smestajiId.Count && smestajiId[i].Id==smestajiId[j].Id; j++)
                    pok++;

                Statistika stat = new Statistika();
                stat.BrRezervaacija = pok;
                stat.NazivSmestaja = s.Query<Smestaj>()
                     .Where(v => v.Id == smestajiId[i].Id)
                      .Select(p => p.Naziv).FirstOrDefault();
                statistika.Add(stat);
                i += pok - 1;
            }
            return statistika;
        }
        
        public int Glasaj(int idSmestaj, int ocena)
        {
            ISession s = DataLayer.GetSession();

            s.Query<Smestaj>()
                   .Where(v => v.Id == idSmestaj)
                   .UpdateBuilder()
                   .Set(c => c.Ocena, c =>( c.Ocena*c.BrGlasova + ocena)/(c.BrGlasova+1))
                   .Set(c => c.BrGlasova, c => c.BrGlasova + 1)                   
                   .Update();

            return s.Query<Smestaj>()
                    .Where(v => v.Id ==idSmestaj)
                      .Select(p => p.Ocena).FirstOrDefault();

        }

        public List<SmestajView> VratiS(int id)
        {
            ISession s = DataLayer.GetSession();
            
            Destinacija d = s.Query<Destinacija>()
                 .Where(v => v.Id == id).Select(p => p).FirstOrDefault();

            List<SmestajView> smestaji = s.Query<Smestaj>()
                 .Where(v => v.PripadaDestinaciji == d).Select(p => new SmestajView(p)).ToList(); 
               
            return smestaji;
        }

        public List<Smestaj> VratiSmestaje(int id)
        {
            ISession s = DataLayer.GetSession();

            Destinacija d = s.Query<Destinacija>()
                 .Where(v => v.Id == id).Select(p => p).FirstOrDefault();

            List<Smestaj> smestaji = s.Query<Smestaj>()
                 .Where(v => v.PripadaDestinaciji == d).Select(p => p).ToList();

            return smestaji;
        }

        public List<SmestajView> FilterCena(int id, int min, int max)
        {
            ISession s = DataLayer.GetSession();

            Destinacija d = s.Query<Destinacija>()
                 .Where(v => v.Id == id).Select(p => p).FirstOrDefault();

            List<SmestajView> smestaji = s.Query<Smestaj>()
                 .Where(sm => sm.PripadaDestinaciji == d)
                 .Where(sm => sm.CenaPoKrevetu >= min)
                 .Where(sm => sm.CenaPoKrevetu <= max).Select(p => new SmestajView(p)).ToList();

            return smestaji;
        }

        public bool AddSmestaj(DataWrapper.AddHotel hotel)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Smestaj sm = new Smestaj();
                sm.Naziv = hotel.Naziv;
                sm.Opis = hotel.Opis;
                sm.CenaPoKrevetu = hotel.CenaPoKrevetu;
                sm.CenaPrevoza = hotel.CenaPrevoza;

                Destinacija d = s.Query<Destinacija>()
                 .Where(v => v.Id == hotel.IdDestinacije).Select(p => p).FirstOrDefault();
                sm.PripadaDestinaciji = d;

                s.Save(sm);

                foreach (String str in hotel.SlikeSmestaja)
                {
                    Slike sl = new Slike();
                    sl.UrlSlike = str;
                    sl.SmestajS = sm;

                    s.Save(sl);
                }

                s.Flush();
                s.Close();

                return true;
            }
            catch (Exception ec)
            {
                return false;
            }
        }

        public double CenaSobe(Smestaj sm, String tip )
        {
            ISession s = DataLayer.GetSession();
            int cena= s.Query<Smestaj>()
                 .Where(v => v.Id == sm.Id).Select(p => p.CenaPoKrevetu).FirstOrDefault();
            if (tip.Equals("trokrevetna"))
                return cena * 1.3;
            if(tip.Equals("cetvorokrevetna"))
                return cena * 1.4;
           if( tip.Equals("duplex"))
                return cena * 1.5;
            return 0;
        }

        public int RemoveSmestaj(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                
                Smestaj sm = s.Query<Smestaj>().Where(v => v.Id == id).Select(p => p).FirstOrDefault();

                s.Delete(sm);
                s.Flush();
                s.Close();

                ISession s2 = DataLayer.GetSession();

                s2.Query<Slike>()
                .Where(v => v.SmestajS == sm).Delete();

                s2.Flush();
                s2.Close();

                return 1;
            }
            catch (Exception exc)
            {
                return -1;
            }

        }

        public bool PutSmestaj(DataWrapper.PutSmestaj smestaj)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Smestaj sm = s.Query<Smestaj>().Where(v => v.Id == smestaj.Id).Select(p => p).FirstOrDefault();
                sm.Naziv = smestaj.Naziv;
                sm.Opis = smestaj.Opis;
                sm.CenaPoKrevetu = smestaj.CenaPoKrevetu;
                sm.CenaPrevoza = smestaj.CenaPrevoza;

                s.Save(sm);

                s.Flush();
                s.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
