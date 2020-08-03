using Agencija_4C.Entiteti;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencija_4C.Providers
{
    public class ZaposleniProvider
    {
        public IEnumerable<Zaposleni> GetZaposlene()
        {
            ISession s = DataLayer.GetSession();

            IEnumerable<Zaposleni> zaposleni = s.Query<Zaposleni>()
                                                //.Where(p => (p.Tip == "LUTKE" || p.Tip == "DODACI ZA LUTKE"))
                                                //.OrderBy(p => p.Tip).ThenBy(p => p.Naziv.Length)
                                                .Select(p => p);
            return zaposleni;
        }

        public Zaposleni GetZaposleni(int id)
        {
            ISession s = DataLayer.GetSession();

            return s.Query<Zaposleni>()
                .Where(v => v.Id == id).Select(p => p).FirstOrDefault();
        }

        /*public VojnikView GetVojnikView(int barkod)
        {
            ISession s = DataLayer.GetSession();

            Vojnik voj = s.Query<Vojnik>()
                .Where(v => v.BarKod == barkod).Select(p => p).FirstOrDefault();

            if (voj == null) return new VojnikView();

            return new VojnikView(voj);
        }*/

        public int AddZaposleni(Zaposleni z)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Zaposleni postoji = s.Query<Zaposleni>()
                .Where(v => v.Username == z.Username).Select(p => p).FirstOrDefault();

                if (postoji == null)
                {
                    s.Save(z);
                    s.Flush();
                }
                else
                {
                    return -1;// vec postoji
                }
                s.Close();
                return 1;
            }
            catch (Exception ec)
            {
                return -1;
            }
        }

        public int RemoveZaposleni(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Zaposleni z = s.Load<Zaposleni>(id);

                s.Delete(z);

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
