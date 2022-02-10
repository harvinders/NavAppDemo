using System;
using System.Collections.ObjectModel;
using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;

using NavAppDemo.Contracts.ViewModels;
using NavAppDemo.Core.Contracts.Services;
using NavAppDemo.Core.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NavAppDemo.ViewModels
{
    public class ListDetailsViewModel : ReactiveObject, INavigationAware
    {
        private readonly ISampleDataService _sampleDataService;

        [Reactive] public SampleOrder Selected { get; set; }

        public ObservableCollection<SampleOrder> SampleItems { get; private set; } = new ObservableCollection<SampleOrder>();

        public ListDetailsViewModel(ISampleDataService sampleDataService)
        {
            _sampleDataService = sampleDataService;
        }

        public async void OnNavigatedTo(object parameter)
        {
            SampleItems.Clear();

            // Replace this with your actual data
            var data = await _sampleDataService.GetListDetailsDataAsync();

            foreach (var item in data)
            {
                SampleItems.Add(item);
            }
        }

        public void OnNavigatedFrom()
        {
        }

        public void EnsureItemSelected()
        {
            if (Selected == null)
            {
                Selected = SampleItems.First();
            }
        }
    }
}
