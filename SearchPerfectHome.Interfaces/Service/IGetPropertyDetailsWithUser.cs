using SearchPerfectHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPerfectHome.Interfaces
{
    public interface IGetPropertyDetailsWithUser
    {
        PropertyModel GetPropertyDetails(int propertyId);
        IQueryable<PropertyModel> GetAllProperty();
    }
}
