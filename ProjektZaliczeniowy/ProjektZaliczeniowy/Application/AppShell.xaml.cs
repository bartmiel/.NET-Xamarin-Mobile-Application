using Autofac;
using ProjektZaliczeniowy.Modules.AddTransaction;
using Xamarin.Forms;

namespace ProjektZaliczeniowy
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<AppShellViewModel>();

            Routing.RegisterRoute("AddTransactionViewModel", typeof(AddTransactionView));
        }
    }
}