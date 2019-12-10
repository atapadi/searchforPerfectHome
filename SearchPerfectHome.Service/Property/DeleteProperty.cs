using SearchPerfectHome.Interfaces;
using SearchPerfectHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPerfectHome.Service
{
    public class DeleteProperty : IDeleteProperty
    {
        private readonly IPropertyRepository _propertyRepository;

        public DeleteProperty(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public Task<bool> Delete(int propertyId)
        {
            PropertyModel model = new PropertyModel { Id = propertyId };
            return _propertyRepository.Delete(model);
        }
    }
}
