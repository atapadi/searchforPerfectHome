using SearchPerfectHome.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SearchPerfectHome.ViewModels
{
    public class UsersViewModel : Entity
    {
        public int PropertyId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        [StringLength(127, MinimumLength = 1)]
        public string UserName { get; set; }

        public string Interests { get; set; }
        public string Status { get; set; }
        public string ImagePath { get; set; }

        [StringLength(127, MinimumLength = 1)]
        [Display (Name = "University Name")]
        public string University { get; set; }
        public string Degree { get; set; }
        public DateTime LastUpdated { get; set; }
        public string ErrorMessage { get; set; }
        public HttpPostedFileBase[] UserImage { get; set; }

    }
}
