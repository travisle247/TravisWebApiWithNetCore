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

        public List<City> Cities { get; set; }

        public CitiesDao()
        {
            Cities = new List<City>
            {
                new City{Id=1,Name="Los Angeles" , Description= "City of Angel", PointsOfInterest=new List<PointsOfInterest>()
                    {
                        new PointsOfInterest()
                        {
                            Id=1,
                            Description="Beach",
                            Name="Santa Monica"
                        },
                        new PointsOfInterest()
                        {
                            Id=2,
                            Description="Movie",
                            Name="Hollywood"
                        },
                    }
                },
                new City{Id=2,Name="Dallas" , Description= "City of Business"},
                new City{Id=3,Name="Atlanta" , Description= "City of Greenery"},
            };
        }
    }
}
