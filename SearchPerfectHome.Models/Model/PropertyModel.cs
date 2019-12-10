using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPerfectHome.Models
{
    public class PropertyModel : Entity
    {
        //public string UserId { get; set; }
        public string PropertyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Details { get; set; }
        public string Amenities { get; set; }
        public string University { get; set; }
        public DateTime LastChanged { get; set; }
        public IEnumerable<PropertyImageModel> Images { get; set; }
        public PropertyImageModel IndividualImage { get; set; }
        public UsersModel Users { get; set; }
    }
}
