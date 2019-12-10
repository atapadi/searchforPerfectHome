using SearchPerfectHome.Interfaces;
using SearchPerfectHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPerfectHome.Service
{
    public class DeleteUser : IDeleteUser
    {
        private IPropertyRepository _propertyRepository;
        private IUserRepository _userRepository;

        public DeleteUser(IPropertyRepository propertyRepository, IUserRepository userRepository)
        {
            _propertyRepository = propertyRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> Delete(string userId)
        {
            bool returnValue = false;
            try
            {
                if (_propertyRepository.CheckPropertyPresent(userId))
                {
                    PropertyModel model = new PropertyModel { UserId = userId };
                    bool result = _propertyRepository.DeleteUserProperty(model);
                }

                UsersModel user = new UsersModel { UserId = userId };
                returnValue = await _userRepository.DeleteUser(user);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return returnValue;
        }
    }
}
