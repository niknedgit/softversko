using System;
using Agencija_4C.DataWrapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agencija_4C.Entiteti;
using Agencija_4C.Providers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Newtonsoft.Json.Linq;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Agencija_4C.Controllers
{
    [Route("api/[controller]")]
    public class KlijentController : Controller
    {
        //GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            KlijentProvider provider = new KlijentProvider();

            IEnumerable<DTOs.KlijentView> klijenti = provider.GetKlijentiView();
            if (klijenti == null)
                return NotFound();
            var json = JsonConvert.SerializeObject(klijenti, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            return Ok(json);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            KlijentProvider provider = new KlijentProvider();
            //  var json = JsonConvert.SerializeObject(provider.GetKlijent(id));
            Klijent k = provider.GetKlijent(id);
            if (k == null)
                return NotFound();
            var json = JsonConvert.SerializeObject(k, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            return Ok(json);
        }
        [HttpPost]
        [Route("vratiSveRezervacije")]
        public IActionResult vratiSveRezervacije([FromBody]Klijent klijent)
        {
            KlijentProvider provider = new KlijentProvider();
            List<SveRezervacije> JsonObj = new List<SveRezervacije>();
            RezervacijaProvider rez = new RezervacijaProvider();
            if (provider.Postoji(klijent.Password, klijent.Username))
            {
                JsonObj = rez.vratiSveRezervacije(klijent);
            }
            var json = JsonConvert.SerializeObject(JsonObj, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            return Ok(json);
        }

        [HttpOptions]
        [Route("vratiSveRezervacije")]
        public IActionResult vratiSveRezervacije()
        {
            return Ok();
        }

        [HttpPost]
        [Route("LogIn")]
        public IActionResult Prijavljivanje([FromBody]Prijava prijava)
        {
            KlijentProvider provider = new KlijentProvider();

            if (provider.Zaposleni(prijava.Username, prijava.Password))
            {
                var tip = new { tip = "Zaposleni" };
                return Ok(tip);
            }

            if (provider.Postoji(prijava.Password, prijava.Username))
            {
                var tip = new { tip = "Klijent" };
                return Ok(tip);
            }
            return NotFound();
        }
        [HttpPost]
        [Route("registrujSe")]
        public IActionResult RegistrujSe([FromBody]Prijava prijava)
        {
            KlijentProvider provider = new KlijentProvider();

            if (provider.Zaposleni(prijava.Username, prijava.Password))
            {
                //zaposleni
                var tip = new { tip = "Zaposleni" };
                return Ok(tip);
            }
            else
            {
                Klijent k = new Klijent();
                k.Mail = prijava.Mejl;
                k.Password = prijava.Password;
                k.PunoIme = prijava.Ime;
                k.Username = prijava.Username;

                int result = provider.AddKlijent(k);

                if (result == 1)
                {
                    var tip = new { tip = "Klijent" };
                    return Ok(tip);
                }

                return NotFound();
            }
        }


        [HttpOptions]
        [Route("registrujSe")]
        public IActionResult RegistrujSeCorseCheck()
        {
            return Ok();
        }

        [HttpPost]
        [Route("glasaj")]
        public IActionResult Glasaj([FromBody]Glasaj ocena)
        {
            SmestajProvider smProvider = new SmestajProvider();
            if (ocena.Ocena < 0)
            {
                var tip0 = new { tip = "Negativan Broj" };
                return Ok(tip0);
            }
            //KlijentProvider klijent = new KlijentProvider();

            //if (klijent.Postoji(ocena.Klijent.Password, ocena.Klijent.Username))
            //{
            //    var tip = new { tip = smProvider.Glasaj(ocena.IdSmestaja, ocena.Ocena) };
            //    return Ok(tip);
            //}

            var tip = new { tip = smProvider.Glasaj(ocena.IdSmestaja, ocena.Ocena) };
                return Ok(tip);
        }

        [HttpOptions]
        [Route("glasaj")]
        public IActionResult GlasajCorseCheck()
        {
            return Ok();
        }


        [HttpOptions]
        [Route("LogIn")]
        public IActionResult PrijavljivanjeCorseCheck()
        {
            return Ok();
        }

        [HttpPost]
        [Route("otkaziR")]
        public IActionResult OtkaziRezervaciju([FromBody]Klasa klasa)
        {
            TerminiProvider termin = new TerminiProvider();
            KlijentProvider klijent = new KlijentProvider();

            if (klijent.Postoji(klasa.Klijent.Password, klasa.Klijent.Username))
            {
                if (termin.Otkazi(klasa.Termin, klasa.Soba, klasa.Klijent))
                {
                    var tip = new { tip = "Otkazano" };
                    return Ok(tip);
                }
                else
                {
                    var tip = new { tip = "Nije otkazana" };
                    return Ok(tip);
                }
            }
            else
            {
                return NotFound();
            }

        }
        [HttpOptions]
        [Route("otkaziR")]
        public IActionResult OtkaziRezervacijuCorseCheck()
        {
            return Ok();
        }


        [HttpPost]
        [Route("rezervisi")]
        public IActionResult Rezervisi([FromBody]Klasa klasa) //id Smestaja
        {
            //prvo se proveri da li je u tabeli klijenti ako nije onda treba da se registruje 
            //ako jeste onda da li slobodan smestaj  
            //ako nije stavi datum u klijentu i smestajZaKoji je zainteresovan
            //ako jeste rezervisi 
            TerminiProvider termin = new TerminiProvider();
            KlijentProvider klijent = new KlijentProvider();

            if (klijent.Postoji(klasa.Klijent.Password, klasa.Klijent.Username))
            {
                if (termin.Slobodan(klasa.Termin, klasa.Soba, klasa.Klijent))
                {
                    var tip = new { tip = "Rezervisano" };
                    return Ok(tip);
                }
                else
                {
                    var tip = new { tip = "Zauzet" };
                    return Ok(tip);
                }
            }
            else
            {
                var tip = new { tip = "Nije ulogovan korisnik" };
                return Ok(tip);
            }
        }

        [HttpOptions]
        [Route("rezervisi")]
        public IActionResult RezervisiCorseCheck()
        {
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            KlijentProvider provider = new KlijentProvider();

            return provider.RemoveKlijent(id);
        }
        [HttpOptions("{id}")]
        public IActionResult DeleteCorseCheck()
        {
            return Ok();
        }

        public ActionResult novaStrana()
        {
            return View();
        }

        //GET: api/<controller>
        [HttpGet("byUsername/{username}")]

        public IActionResult GetUserByUsername(string username)
        {
            KlijentProvider provider = new KlijentProvider();
            Klijent klijent = provider.GetKlijenti().Where<Klijent>(x => x.Username == username).FirstOrDefault();
            var result = new
            {
                Username = klijent.Username,
                Password = klijent.Password
            };
            return Ok(result);
        }
    }
}
