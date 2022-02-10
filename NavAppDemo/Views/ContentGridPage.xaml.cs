using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;

using NavAppDemo.ViewModels;
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
        }
    }
}
