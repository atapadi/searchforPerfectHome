using SearchPerfectHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPerfectHome.Interfaces
{
    public interface IUserRepository : IRepository<UsersModel>
    {
        UsersModel GetUserProfile(string userId);
        IQueryable<UsersModel> GetAllUsers();
        Task<bool> DeleteUser(UsersModel model);
    }
}
