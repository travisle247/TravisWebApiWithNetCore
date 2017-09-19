using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravisWebApiWithAspCore.Dao;
using TravisWebApiWithAspCore.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;

namespace TravisWebApiWithAspCore.Controllers
{
    [Produces("application/json")]
    [Route("api/cities/")]
    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> _logger;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            try
            {
                var city = CitiesDao.Current.Cities.FirstOrDefault(x => x.Id == cityId);
                if (city == null)
                {
                    _logger.LogInformation($"City with {cityId} was not found in list");
                    return NotFound();
                }
                return Ok(city.PointsOfInterest);
            }
            catch(Exception ex)
            {
                _logger.LogCritical("Super wrong",ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
            
        }

        [HttpGet("{cityId}/pointsofinterest/{id}",Name ="GetPointOfInterest")]
        public IActionResult GetPointsOfInterest(int cityId, int id)
        {
            var city = CitiesDao.Current.Cities.FirstOrDefault(x => x.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(x => x.Id == id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(pointOfInterest);
        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestCreationDto pointOfInterest)
        {
            if(pointOfInterest==null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Name == pointOfInterest.Description)
			{
                ModelState.AddModelError("Descriptiom","Name and desciption can not be the same.");
			}

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var city = CitiesDao.Current.Cities.FirstOrDefault(x => x.Id == cityId);

            if(city==null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = CitiesDao.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

            var finalPointOfInterest = new PointsOfInterest()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new { cityId = cityId, id = finalPointOfInterest.Id }, finalPointOfInterest );
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id, [FromBody] PointOfInterestUpdateDto pointOfInterest)
        {
			if (pointOfInterest == null)
			{
				return BadRequest();
			}

			if (pointOfInterest.Name == pointOfInterest.Description)
			{
				ModelState.AddModelError("Description","Name and desciption can not be the same.");
			}

			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var city = CitiesDao.Current.Cities.FirstOrDefault(x => x.Id == cityId);

			if (city == null)
			{
				return NotFound();
			}

            var selectedPointOfInterest = city.PointsOfInterest.FirstOrDefault(x => x.Id == id);

            if(selectedPointOfInterest == null)
            {
                return NotFound();
            }

            selectedPointOfInterest.Name = pointOfInterest.Name;
            selectedPointOfInterest.Description = pointOfInterest.Description;

            return NoContent();
		}

        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, [FromBody] JsonPatchDocument<PointOfInterestUpdateDto> patchDoc)
		{
            if (patchDoc == null)
			{
				return BadRequest();
			}
			

			var city = CitiesDao.Current.Cities.FirstOrDefault(x => x.Id == cityId);

			if (city == null)
			{
				return NotFound();
			}

			var selectedPointOfInterest = city.PointsOfInterest.FirstOrDefault(x => x.Id == id);

			if (selectedPointOfInterest == null)
			{
				return NotFound();
			}

            var pointOfInterestToPatch = new PointOfInterestUpdateDto()
            {
                Name = selectedPointOfInterest.Name,
                Description = selectedPointOfInterest.Description
            };
			
            patchDoc.ApplyTo(pointOfInterestToPatch);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

			selectedPointOfInterest.Name = pointOfInterestToPatch.Name;
			selectedPointOfInterest.Description = pointOfInterestToPatch.Description;

			return NoContent();
		}

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
			var city = CitiesDao.Current.Cities.FirstOrDefault(x => x.Id == cityId);

			if (city == null)
			{
				return NotFound();
			}

			var selectedPointOfInterest = city.PointsOfInterest.FirstOrDefault(x => x.Id == id);

			if (selectedPointOfInterest == null)
			{
				return NotFound();
			}

            city.PointsOfInterest.Remove(selectedPointOfInterest);


			return NoContent();

		}
    }
}