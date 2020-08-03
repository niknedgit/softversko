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
using Agencija_4C.DTOs;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Agencija_4C.Controllers
{
    [Route("api/[controller]")]
    public class TerminiController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            TerminiProvider provider = new TerminiProvider();

            IEnumerable<Termini> term = provider.GetTermine();
            if (term == null)
                return NotFound();
            var json = JsonConvert.SerializeObject(term);
            return Ok(json);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            TerminiProvider provider = new TerminiProvider();
            Termini t = provider.GetTermini(id);
            if (t == null)
                return NotFound();
            //var json = JsonConvert.SerializeObject(t);
            var json = JsonConvert.SerializeObject(t, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            return Ok(json);
        }

        [HttpGet("s={id}")]
        public IActionResult VratiTermineZaSmestaj(int id)
        {
            TerminiProvider provider = new TerminiProvider();
            List<TerminiView> t = provider.VratiT_ZaS(id);
            if (t == null)
                return NotFound();
            //var json = JsonConvert.SerializeObject(t);
            var json = JsonConvert.SerializeObject(t, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            return Ok(json);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("add")]
        public IActionResult Post([FromBody]DataWrapper.AddTermini term)
        {
            TerminiProvider provider = new TerminiProvider();
            if (provider.AddTermini(term))
            {

                var tip = new { tip = "dodato" };
                return Ok(tip);
            }
            return NotFound();
        }
        [HttpOptions]
        [Route("add")]
        public IActionResult PostCorseCheck()
        {
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Termini value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            TerminiProvider provider = new TerminiProvider();
            return provider.RemoveTermini(id);
        }
    }
}
