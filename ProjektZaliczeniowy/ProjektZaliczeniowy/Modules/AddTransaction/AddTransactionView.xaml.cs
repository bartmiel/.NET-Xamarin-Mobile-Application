using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjektZaliczeniowy.Modules.AddTransaction
{
    [QueryProperty("Id", "id")]
    public partial class AddTransactionView : ContentPage
    {
        public AddTransactionView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<AddTransactionViewModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as AddTransactionViewModel).InitializeAsync(null);
        }
    }
}