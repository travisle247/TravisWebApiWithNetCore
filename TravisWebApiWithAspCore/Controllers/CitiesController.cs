using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravisWebApiWithAspCore.Dao;

namespace TravisWebApiWithAspCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Cities")]
    public class CitiesController : Controller
    {
        [HttpGet()]
        public IActionResult GetCities()
        {
            return Ok(CitiesDao.Current.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var cityToReturn =CitiesDao.Current.Cities.FirstOrDefault(x => x.Id == id);
            if(cityToReturn ==null)
            {
                return NotFound();
            }

            return Ok(cityToReturn);
        }
    }
}