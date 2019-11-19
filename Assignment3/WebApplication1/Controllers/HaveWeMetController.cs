using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assignment3Lib;
using Newtonsoft.Json;
using System.IO;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/")]
    public class HaveWeMetController : ControllerBase
    {
        GoogleResponse referenceResponse;
        public void LoadJson()
        {
            using (StreamReader r = new StreamReader("Location History Short.json"))
            {
                string json = r.ReadToEnd();
                referenceResponse = JsonConvert.DeserializeObject<GoogleResponse>(json);
            }
        }

        //GET / Return reference data with no params
        [HttpGet]
        public GoogleResponse Get()
        {
            if (referenceResponse == null) LoadJson();
            return referenceResponse;
        }

        //GET / Return reference data's location for a given date post ""2014-10-10"" to test
        [Route("Alibi")]
        [HttpGet]
        public location Get([FromBody] DateTime dt)
        {
            if (referenceResponse == null) LoadJson();
            return GoogleMethods.checkAlibi(referenceResponse, dt);
        }

        // POST / Returns if they have met
        [HttpPost]
        public bool Post([FromBody] GoogleResponse response)
        {
            if (referenceResponse == null) LoadJson();
            return GoogleMethods.haveWeMet(referenceResponse, response);
        }

    }
}
