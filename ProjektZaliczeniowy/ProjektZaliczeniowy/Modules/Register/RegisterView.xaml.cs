using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjektZaliczeniowy.Modules.Register
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterView : ContentPage
    {
        public RegisterView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<RegisterViewModel>();
        }
    }
}