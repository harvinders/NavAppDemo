using System.Reactive.Disposables;
using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;

using NavAppDemo.ViewModels;
using ReactiveUI;

namespace NavAppDemo.Views
{
    // TODO WTS: Change the grid as appropriate to your app, adjust the column definitions on DataGridPage.xaml.
    // For more details see the documentation at https://docs.microsoft.com/windows/communitytoolkit/controls/datagrid
    public class ReactiveDataGridPage : ReactivePage<DataGridViewModel>
    {
    }

    public sealed partial class DataGridPage : ReactiveDataGridPage
    {

        public DataGridPage()
        {
            ViewModel = Ioc.Default.GetService<DataGridViewModel>();
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, vm => vm.Source, v => v.DataGrid.ItemsSource)
                    .DisposeWith(disposables);
            });
        }
    }
}
