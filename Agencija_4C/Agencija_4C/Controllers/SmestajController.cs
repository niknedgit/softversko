using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Agencija_4C.Entiteti;
using Agencija_4C.Providers;
using Newtonsoft.Json;
using Agencija_4C.DTOs;
using Agencija_4C.DataWrapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Agencija_4C.Controllers
{
    [Route("api/[controller]")]
    public class SmestajController : Controller
    {
        //// GET: api/<controller>
        //[HttpGet]
        //public JsonResult Get()
        //{
        //    SmestajProvider provider = new SmestajProvider();

        //    IEnumerable<Smestaj> sme = provider.GetSmestaji();
        //    var json = JsonConvert.SerializeObject(sme);
        //    return Json(json);
        //    //var json = JsonConvert.SerializeObject(sme, new JsonSerializerSettings()
        //    //{
        //    //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        //    //});
        //    //return Json(json);
        //}

        [HttpGet]
        public IActionResult Get()
        {
            SmestajProvider provider = new SmestajProvider();


            //IEnumerable<Destinacija> destinacije = provider.GetDestinacije();
            IEnumerable<SmestajView> smestaj = provider.GetSmestaje();
            //var json = JsonConvert.SerializeObject(destinacije);
            if (smestaj == null)
                return NotFound();
            var json = JsonConvert.SerializeObject(smestaj, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            return Ok(json);
            //return Json(json);
        }
        [HttpGet]
        [Route("statistika")]
        public IActionResult Statistika()
        {
            SmestajProvider provider = new SmestajProvider();
            List<Statistika> s = provider.vratiStatistiku();
            var json = JsonConvert.SerializeObject(s, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            return Ok(json);
        }
        [HttpGet]
        [Route("lastMinute")]
        public IActionResult LastMinute()
        {
            SmestajProvider provider = new SmestajProvider();

            List<SmestajView> smestaj = provider.LastMinute();
            smestaj = NoLastMinute(smestaj);
            var response = new { smestaj = smestaj, poruka = "" };
            var json = "";

            if (smestaj.Count > 0)
            {
                response = new { smestaj = smestaj, poruka = string.Empty };
                json = JsonConvert.SerializeObject(response, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return Ok(json);
            }
            response = new { smestaj = smestaj, poruka = "Trenutno nema first minute ponuda." };
            json = JsonConvert.SerializeObject(response, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            return Ok(response);
        }
        [HttpGet]
        [Route("firstMinute")]
        public IActionResult FirstMinute()
        {
            SmestajProvider provider = new SmestajProvider();

            List<SmestajView> smestaj = provider.FirstMinute();
            smestaj = NoLastMinute(smestaj);
            var response = new { smestaj = smestaj, poruka = ""};
            var json = "";

            if (smestaj.Count > 0)
            {
                response = new { smestaj = smestaj, poruka = string.Empty };
                json = JsonConvert.SerializeObject(response, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return Ok(json);
            }
            response = new { smestaj = smestaj, poruka = "Trenutno nema first minute ponuda." };
            json = JsonConvert.SerializeObject(response, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            return Ok(response);
        }

        private List<SmestajView> NoLastMinute(List<SmestajView> ponude)
        {
            if(ponude != null)
            {
                return ponude;
            }

            ponude = new List<SmestajView>();
            return ponude;
        }

        [HttpGet("{id}")]
        public IActionResult VratiS(int id)
        {
            SmestajProvider provider = new SmestajProvider();
            List<SmestajView> s = provider.VratiS(id);
            if (s == null)
                return NotFound();
            var json = JsonConvert.SerializeObject(s, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            return Ok(json);
        }

        [HttpGet("idSmestaja={id}")]
        public IActionResult JedanSmestaj(int id)
        {
            SmestajProvider provider = new SmestajProvider();
            SmestajView s = provider.GetSmestaj(id);
            if (s == null)
                return NotFound();
            var json = JsonConvert.SerializeObject(s, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            return Ok(json);
        }

        //[HttpGet]
        //[Route("filterCena/id={id}&min={min}&max={max}")]
        //public IActionResult FilterCena(int id, int min, int max)
        //{
        //    SmestajProvider provider = new SmestajProvider();
        //    DestinacijaProvider dp = new DestinacijaProvider();

        //    List<SmestajView> fsm = provider.FilterCena(id, min, max);
        //    if (fsm == null)
        //        return NotFound();
        //    var json = JsonConvert.SerializeObject(fsm, new JsonSerializerSettings()
        //    {
        //        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        //    });

        //    return Ok(json);
        //}

        //[HttpGet("filterDatum/D={D}&min={min}")]
        //public IActionResult FilterDatum(int D, string min)
        //{
        //    SmestajProvider provider = new SmestajProvider();
        //    TerminiProvider tp = new TerminiProvider();

        //    List<SmestajView> sm = provider.VratiS(D); 
        //    List<SmestajView> fsm = new List<SmestajView>();

        //    DateTime minimal = DateTime.Parse(min);

        //    foreach (SmestajView smes in sm)
        //    {
        //        bool ima = tp.FilterDatum(smes.Id, minimal);
        //        if (ima)
        //            fsm.Add(smes);
        //    }
        //    if (fsm == null)
        //        return NotFound();
        //    var json = JsonConvert.SerializeObject(fsm, new JsonSerializerSettings()
        //    {
        //        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        //    });

        //    return Ok(json);
        //}

        [HttpPost]
        [Route("filter")]
        public IActionResult Filter([FromBody]Filter filter)
        {
            SmestajProvider provider = new SmestajProvider();

            List<SmestajView> fsm = new List<SmestajView>();

            if (filter.C_min > 0 || filter.C_max > 0)
            {
                fsm = provider.FilterCena(filter.idDest, filter.C_min, filter.C_max);
            }

            if (filter.D_od != null || filter.D_do != null)
            {
                DateTime D_od = DateTime.Parse(filter.D_od);
                DateTime D_do = DateTime.Parse(filter.D_do);
                if (filter.D_od == null)
                {
                    D_od = new DateTime(2019, 1, 1);
                }
                if (filter.D_do == null)
                {
                    D_do = new DateTime(2119, 1, 1);
                }

                TerminiProvider tp = new TerminiProvider();

                List<SmestajView> sm = provider.VratiS(filter.idDest);
                List<SmestajView> fsmD = new List<SmestajView>();
                foreach (SmestajView smes in sm)
                {
                    bool ima = tp.FilterDatum(smes.Id, D_od, D_do);
                    if (ima)
                        fsmD.Add(smes);
                }

                foreach (SmestajView smes in fsmD)
                {
                    if (!fsm.Contains(smes))
                        fsm.Add(smes);
                }
            }

            if (fsm == null)
                return NotFound();
            var json = JsonConvert.SerializeObject(fsm, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

            return Ok(json);
        }

        [HttpOptions]
        [Route("filter")]
        public IActionResult FilterCorseCheck()
        {
            return Ok();
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Post([FromBody]DataWrapper.AddHotel hotel)
        {
            SmestajProvider provider = new SmestajProvider();

            if (provider.AddSmestaj(hotel))
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

        //// GET api/<controller>/5
        //[HttpGet("{id}")]
        //public JsonResult Get(int id)
        //{
        //    SmestajProvider provider = new SmestajProvider();
        //    var json = JsonConvert.SerializeObject(provider.GetSmestaj(id));
        //    return Json(json);
        //}

        // POST api/<controller>
        [HttpPost]
        [Route("cena")]
        public IActionResult CenaSobe([FromBody]Soba soba)
        {
            SmestajProvider provider = new SmestajProvider();
            double cena = provider.CenaSobe(soba.smestaj, soba.Tip);

            if (cena > 0)
                return Ok(cena);
            return NotFound();
        }

        [HttpOptions]
        [Route("cena")]
        public IActionResult CenaSobeCheck()
        {
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("edit")]
        public IActionResult Put([FromBody]PutSmestaj value)
        {
            SmestajProvider provider = new SmestajProvider();
            if (provider.PutSmestaj(value))
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
            SmestajProvider provider = new SmestajProvider();
            return provider.RemoveSmestaj(id);
        }

        [HttpOptions("{id}")]
        public IActionResult DeleteCorseCheck()
        {
            return Ok();
        }
    }
}
