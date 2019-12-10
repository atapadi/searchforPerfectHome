using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SearchPerfectHome.Models
{
    public class PropertyImageModel :Entity
    {
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public Nullable<int> Size { get; set; }
        public HttpPostedFileBase[] files { get; set; }
        public string filePath { get; set; }
        public DateTime ImageUploadedTime { get; set; }
    }
}
