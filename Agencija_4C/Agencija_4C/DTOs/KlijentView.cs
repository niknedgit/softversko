using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencija_4C.DTOs
{
    public class KlijentView
    {
        public int Id { get; set; }
        public String PunoIme { get; set; }
        public String Mail { get; set; }
        public String Password { get; set; }
        public String Username { get; set; }

        public KlijentView(Entiteti.Klijent k)
        {
            this.Id = k.Id;
            this.PunoIme = k.PunoIme;
            this.Mail = k.Mail;
            this.Password = k.Password;
            this.Username = k.Username;
        }
    }
}
