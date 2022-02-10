using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.WinUI.UI.Controls;

using Microsoft.UI.Xaml.Controls;

using NavAppDemo.ViewModels;
using ReactiveUI;

namespace NavAppDemo.Views
{
    public class ReactiveListDetailsPage:ReactivePage<ListDetailsViewModel> 
    {
    }

    public sealed partial class ListDetailsPage : ReactiveListDetailsPage
    {

        public ListDetailsPage()
        {
            ViewModel = Ioc.Default.GetService<ListDetailsViewModel>();
            InitializeComponent();
        }

        private void OnViewStateChanged(object sender, ListDetailsViewState e)
        {
            if (e == ListDetailsViewState.Both)
            {
                ViewModel.EnsureItemSelected();
            }
        }
    }
}
