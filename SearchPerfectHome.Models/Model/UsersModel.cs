using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SearchPerfectHome.Models
{
    public class UsersModel : Entity
    {
        public int PropertyId { get; set; }
        public string UserName { get; set; }
        public string Interests { get; set; }
        public string Status { get; set; }
        public string ImagePath { get; set; }
        public string University { get; set; }
        public string Degree { get; set; }
        public DateTime LastUpdated { get; set; }
        public HttpPostedFileBase[] UserImage { get; set; }
        public string Email { get; set; }
        public bool IsFirstTimeRegister { get; set; }
    }
}
