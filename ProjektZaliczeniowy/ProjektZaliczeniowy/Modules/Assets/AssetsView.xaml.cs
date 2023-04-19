using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjektZaliczeniowy.Modules.Assets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssetsView : ContentPage
    {
        public AssetsView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<AssetsViewModel>();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as AssetsViewModel).InitializeAsync(null);
        }
    }
}