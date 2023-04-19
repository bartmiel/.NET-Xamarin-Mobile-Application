using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjektZaliczeniowy.Modules.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<LoginViewModel>();
        }
    }
}