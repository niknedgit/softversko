//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Agencija_4C.Entiteti;
//using Agencija_4C.Providers;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;

//namespace Agencija_4C.Controllers
//{
//    [Route("api/[controller]")]
//    public class RezervacijaController : Controller
//    {
//        // GET: api/<controller>
//        [HttpGet]
//        public JsonResult Get()
//        {
//            RezervacijaProvider provider = new RezervacijaProvider();

//            IEnumerable<Rezervacija> rez = provider.GetRezervacije();
//            var json = JsonConvert.SerializeObject(rez);
//            return Json(json);
//        }

//        // GET api/<controller>/5
//        [HttpGet("{id}")]
//        public JsonResult Get(int id)
//        {
//            RezervacijaProvider provider = new RezervacijaProvider();
//            var json = JsonConvert.SerializeObject(provider.GetRezervacija(id));
//            return Json(json);
//        }

//        // POST api/<controller>
//        [HttpPost]
//        public int Post([FromBody]Rezervacija rez)
//        {
//            RezervacijaProvider provider = new RezervacijaProvider();
//            return provider.AddRezervacija(rez);
//        }

//        // PUT api/<controller>/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody]Rezervacija value)
//        {
//        }

//        // DELETE api/<controller>/5
//        [HttpDelete("{id}")]
//        public int Delete(int id)
//        {
//            RezervacijaProvider provider = new RezervacijaProvider();
//            return provider.RemoveRezervacija(id);
//        }
//    }
//}
