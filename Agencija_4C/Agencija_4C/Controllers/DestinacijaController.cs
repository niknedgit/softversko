using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agencija_4C.Entiteti;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MailKit;
using Newtonsoft.Json;
using MimeKit;
using System.Net;
using Agencija_4C.DTOs;
//using System.Net.Mail;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Agencija_4C.Controllers
{
    [Route("api/[controller]")]
    public class DestinacijaController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            DestinacijaProvider provider = new DestinacijaProvider();

            //IEnumerable<Destinacija> destinacije = provider.GetDestinacije();
            IEnumerable<DestinacijaView> destinacije = provider.GetDestinacije();
            //var json = JsonConvert.SerializeObject(destinacije);
            if (destinacije == null)
                return NotFound();
            var json = JsonConvert.SerializeObject(destinacije, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            return Ok(json);
            //return Json(json);
        }

       
        //[HttpGet]
        //[Route("vrati")]
        //public JsonResult vratiD()
        //{

        //    DestinacijaProvider provider = new DestinacijaProvider();
        //    var json = JsonConvert.SerializeObject(provider.GetDestinacija(3));
        //    return Json(json);
        //}


        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            DestinacijaProvider provider = new DestinacijaProvider();
            //var json = JsonConvert.SerializeObject(provider.GetDestinacija(id));
            DestinacijaView dest = provider.GetDestinacija(id);
            if (dest == null)
                return NotFound();
            var json = JsonConvert.SerializeObject(dest, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            return Ok(json);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("add")]
        public IActionResult Post([FromBody]DataWrapper.AddDest dest)
        {
            DestinacijaProvider provider = new DestinacijaProvider();

            if (provider.AddDestinacija(dest))
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
        [HttpPut]
        [Route("edit")]

        public IActionResult Put([FromBody]DataWrapper.PutDest dest)
        {
            DestinacijaProvider provider = new DestinacijaProvider();
            if (provider.PutDestinacija(dest))
            {
                var tip = new { tip = "promenjeno" };
                return Ok(tip);
            }
           return NotFound();
        }
        [HttpOptions]
        public IActionResult PutCorseCheck()
        {
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            DestinacijaProvider provider = new DestinacijaProvider();

            return provider.RemoveDestinacija(id);
        }
        [HttpOptions("{id}")]

        public IActionResult DeleteCorseCheck()
        {
            return Ok();
        }
    }
}
