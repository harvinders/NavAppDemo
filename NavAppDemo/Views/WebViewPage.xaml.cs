using System.Reactive.Disposables;
using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;

using NavAppDemo.ViewModels;
using ReactiveUI;

namespace NavAppDemo.Views
{
    public class ReactiveWebViewPage : ReactivePage<WebViewViewModel>
    {
    }

    // To learn more about WebView2, see https://docs.microsoft.com/microsoft-edge/webview2/
    public sealed partial class WebViewPage : ReactiveWebViewPage
    {

        public WebViewPage()
        {
            ViewModel = Ioc.Default.GetService<WebViewViewModel>();
            InitializeComponent();
            ViewModel.WebViewService.Initialize(webView);

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, vm => vm.Source, v => v.webView.Source)
                    .DisposeWith(disposables);

                this.OneWayBind(ViewModel, vm => vm.IsLoading, v => v.LoadingArea.Visibility)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.IsLoading, v => v.LoadingRing.IsActive)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.HasFailures, v => v.ErrorArea.Visibility)
                    .DisposeWith(disposables);

                this.BindCommand(ViewModel, vm => vm.RetryCommand, v => v.RetryButton)
                    .DisposeWith(disposables);

                this.BindCommand(ViewModel, vm => vm.ReloadCommand, v => v.ReloadButton)
                    .DisposeWith(disposables);
                this.BindCommand(ViewModel, vm => vm.BackwardCommand, v => v.BackButton)
                    .DisposeWith(disposables);
                this.BindCommand(ViewModel, vm => vm.ForwardCommand, v => v.ForwardButton)
                    .DisposeWith(disposables);
                this.BindCommand(ViewModel, vm => vm.OpenInBrowserCommand, v => v.OpenInButton)
                    .DisposeWith(disposables);
            });
        }
    }
}
