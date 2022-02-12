using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NavAppDemo.Contracts.Services;
using NavAppDemo.Contracts.ViewModels;
using NavAppDemo.Core.Contracts.Services;
using NavAppDemo.Core.Models;
using ReactiveUI;

namespace NavAppDemo.ViewModels
{
    public class ContentGridViewModel : ReactiveObject, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly ISampleDataService _sampleDataService;

        public ReactiveCommand<SampleOrder,Unit> ItemClickCommand { get; set; }
        public ObservableCollection<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

        public ContentGridViewModel(INavigationService navigationService, ISampleDataService sampleDataService)
        {
            _navigationService = navigationService;
            _sampleDataService = sampleDataService;
            ItemClickCommand = ReactiveCommand.Create<SampleOrder, Unit>(x =>
            {
                OnItemClick(x);
                return Unit.Default;
            });
        }

        public async void OnNavigatedTo(object parameter)
        {
            Source.Clear();

            // Replace this with your actual data
            var data = await _sampleDataService.GetContentGridDataAsync();
            foreach (var item in data)
            {
                Source.Add(item);
            }
        }

        public void OnNavigatedFrom()
        {
        }

        private void OnItemClick(SampleOrder clickedItem)
        {
            if (clickedItem != null)
            {
                _navigationService.SetListDataItemForNextConnectedAnimation(clickedItem);
                _navigationService.NavigateTo(typeof(ContentGridDetailViewModel).FullName, clickedItem.OrderID);
            }
        }
    }
}
