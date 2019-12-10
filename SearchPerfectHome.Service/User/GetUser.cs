using SearchPerfectHome.Interfaces;
using SearchPerfectHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPerfectHome.Service
{
    public class GetUser : IGetUser
    {
        private IUserRepository _userRepository;

        public GetUser()
        {

        }

        public GetUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UsersModel Get(string userId)
        {
           return _userRepository.GetUserProfile(userId);
        }

        public IQueryable<UsersModel> GetAll()
        {
            return _userRepository.GetAllUsers();
        }
    }
}
