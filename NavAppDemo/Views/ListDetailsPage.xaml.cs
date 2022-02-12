using System.Reactive.Disposables;
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

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, vm => vm.SampleItems, v => v.ListDetailsViewControl.ItemsSource)
                    .DisposeWith(disposables);

                this.Bind(ViewModel, vm => vm.Selected, v => v.ListDetailsViewControl.SelectedItem)
                    .DisposeWith(disposables);
            });
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
