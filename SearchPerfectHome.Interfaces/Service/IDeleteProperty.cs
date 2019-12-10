using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPerfectHome.Interfaces
{
    public interface IDeleteProperty
    {
        Task<bool> Delete(int propertyId);
    }
}
