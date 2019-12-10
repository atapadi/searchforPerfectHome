using SearchPerfectHome.Interfaces;
using SearchPerfectHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPerfectHome.DAL
{
    public class PropertyImageRepository : EntityRepositoryBase<PropertyImageModel>, IPropertyImageRepository
    {

        protected override IQueryable<PropertyImageModel> OnGetAllItems(string userId)
        {
            List<PropertyImageModel> model = null;

            using (var db = new SearchHomeEntities())
            {
                model = db.PropertyImages.Where(x => x.Property.UserId == userId)
                     .Select(x => new PropertyImageModel
                     {
                         filePath = x.ImagePath,
                         Name = x.Property.PropertyName,
                         PropertyId = x.PropertyId,
                         Id = x.Id
                     }).ToList();
            }

            return model.AsQueryable();
        }

        public List<PropertyImageModel> GetAllPropertyImages()
        {
            List<PropertyImageModel> model = null;

            using (var db = new SearchHomeEntities())
            {
                model = db.PropertyImages
                     .Select(x => new PropertyImageModel
                     {
                         filePath = x.ImagePath,
                         Name = x.Property.PropertyName,
                         PropertyId = x.PropertyId,
                         Id = x.Id,
                         ImageUploadedTime = (DateTime) x.Property.LastChange
                     }).ToList();
            }

            return model;
        }
    }
}
