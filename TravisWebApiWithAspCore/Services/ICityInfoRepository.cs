using System;
using System.Collections.Generic;
using TravisWebApiWithAspCore.Entities;
namespace TravisWebApiWithAspCore.Services
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();

        City GetCity(int cityId, bool isIncludePointOfInterest);

        IEnumerable<PointOfInterest> GetPointsOfInterest(int cityId);

        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);

    }
}
