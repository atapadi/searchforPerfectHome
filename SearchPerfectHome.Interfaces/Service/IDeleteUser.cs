using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPerfectHome.Interfaces
{
    public interface IDeleteUser
    {
        Task<bool> Delete(string userId);
    }
}
