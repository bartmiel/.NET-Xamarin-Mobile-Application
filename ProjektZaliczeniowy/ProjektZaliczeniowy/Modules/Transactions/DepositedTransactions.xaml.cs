using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjektZaliczeniowy.Modules.Transactions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DepositedTransactions : ContentPage
    {
        public DepositedTransactions()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<TransactionsViewModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as TransactionsViewModel).InitializeAsync(Constants.TRANSACTION_DEPOSITED);
        }
    }
}