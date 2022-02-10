using System.Collections.Generic;
using System.Threading.Tasks;

using NavAppDemo.Core.Models;

namespace NavAppDemo.Core.Contracts.Services
{
    // Remove this class once your pages/features are using your data.
    public interface ISampleDataService
    {
        Task<IEnumerable<SampleOrder>> GetContentGridDataAsync();

        Task<IEnumerable<SampleOrder>> GetGridDataAsync();

        Task<IEnumerable<SampleOrder>> GetListDetailsDataAsync();
    }
}
