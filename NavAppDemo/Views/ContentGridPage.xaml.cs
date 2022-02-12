using System.Reactive.Disposables;
using System.Reactive.Linq;
using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;

using NavAppDemo.ViewModels;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;

namespace NavAppDemo.Views
{
    public class ReactiveContentGridPage : ReactivePage<ContentGridViewModel>
    {
    }

    public sealed partial class ContentGridPage : ReactiveContentGridPage
    {

        public ContentGridPage()
        {
            ViewModel = Ioc.Default.GetService<ContentGridViewModel>();
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, vm => vm.Source, v => v.GridView.ItemsSource)
                    .DisposeWith(disposables);

                this.GridView.Events().ItemClick.Select(x => x.ClickedItem)
                    .InvokeCommand(this, x => x.ViewModel.ItemClickCommand).DisposeWith(disposables);
                //this.BindCommand(ViewModel, vm => vm.ItemClickCommand, v => v.GridView.ItemClickCommand)
                //    .DisposeWith(disposables);
            });
        }
    }
}
