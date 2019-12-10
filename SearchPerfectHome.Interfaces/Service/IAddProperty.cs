using SearchPerfectHome.Models;
using SearchPerfectHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPerfectHome.Interfaces
{
    public interface IAddProperty
    {
        /// <summary>
        /// Add Property
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<PropertyViewModel> Add(PropertyViewModel model, List<PropertyImageViewModel> imageModel);
    }
}
