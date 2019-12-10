using SearchPerfectHome.Interfaces;
using SearchPerfectHome.Models;
using SearchPerfectHome.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPerfectHome.DAL
{
    public class PropertyRepository : EntityRepositoryBase<PropertyModel>, IPropertyRepository
    {
        protected override bool OnSaveToStorage(PropertyModel model)
        {
            bool returnValue = false;
            Property property = null;
            try
            {
                using (var db = new SearchHomeEntities())
                {
                    if (model.Id == 0)
                    {
                        property = new Property
                        {
                            PropertyName = model.PropertyName,
                            Address = model.Address,
                            UserId = model.UserId,
                            City = model.City,
                            State = model.State,
                            Details = model.Details,
                            Amenities = model.Amenities,
                            University = model.University,
                            LastChange = DateTime.Now
                        };
                        db.Properties.Add(property);
                    }
                    else
                    {
                        property = db.Properties.First(x => x.Id == model.Id);
                        property.PropertyName = model.PropertyName;
                        property.Address = model.Address;
                        property.City = model.City;
                        property.State = model.State;
                        property.Details = model.Details;
                        property.Amenities = model.Amenities;
                        property.University = model.University;
                        property.LastChange = DateTime.Now;
                    }

                    if (returnValue = db.SaveChanges() > 0)
                        model.Id = property.Id;
                }
            }
            catch (Exception ex)
            {
                LogWriter.LogException("Property Repository: ", ex);
            }

            return returnValue;
        }

        public bool UpdatePropertyImages(IEnumerable<PropertyImageModel> models, int propertyId)
        {
            bool returnValue = false;
            using (var db = new SearchHomeEntities())
            {
                var prop = db.PropertyImages.First(x => x.PropertyId == propertyId);

                foreach (var model in models)
                {
                    prop.ImagePath = model.filePath;
                    prop.LastUpdated = DateTime.Now;
                }
                returnValue = db.SaveChanges() > 0;
            }
            return returnValue;
        }

        public bool AddPropertyImages(IEnumerable<PropertyImageModel> models, int propertyId)
        {
            bool returnValue = false;
            using (var db = new SearchHomeEntities())
            {
                foreach (var model in models)
                {
                    var propertyImage = new PropertyImage
                    {
                        ImagePath = model.filePath,
                        Name = model.Name,
                        PropertyId = propertyId,
                        Size = model.Size
                    };
                    db.PropertyImages.Add(propertyImage);
                }
                returnValue = db.SaveChanges() > 0;
            }
            return returnValue;
        }

        protected override IQueryable<PropertyModel> OnGetAllItems(string userId)
        {
            List<PropertyModel> model = null;
            using (var db = new SearchHomeEntities())
            {
                model = db.Properties.Where(x => x.UserId == userId)
                     .Select(x => new PropertyModel
                     {
                         PropertyName = x.PropertyName,
                         Id = x.Id,
                         Images = x.PropertyImages.Select(y => new PropertyImageModel
                         {
                             filePath = y.ImagePath
                         })
                     }).ToList();
            }

            return model.AsQueryable();
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="propertyImageId"></param>
        /// <returns></returns>
        public PropertyModel GetPropertyByImageId(int propertyImageId)
        {
            PropertyModel model = null;
            using (var db = new SearchHomeEntities())
            {
                model = db.PropertyImages.Where(x => x.PropertyId == propertyImageId)
                    .Select(x => new PropertyModel
                    {
                        Images = db.PropertyImages.Where(y => y.PropertyId == propertyImageId)
                                .Select(y => new PropertyImageModel
                                {
                                    filePath = y.ImagePath,
                                    Id = y.Id
                                }).ToList(),

                        //IndividualImage = new PropertyImageModel { filePath = x.ImagePath, Id = x.Id },
                        Amenities = x.Property.Amenities,
                        Address = x.Property.Address,
                        City = x.Property.City,
                        PropertyName = x.Property.PropertyName,
                        State = x.Property.State,
                        Details = x.Property.Details,
                        Id = x.Property.Id,
                        University = x.Property.University,
                        LastChanged = (DateTime) x.Property.LastChange
                    }).FirstOrDefault();
            }
            return model;
        }

        public PropertyModel GetPropertyByPropertyId(int propertyId)
        {
            PropertyModel model = null;
            using (var db = new SearchHomeEntities())
            {
                model = db.Properties.Where(x => x.Id == propertyId)
                    .Select(x => new PropertyModel
                    {
                        Amenities = x.Amenities,
                        Address = x.Address,
                        City = x.City,
                        PropertyName = x.PropertyName,
                        State = x.State,
                        Details = x.Details,
                        UserId = x.UserId,
                        University = x.University
                    }).FirstOrDefault();
            }
            return model;
        }

        public IQueryable<PropertyModel> GetAllItems()
        {
            List<PropertyModel> model = null;
            try
            {
                using (var db = new SearchHomeEntities())
                {
                    model = (from pt in db.Properties
                             join user in db.USERS on pt.UserId equals user.UserId
                             join pimg in db.PropertyImages on pt.Id equals pimg.PropertyId
                             select new PropertyModel
                             {
                                 Id = pt.Id,
                                 Address = pt.Address,
                                 City = pt.City,
                                 State = pt.State,
                                 Details = pt.Details,
                                 Amenities = pt.Amenities,
                                 University = pt.University,
                                 PropertyName = pt.PropertyName,
                                 UserId = pt.UserId,
                                 Users = new UsersModel
                                 {
                                     Email = user.UserEmail,
                                     UserName = user.UserName
                                 },
                                 IndividualImage = new PropertyImageModel
                                 {
                                     filePath = pimg.ImagePath,
                                     Id = pimg.Id
                                 },
                                 LastChanged = (DateTime) pt.LastChange
                             }).OrderByDescending(x => x.LastChanged).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return model.AsQueryable();
        }

        protected override bool OnRemoveFromStorage(PropertyModel model)
        {
            bool returnValue;
            try
            {
                using (var db = new SearchHomeEntities())
                {
                    var propImage = db.PropertyImages.Where(x => x.PropertyId == model.Id);
                    db.PropertyImages.RemoveRange(propImage);

                    var property = db.Properties.Where(x => x.Id == model.Id).First();
                    db.Properties.Remove(property);

                    returnValue = db.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }

        public bool DeleteUserProperty(PropertyModel model)
        {
            bool returnValue = false;
            try
            {
                using (var db = new SearchHomeEntities())
                {
                    var propImg = db.PropertyImages.Where(x => x.Property.UserId == model.UserId);
                    db.PropertyImages.RemoveRange(propImg);

                    var properties = db.Properties.Where(x => x.UserId == model.UserId);
                    db.Properties.RemoveRange(properties);

                    returnValue = db.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnValue;
        }

        public bool CheckPropertyPresent(string userId)
        {
            bool returnValue = false;
            try
            {
                using (var db = new SearchHomeEntities())
                {
                    returnValue = db.Properties.Where(x => x.UserId == userId).Any();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnValue;
        }

    }
}
