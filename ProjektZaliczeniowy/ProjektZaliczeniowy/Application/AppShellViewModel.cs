using ProjektZaliczeniowy.Common.Navigation;
using ProjektZaliczeniowy.Modules.Login;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProjektZaliczeniowy
{
    public class AppShellViewModel
    {
        private INavigationService _navigationService;

        public AppShellViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public ICommand SignOutCommand
        {
            get => new Command(async () => await SignOut());
        }
        private async Task SignOut()
        {
            Preferences.Remove(Constants.IS_USER_LOGGED_IN);
            _navigationService.GoToLoginFlow();
            await _navigationService.InsertAsRoot<LoginViewModel>();
        }
    }
}
