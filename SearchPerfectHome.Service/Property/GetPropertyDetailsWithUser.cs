using SearchPerfectHome.Interfaces;
using SearchPerfectHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPerfectHome.Service
{
    public class GetPropertyDetailsWithUser : IGetPropertyDetailsWithUser
    {
        private IPropertyRepository _propertyRepository;
        private IGetUser _getUser;

        public GetPropertyDetailsWithUser(IPropertyRepository propertyRepository, IGetUser getUser)
        {
            _propertyRepository = propertyRepository;
            _getUser = getUser;
        }

        public PropertyModel GetPropertyDetails(int propertyId)
        {
            var property = _propertyRepository.GetPropertyByPropertyId(propertyId);
            var user = _getUser.Get(property.UserId);

            var model = new PropertyModel
            {
                PropertyName = property.PropertyName,
                Address = property.Address,
                City = property.City,
                Amenities = property.Amenities,
                Details = property.Details,
                State = property.State,
                University = property.University,
                Users = new UsersModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    University = user.University
                }
            };
            return model;
        }

        public IQueryable<PropertyModel> GetAllProperty()
        {
            var property = _propertyRepository.GetAllItems();
            return property;
        }
    }
}
