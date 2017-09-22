using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravisWebApiWithAspCore.Models;

namespace TravisWebApiWithAspCore.Dao
{
    public class CitiesDao
    {
        public static CitiesDao Current { get; } = new CitiesDao();

        public List<CityDto> Cities { get; set; }

        public CitiesDao()
        {
            Cities = new List<CityDto>
            {
                new CityDto{Id=1,Name="Los Angeles" , Description= "City of Angel", PointsOfInterest=new List<PointsOfInterestDto>()
                    {
                        new PointsOfInterestDto()
                        {
                            Id=1,
                            Description="Beach",
                            Name="Santa Monica"
                        },
                        new PointsOfInterestDto()
                        {
                            Id=2,
                            Description="Movie",
                            Name="Hollywood"
                        },
                    }
                },
                new CityDto{Id=2,Name="Dallas" , Description= "City of Business"},
                new CityDto{Id=3,Name="Atlanta" , Description= "City of Greenery"},
            };
        }
    }
}
