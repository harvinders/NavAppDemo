using System.Reactive.Disposables;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using NavAppDemo.Core.Models;
using ReactiveUI;

namespace NavAppDemo.Views
{
    public class ListDetailsDetailReactiveControl : ReactiveUserControl<SampleOrder>
    {
    }

    public sealed partial class ListDetailsDetailControl : ListDetailsDetailReactiveControl
    {
        public ListDetailsDetailControl()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, vm => vm.Symbol, v => v.FontIcon.Glyph)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Company, v => v.CompanyHeader.Text)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Status, v => v.Status.Text)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.OrderDate, v => v.OrderDate.Text)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Company, v => v.Company.Text)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.ShipTo, v => v.ShipTo.Text)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.OrderTotal, v => v.OrderTotal.Text)
                    .DisposeWith(disposables);
            });
        }

        private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ListDetailsDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
