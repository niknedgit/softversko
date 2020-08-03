using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agencija_4C.DataWrapper;
using Agencija_4C.DTOs;
using Agencija_4C.Entiteti;
using Agencija_4C.Providers;
using NHibernate;
using NHibernate.Linq;

namespace Agencija_4C
{
    public class DestinacijaProvider
    {
        public IEnumerable<DestinacijaView> GetDestinacije()
        {
            ISession s = DataLayer.GetSession();

            IEnumerable<DestinacijaView> destinacije = s.Query<Destinacija>()
                                                .Select(p => new DestinacijaView(p));
            return destinacije;
        }

        public List<DestinacijaView> ListaDestinacija()
        {
            ISession s = DataLayer.GetSession();

            List<DestinacijaView> destinacije = s.Query<Destinacija>()
                                                .Select(p => new DestinacijaView(p)).ToList();
            
            return destinacije;
        }

        public DestinacijaView GetDestinacija(int id)
        {
            ISession s = DataLayer.GetSession();

            return s.Query<Destinacija>()
                .Where(v => v.Id == id).Select(d => new DestinacijaView(d)).FirstOrDefault();
            //  .Select(p => p).FirstOrDefault();
        }

        public bool AddDestinacija(AddDest dest)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Destinacija d = new Destinacija();
                d.Naziv = dest.Naziv;
                d.Opis = dest.Opis;

                s.Save(d);

                foreach (String str in dest.ImaSlike)
                {
                    Slike sl = new Slike();
                    sl.UrlSlike = str;
                    sl.DestinacijaS = d;

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

        public int RemoveDestinacija(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                //Destinacija d = s.Load<Destinacija>(id);
                Destinacija d = s.Query<Destinacija>().Where(v => v.Id == id).Select(p => p).FirstOrDefault();

                s.Delete(d);
                s.Flush();
                s.Close();

                ISession s2 = DataLayer.GetSession();

                SmestajProvider sp = new SmestajProvider();
                List<Smestaj> smestaji = sp.VratiSmestaje(id);
                foreach (Smestaj sm in smestaji)
                    s2.Delete(sm);

                s2.Query<Slike>()
                .Where(v => v.DestinacijaS == d).Delete();

                s2.Flush();
                s2.Close();

                return 1;
            }
            catch (Exception exc)
            {
                return -1;
            }

        }

        public bool PutDestinacija(PutDest dest)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Destinacija d = s.Query<Destinacija>().Where(v => v.Id == dest.Id).Select(p => p).FirstOrDefault();
                d.Naziv = dest.Naziv;
                d.Opis = dest.Opis;
                s.Save(d);

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
