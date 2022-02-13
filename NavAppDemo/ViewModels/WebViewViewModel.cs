using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Web.WebView2.Core;

using NavAppDemo.Contracts.Services;
using NavAppDemo.Contracts.ViewModels;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NavAppDemo.ViewModels
{
    // TODO WTS: Review best practices and distribution guidelines for apps using WebView2
    // https://docs.microsoft.com/microsoft-edge/webview2/concepts/developer-guide
    // https://docs.microsoft.com/microsoft-edge/webview2/concepts/distribution
    //
    // You can also read more about WebView2 control at
    // https://docs.microsoft.com/microsoft-edge/webview2/get-started/winui
    public class WebViewViewModel : ReactiveObject, INavigationAware
    {
        // TODO WTS: Set the URI of the page to show by default
        private const string DefaultUrl = "https://docs.microsoft.com/windows/apps/";
        private ICommand _browserBackCommand;
        private ICommand _browserForwardCommand;


        public IWebViewService WebViewService { get; }

        [Reactive] public Uri Source { get; set; }

        [Reactive] public bool IsLoading { get; set; } = true;

        [Reactive] public bool HasFailures { get; set; }

        //public ICommand BrowserBackCommand => _browserBackCommand ?? (_browserBackCommand = new RelayCommand(
        //    () => WebViewService?.GoBack(), () => WebViewService?.CanGoBack ?? false));

        //public ICommand BrowserForwardCommand => _browserForwardCommand ?? (_browserForwardCommand = new RelayCommand(
        //    () => WebViewService?.GoForward(), () => WebViewService?.CanGoForward ?? false));

        public ReactiveCommand<Unit, Unit> ForwardCommand { get; set; }
        public ReactiveCommand<Unit, Unit> BackwardCommand { get; set; }

        public ReactiveCommand<Unit, Unit> RetryCommand { get; set; }
        public ReactiveCommand<Unit, Unit> ReloadCommand { get; set; }
        public ReactiveCommand<Unit, bool> OpenInBrowserCommand { get; set; }

        [Reactive]public bool CanGoBackward { get; set; }
        [Reactive] public bool CanGoForward { get; set; }

        public WebViewViewModel(IWebViewService webViewService)
        {
            WebViewService = webViewService;
            RetryCommand = ReactiveCommand.Create(OnRetry);
            ReloadCommand = ReactiveCommand.Create(() => WebViewService?.Reload());
            OpenInBrowserCommand = ReactiveCommand.CreateFromTask<Unit,bool>(async _ => await Windows.System.Launcher.LaunchUriAsync(Source));
            ForwardCommand = ReactiveCommand.Create(() => WebViewService?.GoForward(), this.WhenAnyValue(x=>x.CanGoForward));
            BackwardCommand = ReactiveCommand.Create(() => WebViewService?.GoBack(), this.WhenAnyValue(x => x.CanGoBackward));
        }

        public void OnNavigatedTo(object parameter)
        {
            WebViewService.NavigationCompleted += OnNavigationCompleted;
            Source = new Uri(DefaultUrl);
        }

        public void OnNavigatedFrom()
        {
            WebViewService.UnregisterEvents();
            WebViewService.NavigationCompleted -= OnNavigationCompleted;
        }

        private void OnNavigationCompleted(object sender, CoreWebView2WebErrorStatus webErrorStatus)
        {
            IsLoading = false;
            CanGoBackward = WebViewService.CanGoBack;
            CanGoForward = WebViewService.CanGoForward;
 
            if (webErrorStatus != default)
            {
                // Use `webErrorStatus` to vary the displayed message based on the error reason
                HasFailures = true;
            }
        }

        private void OnRetry()
        {
            HasFailures = false;
            IsLoading = true;
            WebViewService?.Reload();
        }
    }
}
