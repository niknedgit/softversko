using Agencija_4C.DataWrapper;
using Agencija_4C.DTOs;
using Agencija_4C.Entiteti;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Agencija_4C.Providers
{
    public class TerminiProvider
    {
        public IEnumerable<Termini> GetTermine()
        {
            ISession s = DataLayer.GetSession();

            IEnumerable<Termini> termini = s.Query<Termini>().Select(p => p);
            return termini;
        }

        public Termini GetTermini(int id)
        {
            ISession s = DataLayer.GetSession();

            return s.Query<Termini>()
                .Where(v => v.Id == id).Select(p => p).FirstOrDefault();
        }

        public List<Termini> ListaTermina()
        {
            ISession s = DataLayer.GetSession();

            List<Termini> termini = s.Query<Termini>()
                                                .Select(p => p).ToList();

            return termini;
        }

        public List<TerminiView> VratiT_ZaS(int id)  //bilo je List<Termini>
        {
            ISession s = DataLayer.GetSession();

            Smestaj sme = s.Query<Smestaj>()
                 .Where(v => v.Id == id).Select(p => p).FirstOrDefault();

            List<TerminiView> termini = s.Query<Termini>()
                 .Where(v => v.VezanZaSmestaj == sme).Select(p => new TerminiView(p)).ToList();

            return termini;
        }

        public bool FilterDatum(int id, DateTime min, DateTime max)
        {
            ISession s = DataLayer.GetSession();

            Smestaj sme = s.Query<Smestaj>()
                 .Where(v => v.Id == id).Select(p => p).FirstOrDefault();

            List<Termini> termini = s.Query<Termini>()
                 .Where(ter => ter.VezanZaSmestaj == sme)
                 .Where(ter => ter.Od >= min)
                 .Where(ter => ter.Od <= max)
                 .Where(ter => 
                    (ter.BrTrokrevetnih > 0 || ter.BrCetvorokrevetnih > 0 || ter.BrDuplex > 0))
                 .ToList();

            if (termini.Count > 0)
                return true;
            return false;
        }


        public bool Slobodan(Termini t, String soba, Klijent k)
        {
                ISession s = DataLayer.GetSession();

                Termini termin = GetTermini(t.Id);
                Smestaj sm = s.Query<Termini>()
                     .Where(v => v.Id == termin.Id)
                     .Select(p => p.VezanZaSmestaj).FirstOrDefault();

                KlijentProvider klijentProv = new KlijentProvider();

                Klijent klijent = s.Query<Klijent>()
                .Where(v => v.Username.Equals(k.Username))
                .Select(p => p).FirstOrDefault();
                RezervacijaProvider rezervacijaProv = new RezervacijaProvider();

                Rezervacija rezervacija = new Rezervacija();
                rezervacija.KlijentR = klijent;
                rezervacija.SmestajR = sm;
                rezervacija.TerminiR = t;
                rezervacija.Soba = soba;

                //postoji smestaj onda trebamo ispitati da li je termin slobodan
                if (soba.Equals("cetvorokrevetna"))
                {
                    //update termine
                    if (termin.BrCetvorokrevetnih > 0)
                    {
                        s.Query<Termini>()
                       .Where(v => v.Id == t.Id)
                       .UpdateBuilder()
                       .Set(c => c.BrCetvorokrevetnih, c => c.BrCetvorokrevetnih - 1)
                       .Update();

                    if (t.Od.Month - DateTime.Now.Month > 2 && t.Od.Month - DateTime.Now.Month < 6)
                        klijentProv.Mail(klijent, "Rezervisan je traženi smeštaj-" + sm.Naziv + ". Cena smeštaja iznosi " + sm.CenaPoKrevetu * 1.4*120 + "din. Ukoliko se odlučite za naš prevoz, cena po osobi je " + sm.CenaPrevoza * 120 + "din");
                    else
                        klijentProv.Mail(klijent, "Rezervisan je traženi smeštaj-" + sm.Naziv + ". Početna cena smeštaja iznosi " + sm.CenaPoKrevetu * 1.4 * 120 + "din dok je cena sa popustom "+ sm.CenaPoKrevetu * 1.4 * 120*0.8 + "din. Ukoliko se odlučite za nas prevoz, cena po osobi je " + sm.CenaPrevoza * 120 + "din");
                }
                    else
                    {
                        rezervacija.Cekanje = "da";
                    rezervacijaProv.AddRezervacija(rezervacija);
                    //smestimo klijenta u red
                    klijentProv.Mail(klijent, "Nažalost traženi smeštaj " + sm.Naziv + " je zauzet. Ukoliko se, oslobodi bićete obavešteni");
                    return false;

                }

                }
                else if (soba.Equals("trokrevetna"))
                {
                    if (termin.BrTrokrevetnih > 0)
                    {
                        s.Query<Termini>()
                     .Where(v => v.Id == t.Id)
                     .UpdateBuilder()
                     .Set(c => c.BrTrokrevetnih, c => c.BrTrokrevetnih - 1)
                     .Update();
                    if(t.Od.Month-DateTime.Now.Month>2 && t.Od.Month-DateTime.Now.Month<6)
                        klijentProv.Mail(klijent, "Rezervisan je traženi smeštaj-" + sm.Naziv + ". Cena smeštaja iznosi " + sm.CenaPoKrevetu * 1.3 *120+ "din. Ukoliko se odlučite za naš prevoz, cena po osobi je " + sm.CenaPrevoza * 120 + "din");
                    else
                        klijentProv.Mail(klijent, "Rezervisan je traženi smeštaj-" + sm.Naziv + ". Cena smeštaja iznosi " + sm.CenaPoKrevetu * 1.3 * 120 + "din dok je cena sa popustom " + sm.CenaPoKrevetu * 1.3 * 120 * 0.8 + "din. Ukoliko se odlučite za naš prevoz, cena po osobi je " + sm.CenaPrevoza * 120 + "din");
                }
                    else
                    {
                        rezervacija.Cekanje = "da";
                        klijentProv.Mail(klijent, "Nažalost traženi smeštaj " + sm.Naziv + " je zauzet. Ukoliko se oslobodi, bićete obavešteni");
                    rezervacijaProv.AddRezervacija(rezervacija);
                    return false;
                }

                }
                else if (soba.Equals("duplex"))
                {
                    if (termin.BrDuplex > 0)
                    {
                        s.Query<Termini>()
                     .Where(v => v.Id == t.Id)
                     .UpdateBuilder()
                     .Set(c => c.BrDuplex, c => c.BrDuplex - 1)
                     .Update();

                    if (t.Od.Month - DateTime.Now.Month > 2 && t.Od.Month - DateTime.Now.Month < 6)
                        klijentProv.Mail(klijent, "Rezervisan je trazeni smestaj-" + sm.Naziv + ". Cena smestaja iznosi " + sm.CenaPoKrevetu * 1.5*120 + "din. Ukoliko se odlucite za nas prevoz cena po osobi je " + sm.CenaPrevoza * 120 + "din");
                    else
                        klijentProv.Mail(klijent, "Rezervisan je trazeni smestaj-" + sm.Naziv + ". Pocetna cena smestaja iznosi " + sm.CenaPoKrevetu * 1.5 * 120 + "din dok je cena sa popustom" + sm.CenaPoKrevetu * 1.5 * 120*0.8 + "din . Ukoliko se odlucite za nas prevoz cena po osobi je " + sm.CenaPrevoza * 120 + "din");
                }
                    else
                    {
                        rezervacija.Cekanje = "da";
                        klijentProv.Mail(klijent, "Nazalost trazeni smestaj " + sm.Naziv + " je zauzet. Ukoliko se oslobodi bicete obavesteni");
                    rezervacijaProv.AddRezervacija(rezervacija);
                    return false;
                }
                }
               
                rezervacijaProv.AddRezervacija(rezervacija);

                return true;          
            
        }

        public bool Otkazi(Termini t,String soba, Klijent k)
        {
            ISession s = DataLayer.GetSession();

            Klijent klijent = s.Query<Klijent>()
               .Where(v => v.Username.Equals(k.Username))
               .Select(p => p).FirstOrDefault();

            Termini termin = GetTermini(t.Id);

            Smestaj sm = s.Query<Termini>()
          .Where(v => v.Id == termin.Id)
          .Select(p => p.VezanZaSmestaj).FirstOrDefault();

            Rezervacija rez = s.Query<Rezervacija>()
          .Where(v => v.KlijentR.Id == klijent.Id && v.SmestajR == sm)
          .Select(p => p).FirstOrDefault();

            if (sm == null || rez == null)
            {
                return false; // greska
            }

            RezervacijaProvider rezervacijaProv = new RezervacijaProvider();
            KlijentProvider klijentProv = new KlijentProvider();
            klijentProv.Mail(klijent, "Uspesno otkazana rezervacija letevonja-" + sm.Naziv);
            if (soba.Equals("cetvorokrevetna"))
            {
                s.Query<Termini>()
                    .Where(v => v.Id == t.Id)
                    .UpdateBuilder()
                    .Set(c => c.BrCetvorokrevetnih, c => c.BrCetvorokrevetnih + 1)
                    .Update();
            }
            else if (soba.Equals("trokrevetna"))
            {
                s.Query<Termini>()
                    .Where(v => v.Id == t.Id)
                    .UpdateBuilder()
                    .Set(c => c.BrTrokrevetnih, c => c.BrTrokrevetnih + 1)
                    .Update();
            }
            else if (soba.Equals("duplex"))
            {
                s.Query<Termini>()
                    .Where(v => v.Id == t.Id)
                    .UpdateBuilder()
                    .Set(c => c.BrDuplex, c => c.BrDuplex + 1)
                    .Update();
            }
            else
            {
                return false;
            }

            //ObrisiRezervaciju 

            RezervacijaProvider r = new RezervacijaProvider();

            r.RemoveRezervacija(rez.Id);
            PosaljiPonuduKlijentima(sm, soba, sm.Naziv);
            return true;
        }

        public void PosaljiPonuduKlijentima(Smestaj sm, String soba, String naziv)
        {
            ISession s = DataLayer.GetSession();
            //proveriti
            KlijentProvider klijentProv = new KlijentProvider();
            IList<Klijent> klijenti= s.Query<Rezervacija>()
                                              .Where(p => (p.SmestajR.Id == sm.Id && p.Soba.Equals(soba) && p.Cekanje.Equals("da")))
                                           //   .OrderBy(p => p.Datum) //ne mora
                                          //    .ThenBy(p => p.KlijentR.PunoIme)
                                              .Select(p => p.KlijentR).ToList();

            foreach (Klijent k in klijenti)
            {
                klijentProv.Mail(k, "Ponovo je slobodan trazeni smestaj " + naziv + " za " + soba+" sobu "); 
            }
            
        }

        public bool AddTermini(DataWrapper.AddTermini term)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Termini t = new Termini();
                t.Od = term.Od;
                t.Do = term.Do;
                t.BrTrokrevetnih = term.BrTrokrevetnih;
                t.BrCetvorokrevetnih = term.BrCetvorokrevetnih;
                t.BrDuplex = term.BrDuplex;

                Smestaj sm = s.Query<Smestaj>()
                 .Where(v => v.Id == term.IdHotela).Select(p => p).FirstOrDefault();
                t.VezanZaSmestaj = sm;

                s.Save(t);

                s.Flush();
                s.Close();

                return true;
            }
            catch (Exception ec)
            {
                return false;
            }
        }

        public int RemoveTermini(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Termini t = s.Load<Termini>(id);

                s.Delete(t);

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
