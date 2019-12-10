using SearchPerfectHome.Interfaces;
using SearchPerfectHome.Models;
using SearchPerfectHome.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchPerfectHome.Service
{
    public class AddProperty : IAddProperty
    {
        private readonly IPropertyRepository _propertyRepository;

        public AddProperty(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<PropertyViewModel> Add(PropertyViewModel viewModel, List<PropertyImageViewModel> imageModel)
        {
            var propertyModel = new PropertyModel
            {
                UserId = viewModel.UserId,
                Address = viewModel.Address,
                City = viewModel.City,
                State = viewModel.State,
                PropertyName = viewModel.PropertyName,
                Details = viewModel.Details,
                Amenities = viewModel.Amenities,
                University = viewModel.University
            };

            var propertyImagesModel = imageModel.Select(x => new PropertyImageModel
            {
                filePath = x.filePath,
                Size = x.Size,
                Name = x.Name
            });

            if (await _propertyRepository.Add(propertyModel))
            {
                if (!_propertyRepository.AddPropertyImages(propertyImagesModel, propertyModel.Id))
                {
                    viewModel.ErrorMessage = "Error while adding property images.";
                }
            }
            else
            {
                viewModel.ErrorMessage = "Error while adding property.";
            }
            return viewModel;
        }
    }
}
