using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;
using NavAppDemo.ViewModels;
using ReactiveUI;

namespace NavAppDemo.Views
{
    public class ReactiveMainPage : ReactivePage<MainViewModel>
    { }
    public sealed partial class MainPage : ReactiveMainPage
    {

        public MainPage()
        {
            ViewModel = Ioc.Default.GetService<MainViewModel>();
            InitializeComponent();
        }
    }
}
