using System;
using TravisWebApiWithAspCore.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace TravisWebApiWithAspCore.Services
{
    public class CityInfoRespository : ICityInfoRepository
    {
        private CityInfoContext _context;

        public CityInfoRespository(CityInfoContext context)
        {
            _context = context;
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(c => c.Name).ToList();
        }

        public City GetCity(int cityId, bool isIncludePointOfInterest)
        {
            if(isIncludePointOfInterest == true)
            {
                return _context.Cities.Include(c=>c.PointsOfInterest)
                               .FirstOrDefault(cx => cx.Id == cityId);

			}
			return _context.Cities.FirstOrDefault(cx => cx.Id == cityId);

		}

        public IEnumerable<PointOfInterest> GetPointsOfInterest(int cityId)
        {
            City selectedCity = GetCity(cityId, true);

            return selectedCity.PointsOfInterest.ToList();
        }

        public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
        {
			City selectedCity = GetCity(cityId, true);

            return selectedCity.PointsOfInterest.FirstOrDefault(x => x.Id == pointOfInterestId);
        }

    }
}
