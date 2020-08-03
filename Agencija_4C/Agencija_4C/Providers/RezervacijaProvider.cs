using Agencija_4C.DataWrapper;
using Agencija_4C.Entiteti;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencija_4C.Providers
{
    public class RezervacijaProvider
    {
        public IEnumerable<Rezervacija> GetRezervacije()
        {
            ISession s = DataLayer.GetSession();

            IEnumerable<Rezervacija> rezervacije = s.Query<Rezervacija>().Select(p => p);
            return rezervacije;
        }

        public Rezervacija GetRezervacija(int id)
        {
            ISession s = DataLayer.GetSession();

            return s.Query<Rezervacija>()
                .Where(v => v.Id.Equals(id)).Select(p => p).FirstOrDefault();
        }

        public void ZainteresovanZaSmestaj(Klijent k, int idSmestaja)
        {

            ISession s = DataLayer.GetSession();
            //stavimo datum ako klijent nije dobio trazeni smestaj

            Smestaj sm = s.Query<Smestaj>()
                .Where(v => v.Id == idSmestaja)
                .Select(p => p).FirstOrDefault();

            s.Query<Rezervacija>()
           .Where(v => v.KlijentR == k)
           .UpdateBuilder()
           .Set(c => c.Cekanje, c => "da")
           .Set(c => c.SmestajR, c => sm)
           .Update();

        }
        public List<SveRezervacije> vratiSveRezervacije(Klijent k)
        {
            ISession s = DataLayer.GetSession();
            List<SveRezervacije> sveRezervacije = new List<SveRezervacije>();
            SveRezervacije jednaR = new SveRezervacije();

            int id = s.Query<Klijent>()
                .Where(v => v.Password.Equals(k.Password) && v.Username.Equals(k.Username))
                .Select(p => p.Id).FirstOrDefault();

            List<Rezervacija> rezervacije = s.Query<Rezervacija>()
                .Where(v => v.KlijentR.Id == id)
                .Select(p => p).ToList();

            foreach (Rezervacija r in rezervacije)
            {
                jednaR.SmestajId = r.SmestajR.Id;
                jednaR.Soba = r.Soba;
                jednaR.TerminId = r.TerminiR.Id;
                sveRezervacije.Add(jednaR);
            }

            return sveRezervacije;
        }

        public List<DataWrapper.RezZaposlenom> VratiRezervacijeZaposlenom(Klijent k)
        {
            ISession s = DataLayer.GetSession();
            List<DataWrapper.RezZaposlenom> sveRezervacije = new List<DataWrapper.RezZaposlenom>();
            SmestajProvider sp = new SmestajProvider();

            List<Rezervacija> rezervacije = s.Query<Rezervacija>()
                .Where(v => v.KlijentR.Id == k.Id)
                .Select(p => p).ToList();

            foreach (Rezervacija r in rezervacije)
            {
                DataWrapper.RezZaposlenom rezZ = new DataWrapper.RezZaposlenom();

                rezZ.Smestaj = r.SmestajR.Naziv;
                rezZ.IdSmestaja = r.SmestajR.Id;
                rezZ.Soba = r.Soba;
                rezZ.Termin = r.TerminiR.Od.ToString("dd/M/yyyy") + " - " + r.TerminiR.Do.ToString("dd/M/yyyy");

                rezZ.Cena = sp.CenaSobe(r.SmestajR, r.Soba);

                sveRezervacije.Add(rezZ);
            }

            return sveRezervacije;
        }

        public int AddRezervacija(Rezervacija r)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                s.Save(r);

                s.Flush();
                s.Close();

                return 1;
            }
            catch (Exception ec)
            {
                return -1;
            }
        }

        public int RemoveRezervacija(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Rezervacija r = s.Load<Rezervacija>(id);

                s.Delete(r);

                s.Flush();
                s.Close();

                return 1;
            }
            catch (Exception exc)
            {
                return -1;
            }

        }
    }
}
