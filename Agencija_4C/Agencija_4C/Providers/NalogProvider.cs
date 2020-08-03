//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Agencija_4C.Entiteti;
//using NHibernate;
//using NHibernate.Linq;

//namespace Agencija_4C.Providers
//{
//    public class NalogProvider
//    {
//        public IEnumerable<Nalog> GetNalozi()
//        {
//            ISession s = DataLayer.GetSession();

//            IEnumerable<Nalog> nalozi = s.Query<Nalog>()
//                                                //.Where(p => (p.Tip == "LUTKE" || p.Tip == "DODACI ZA LUTKE"))
//                                                //.OrderBy(p => p.Tip).ThenBy(p => p.Naziv.Length)
//                                                .Select(p => p);
//            return nalozi;
//        }

//        public Nalog GetNalog(int id)
//        {
//            ISession s = DataLayer.GetSession();

//            return s.Query<Nalog>()
//                .Where(v => v.Id == id).Select(p => p).FirstOrDefault();
//        }

//        /*public VojnikView GetVojnikView(int barkod)
//        {
//            ISession s = DataLayer.GetSession();

//            Vojnik voj = s.Query<Vojnik>()
//                .Where(v => v.BarKod == barkod).Select(p => p).FirstOrDefault();

//            if (voj == null) return new VojnikView();

//            return new VojnikView(voj);
//        }*/

//        public int AddNalog(Nalog n)
//        {
//            try
//            {
//                ISession s = DataLayer.GetSession();

//                s.Save(n);

//                s.Flush();
//                s.Close();

//                return 1;
//            }
//            catch (Exception ec)
//            {
//                return -1;
//            }
//        }

//        public int RemoveNalog(int id)
//        {
//            try
//            {
//                ISession s = DataLayer.GetSession();

//                Nalog n = s.Load<Nalog>(id);

//                s.Delete(n);

//                s.Flush();
//                s.Close();

//                return 1;
//            }
//            catch (Exception exc)
//            {
//                return -1;
//            }

//        }
//    }
//}
