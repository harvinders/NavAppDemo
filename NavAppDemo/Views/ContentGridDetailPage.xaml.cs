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
