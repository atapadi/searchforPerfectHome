using SearchPerfectHome.Interfaces;
using SearchPerfectHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPerfectHome.Service
{
    public class GetUserProperty : IGetUserProperty
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IPropertyRepository _propertyRepository;

        public GetUserProperty(IPropertyImageRepository propertyImageRepository, IPropertyRepository propertyRepository)
        {
            _propertyImageRepository = propertyImageRepository;
            _propertyRepository = propertyRepository;
        }

        public async Task<List<PropertyImageModel>> Get(string userId)
        {
            var list = await _propertyImageRepository.GetAll(userId);
            return list.ToList();
        }

        public PropertyModel Get(int propertyImageId)
        {
            var list = _propertyRepository.GetPropertyByImageId(propertyImageId);
            return list;
        }
    }
}
