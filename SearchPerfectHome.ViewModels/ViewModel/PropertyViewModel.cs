using SearchPerfectHome.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPerfectHome.ViewModels
{
    public class PropertyViewModel : Entity
    {
        public string ErrorMessage { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Property Name")]
        public string PropertyName { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address cannot be blank.")]
        [StringLength(255, MinimumLength = 1)]
        public string Address { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "State cannot be blank.")]
        [StringLength(50, MinimumLength = 1)]
        public string State { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City Name cannot be blank.")]
        [StringLength(255, MinimumLength = 1)]
        public string City { get; set; }
        public string Amenities { get; set; }
        public string Details { get; set; }

        [Required(ErrorMessage="University cannot be blank.")]
        public string University { get; set; }
        public PropertyImageViewModel PropertyImages { get; set; }

        public List<PropertyImageViewModel> Property { get; set; }
    }
}
