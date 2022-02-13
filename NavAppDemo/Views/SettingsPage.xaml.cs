using System.Reactive.Disposables;
using System.Reactive.Linq;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using NavAppDemo.ViewModels;
using ReactiveUI;

namespace NavAppDemo.Views
{
    public class ReactiveSettingPage : ReactivePage<SettingsViewModel>
    {

    }
    // TODO WTS: Change the URL for your privacy policy, currently set to https://YourPrivacyUrlGoesHere
    public sealed partial class SettingsPage : ReactiveSettingPage
    {
        //public SettingsViewModel ViewModel { get; }

        public SettingsPage()
        {
            ViewModel = Ioc.Default.GetService<SettingsViewModel>();
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, vm => vm.ElementTheme, v => v.LightRadioButton.IsChecked, x => x == ElementTheme.Light)
                    .DisposeWith(disposables);

                this.BindCommand(ViewModel, vm => vm.SwitchThemeCommand, v => v.LightRadioButton, Observable.Return(ElementTheme.Light))
                    .DisposeWith(disposables);

                this.OneWayBind(ViewModel, vm => vm.ElementTheme, v => v.DarkRadioButton.IsChecked, x => x == ElementTheme.Dark)
                    .DisposeWith(disposables);

                this.BindCommand(ViewModel, vm => vm.SwitchThemeCommand, v => v.DarkRadioButton, Observable.Return(ElementTheme.Dark))
                    .DisposeWith(disposables);

                this.OneWayBind(ViewModel, vm => vm.ElementTheme, v => v.DefaultRadioButton.IsChecked, x => x == ElementTheme.Default)
                    .DisposeWith(disposables);

                this.BindCommand(ViewModel, vm => vm.SwitchThemeCommand, v => v.DefaultRadioButton, Observable.Return(ElementTheme.Default))
                    .DisposeWith(disposables);

                this.OneWayBind(ViewModel, vm => vm.VersionDescription, v => v.Version.Text)
                    .DisposeWith(disposables);
            });
        }
    }
}
