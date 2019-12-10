using SearchPerfectHome.Models;
using System.Threading.Tasks;

namespace SearchPerfectHome.Interfaces
{
    public interface IUpdateProperty
    {
        Task<string> Update(PropertyModel viewModel);
    }
}
