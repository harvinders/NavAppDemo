using System;
using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;

using NavAppDemo.Contracts.ViewModels;
using NavAppDemo.Core.Contracts.Services;
using NavAppDemo.Core.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NavAppDemo.ViewModels
{
    public class ContentGridDetailViewModel : ReactiveObject, INavigationAware
    {
        private readonly ISampleDataService _sampleDataService;

        [Reactive] public SampleOrder Item { get; set; }

        public ContentGridDetailViewModel(ISampleDataService sampleDataService)
        {
            _sampleDataService = sampleDataService;
        }

        public async void OnNavigatedTo(object parameter)
        {
            if (parameter is long orderID)
            {
                var data = await _sampleDataService.GetContentGridDataAsync();
                Item = data.First(i => i.OrderID == orderID);
            }
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
