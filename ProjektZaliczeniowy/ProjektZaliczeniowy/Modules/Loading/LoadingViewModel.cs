using ProjektZaliczeniowy.Common.Base;
using ProjektZaliczeniowy.Common.Navigation;
using ProjektZaliczeniowy.Modules.Login;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ProjektZaliczeniowy.Modules.Loading
{
    public class LoadingViewModel : BaseViewModel
    {
        private INavigationService _navigationService;

        public LoadingViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override Task InitializeAsync(object parameter)
        {
            if(Preferences.ContainsKey(Constants.SHOWN_ONBOARDING))
            {
                Preferences.Set(Constants.SHOWN_ONBOARDING, true);
                //navigate to the onboardnig screen
                _navigationService.GoToLoginFlow();
                return Task.CompletedTask;
            }
            if(Preferences.ContainsKey(Constants.IS_USER_LOGGED_IN) && Preferences.Get(Constants.IS_USER_LOGGED_IN, false) == true)
            {
                //go to the wallet view
                _navigationService.GoToMainFlow();
                return Task.CompletedTask;
            }
            //navigate to the login screen
            _navigationService.GoToLoginFlow();
            return _navigationService.InsertAsRoot<LoginViewModel>();
        }
    }
}
