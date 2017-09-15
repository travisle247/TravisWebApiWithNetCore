using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravisWebApiWithAspCore.Models
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<PointsOfInterest> PointsOfInterest { get; set; } = new List<PointsOfInterest>();

        public int NumberOfPointsOfInterest
        {
            get
            {
                return PointsOfInterest.Count;
            }
        }

    }
}
