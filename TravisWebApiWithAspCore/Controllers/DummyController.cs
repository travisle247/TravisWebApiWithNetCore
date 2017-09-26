using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravisWebApiWithAspCore.Entities;

namespace TravisWebApiWithAspCore.Controllers
{
    [Produces("application/json")]   
    public class DummyController : Controller
    {
        private CityInfoContext _ctx;

        public DummyController(CityInfoContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("api/dummy/testdatabase")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}