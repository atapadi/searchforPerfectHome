using SearchPerfectHome.Interfaces;
using SearchPerfectHome.Models;
using System.Threading.Tasks;

namespace SearchPerfectHome.Service
{
    public class UpdateProperty : IUpdateProperty
    {
        private readonly IPropertyRepository _propertyRepository;

        public UpdateProperty(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<string> Update(PropertyModel model)
        {
            string errorMessage = "";

            if (await _propertyRepository.Update(model))
            {
                if (!_propertyRepository.UpdatePropertyImages(model.Images, model.Id))
                {
                    errorMessage = "Error while adding property images.";
                }
            }

            return errorMessage;
        }
    }
}
