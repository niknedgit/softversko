using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Agencija_4C.Entiteti;
using NHibernate;
using NHibernate.Linq;
using Agencija_4C.Providers;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Agencija_4C.Controllers
{
    [Route("api/[controller]")]
    public class ZaposleniController : Controller
    {
        // GET: api/<controller>
        //[HttpGet]
        //public JsonResult Get()
        //{
        //    ZaposleniProvider provider = new ZaposleniProvider();

        //    IEnumerable<Zaposleni> zap = provider.GetZaposlene();
        //    var json = JsonConvert.SerializeObject(zap);
        //    return Json(json);
        //}
        
        [HttpGet]
        public IActionResult Get()
        {
            ZaposleniProvider provider = new ZaposleniProvider();

            IEnumerable<Zaposleni> zap = provider.GetZaposlene();
            if (zap == null)
                return NotFound();
            var json = JsonConvert.SerializeObject(zap);
            return Ok(json);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ZaposleniProvider provider = new ZaposleniProvider();
            Zaposleni z = provider.GetZaposleni(id);
            if (z == null)
                return NotFound();
            var json = JsonConvert.SerializeObject(z);
            return Ok(json);
        }

        [HttpGet]
        [Route("rezervacijeKlijenta/{username}")]
        public IActionResult VratiRezervacijeKlijenta(string username)
        {
            KlijentProvider kp = new KlijentProvider();
            List<DataWrapper.RezZaposlenom> rez = new List<DataWrapper.RezZaposlenom>();
            RezervacijaProvider rp = new RezervacijaProvider();

            Klijent k = kp.GetKlijentByUsername(username);
           
            rez = rp.VratiRezervacijeZaposlenom(k);

            if (rez != null)
            {
                var json = JsonConvert.SerializeObject(rez, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return Ok(json);
            }
            return NotFound();
        }
        [HttpOptions]
        [Route("rezervacijeKlijenta/{username}")]
        public IActionResult VratiRezCheck()
        {
            return Ok();
        }
        // POST api/<controller>
        [HttpPost]
        public int Post([FromBody]Zaposleni zap)
        {
            ZaposleniProvider provider = new ZaposleniProvider();
            return provider.AddZaposleni(zap);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Zaposleni value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            ZaposleniProvider provider = new ZaposleniProvider();
            return provider.RemoveZaposleni(id);
        }
    }
}
