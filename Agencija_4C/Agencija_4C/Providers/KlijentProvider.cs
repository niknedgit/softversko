using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agencija_4C.Entiteti;
using NHibernate;
using NHibernate.Linq;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace Agencija_4C.Providers
{
    public class KlijentProvider
    {
        public IEnumerable<DTOs.KlijentView> GetKlijentiView()
        {
            ISession s = DataLayer.GetSession();

            IEnumerable<DTOs.KlijentView> klijenti = s.Query<Klijent>()
                .Select(p => new DTOs.KlijentView(p));
            return klijenti;
        }

        public List<Klijent> GetKlijenti()
        {
            ISession s = DataLayer.GetSession();

            List<Klijent> klijenti = s.Query<Klijent>()
                .Select(p => p).ToList();
            return klijenti;
        }

        public Klijent GetKlijent(int id)
        {
            ISession s = DataLayer.GetSession();

            return s.Query<Klijent>()
                .Where(v => v.Id == id).Select(p => p).FirstOrDefault();
        }

        public Klijent GetKlijentByUsername(String username)
        {
            ISession s = DataLayer.GetSession();

            return s.Query<Klijent>()
                .Where(v => v.Username == username).Select(p => p).FirstOrDefault();
        }

        public bool Postoji(String pass, String username)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Klijent postoji= s.Query<Klijent>()
                 .Where(v => v.Username.Equals(username) && v.Password.Equals(pass))
                 .Select(p => p).FirstOrDefault();

                if (postoji!=null)
                      return true; // sve tacno

                return false; //nista se ne poklapa
            }
            catch (Exception ec)
            {
                return false ;
            }
        }

        public int AddKlijent(Klijent k)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Zaposleni zap = s.Query<Zaposleni>()
                 .Where(v => v.Username == k.Username ).Select(p => p).FirstOrDefault();

                Klijent postoji= s.Query<Klijent>()
                 .Where(v => v.Username == k.Username).Select(p => p).FirstOrDefault();

                if (postoji == null && zap==null)
                {
                    s.Save(k);
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
        
       public bool Zaposleni(String username, String password)
        {
            ISession s = DataLayer.GetSession();
            Zaposleni zap = s.Query<Zaposleni>()
                .Where(v => v.Username == username && v.Password==password).Select(p => p).FirstOrDefault();
            if (zap != null)
            {
                return true;
            }
            //Klijent postoji = s.Query<Klijent>()
            // .Where(v => v.Username == username).Select(p => p).FirstOrDefault();

            return false;
        }

        public void Mail(Klijent k, String poruka)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Turisticka agencija", "vamosalaplayaagencija@gmail.com"));
            message.To.Add(new MailboxAddress(k.PunoIme, k.Mail));
            message.Subject = "Ponuda";

            message.Body = new TextPart("plain")
            {
                Text = "Postovani,\n" + poruka + " \n Pozdrav"
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("vamosalaplayaagencija", "4CVamosALaPlaya");
                client.Send(message);
                client.Disconnect(true);
            }

        }

        public int RemoveKlijent(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                
                Klijent k = s.Query<Klijent>().Where(v => v.Id == id).Select(p => p).FirstOrDefault();

                s.Delete(k);
                s.Flush();
                s.Close();

                ISession s2 = DataLayer.GetSession();

                s2.Query<Rezervacija>()
                .Where(v => v.KlijentR == k).Delete();

                s2.Flush();
                s2.Close();

                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
