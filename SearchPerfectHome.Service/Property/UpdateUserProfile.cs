using SearchPerfectHome.Interfaces;
using SearchPerfectHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPerfectHome.Service
{
    public class UpdateUserProfile : IUpdateUserProfile
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserProfile(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Update(UsersModel model)
        {
            string errorMessage = "";
            if(model.Id != 0)
            {
                if (await _userRepository.Update(model))
                    errorMessage = "User updated successfully";
                else
                    errorMessage = "Error while updating.";
            }
            else
            {
                if (await _userRepository.Add(model))
                    errorMessage = "User Added succesfully.";
                else
                    errorMessage = "Error while adding user profile.";
            }
            return errorMessage;
        }
    }
}
