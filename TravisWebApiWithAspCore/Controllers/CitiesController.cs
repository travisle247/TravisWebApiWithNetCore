using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravisWebApiWithAspCore.Dao;
using TravisWebApiWithAspCore.Services;
using TravisWebApiWithAspCore.Models;
using TravisWebApiWithAspCore.Entities;

namespace TravisWebApiWithAspCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Cities")]
    public class CitiesController : Controller
    {
        private ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository){
            _cityInfoRepository = cityInfoRepository;
        }


        [HttpGet()]
        public IActionResult GetCities()
        {
            //return Ok(CitiesDao.Current.Cities);
            var cityEntities = _cityInfoRepository.GetCities();

            var results = AutoMapper.Mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cityEntities);

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool isIncludePointOfInterest = false)
        {
            var cityToReturn =_cityInfoRepository.GetCity(id, isIncludePointOfInterest);
            if(cityToReturn ==null)
            {
                return NotFound();
            }

            if(isIncludePointOfInterest == true)
            {
                var cityWithPointOfInterest = AutoMapper.Mapper.Map<CityDto>(cityToReturn);

				return Ok(cityWithPointOfInterest);
            }

            var result = AutoMapper.Mapper.Map<CityWithoutPointOfInterestDto>(cityToReturn);


			return Ok(result);
        }
    }
}