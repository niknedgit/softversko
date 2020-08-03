//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Agencija_4C.Entiteti;
//using NHibernate;
//using NHibernate.Linq;
//using Agencija_4C.Providers;
//using Newtonsoft.Json;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace Agencija_4C.Controllers
//{
//    [Route("api/[controller]")]
//    public class NalogController : Controller
//    {
//        // GET: api/<controller>
//        [HttpGet]
//        public JsonResult Get()
//        {
//            NalogProvider provider = new NalogProvider();

//            IEnumerable<Nalog> nalozi = provider.GetNalozi();
//            //var json = JsonConvert.SerializeObject(nalozi);
//            var json = JsonConvert.SerializeObject(nalozi, new JsonSerializerSettings()
//            {
//                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//            });
//            return Json(json);
//        }

//        // GET api/<controller>/5
//        [HttpGet("{id}")]
//        public JsonResult Get(int id)
//        {
//            NalogProvider provider = new NalogProvider();
//            var json = JsonConvert.SerializeObject(provider.GetNalog(id));
//            return Json(json);
//        }

//        // POST api/<controller>
//        [HttpPost]
//        public int Post([FromBody]Nalog nalog)
//        {
//            NalogProvider provider = new NalogProvider();
//            return provider.AddNalog(nalog);
//        }

//        // PUT api/<controller>/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody]string value)
//        {
//        }

//        // DELETE api/<controller>/5
//        [HttpDelete("{id}")]
//        public int Delete(int id)
//        {
//            NalogProvider provider = new NalogProvider();
//            return provider.RemoveNalog(id);
//        }
//    }
//}
