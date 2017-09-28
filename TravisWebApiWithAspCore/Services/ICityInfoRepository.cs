using System;
using System.Collections.Generic;
using TravisWebApiWithAspCore.Entities;
namespace TravisWebApiWithAspCore.Services
{
    public interface ICityInfoRepository
    {
        bool CityExists(int cityId);

        IEnumerable<City> GetCities();

        City GetCity(int cityId, bool isIncludePointOfInterest);

        IEnumerable<PointOfInterest> GetPointsOfInterest(int cityId);

        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);

        void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);

        void DeletePointOfInterest(PointOfInterest pointOfInterest);

        bool Save();

    }
}
