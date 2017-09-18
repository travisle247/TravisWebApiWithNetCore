using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TravisWebApiWithAspCore.Models
{
    public class PointOfInterestCreationDto
    {
        [Required(ErrorMessage = "Name is required to create new point of interest.")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
