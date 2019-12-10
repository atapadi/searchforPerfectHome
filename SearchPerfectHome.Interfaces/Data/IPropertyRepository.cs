using SearchPerfectHome.Models;
using System.Collections.Generic;
using System.Linq;

namespace SearchPerfectHome.Interfaces
{
    public interface IPropertyRepository : IRepository<PropertyModel>
    {
        bool AddPropertyImages(IEnumerable<PropertyImageModel> models, int propertyId);
        PropertyModel GetPropertyByImageId(int id);
        PropertyModel GetPropertyByPropertyId(int propertyId);

        IQueryable<PropertyModel> GetAllItems();
        bool CheckPropertyPresent(string userId);
        bool DeleteUserProperty(PropertyModel model);
        bool UpdatePropertyImages(IEnumerable<PropertyImageModel> models, int propertyId);
    }
}
