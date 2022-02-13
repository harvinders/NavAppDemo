using System;
using System.Reactive.Disposables;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.WinUI.UI.Animations;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

using NavAppDemo.Contracts.Services;
using NavAppDemo.ViewModels;
using ReactiveUI;

namespace NavAppDemo.Views
{
    public class ReactiveContentGridDetailPage : ReactivePage<ContentGridDetailViewModel>
    {
    }

    public sealed partial class ContentGridDetailPage : ReactiveContentGridDetailPage
    {

        public ContentGridDetailPage()
        {
            ViewModel = Ioc.Default.GetService<ContentGridDetailViewModel>();
            InitializeComponent();
            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, vm => vm.Item.Symbol, v => v.FontIcon.Glyph)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Item.Company, v => v.title.Text)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Item.Status, v => v.Status.Text)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Item.OrderDate, v => v.OrderDate.Text)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Item.ShipTo, v => v.ShipTo.Text)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Item.OrderTotal, v => v.OrderTotal.Text)
                    .DisposeWith(disposables);
            });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.RegisterElementForConnectedAnimation("animationKeyContentGrid", itemHero);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                var navigationService = Ioc.Default.GetService<INavigationService>();
                navigationService.SetListDataItemForNextConnectedAnimation(ViewModel.Item);
            }
        }
    }
}
