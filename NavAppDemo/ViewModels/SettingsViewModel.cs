using System;
using System.Reactive;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Windows.ApplicationModel;
using NavAppDemo.Contracts.Services;
using NavAppDemo.Helpers;

namespace NavAppDemo.ViewModels
{
    public class SettingsViewModel : ReactiveObject
    {
        public string VersionDescription { get; init; }
        [Reactive] public ElementTheme ElementTheme { get; set; }
        public ReactiveCommand<ElementTheme, Unit> SwitchThemeCommand { get; set; }


        public SettingsViewModel(IThemeSelectorService themeSelectorService)
        {
            ElementTheme = themeSelectorService.Theme;
            VersionDescription = GetVersionDescription();

            SwitchThemeCommand = ReactiveCommand.CreateFromTask<ElementTheme>(themeSelectorService.SetThemeAsync);
        }

        private string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}
