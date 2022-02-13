using System.Reactive.Disposables;
using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

using NavAppDemo.Contracts.Services;
using NavAppDemo.ViewModels;

using Windows.System;
using ReactiveUI;

namespace NavAppDemo.Views
{
    public class ReactiveShellPage : ReactivePage<ShellViewModel>
    {
    }

    // TODO WTS: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    public sealed partial class ShellPage : ReactiveShellPage
    {
        private readonly KeyboardAccelerator _altLeftKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu);
        private readonly KeyboardAccelerator _backKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.GoBack);

        public ShellViewModel ViewModel { get; }

        public ShellPage(ShellViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            ViewModel.NavigationService.Frame = shellFrame;
            ViewModel.NavigationViewService.Initialize(navigationView);

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, vm => vm.IsBackEnabled, v => v.navigationView.IsBackEnabled)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Selected, v => v.navigationView.SelectedItem)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Selected, v => v.navigationView.Header, x=>((ContentControl)x).Content)
                    .DisposeWith(disposables);
            });
        }

        private void OnLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            // Keyboard accelerators are added here to avoid showing 'Alt + left' tooltip on the page.
            // More info on tracking issue https://github.com/Microsoft/microsoft-ui-xaml/issues/8
            KeyboardAccelerators.Add(_altLeftKeyboardAccelerator);
            KeyboardAccelerators.Add(_backKeyboardAccelerator);
        }

        private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
        {
            var keyboardAccelerator = new KeyboardAccelerator() { Key = key };
            if (modifiers.HasValue)
            {
                keyboardAccelerator.Modifiers = modifiers.Value;
            }

            keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;
            return keyboardAccelerator;
        }

        private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            var navigationService = Ioc.Default.GetService<INavigationService>();
            var result = navigationService.GoBack();
            args.Handled = result;
        }
    }
}
