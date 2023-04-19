using Microcharts;
using ProjektZaliczeniowy.Common.Base;
using ProjektZaliczeniowy.Common.Controllers;
using ProjektZaliczeniowy.Common.Models;
using ProjektZaliczeniowy.Common.Navigation;
using ProjektZaliczeniowy.Modules.AddTransaction;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjektZaliczeniowy.Modules.Wallet
{
    public class WalletViewModel: BaseViewModel
    {
        private IWalletController _walletController;
        private INavigationService _navigationService;

        public WalletViewModel(IWalletController walletController, INavigationService navigationService)
        {
            _walletController = walletController;
            _navigationService = navigationService;
            Assets = new ObservableCollection<Coin>();
            LatestTransactions = new ObservableCollection<Transaction>();
        }

        public override async Task InitializeAsync(object parameter)
        {
            bool reload = (bool)parameter;
            if(IsBusy)
            {
                return;
            }
            IsBusy = true;
            IsRefreshing = true;
            var transactions = await _walletController.GetTransactions(reload);
            LatestTransactions = new ObservableCollection<Transaction>(transactions.Take(5));

            var assets = await _walletController.GetCoins(reload);
            Assets = new ObservableCollection<Coin>(assets.Take(3));
            BuildChart(assets);
            PortfolioValue = assets.Sum(x => x.DollarValue);

            IsRefreshing = false;
            IsBusy = false;
        }

        private void BuildChart(List<Coin> assets)
        {
            var whiteColor = SKColor.Parse("#FFFFFF");
            List<ChartEntry> entries = new List<ChartEntry>();
            var colors = Coin.GetAvailableAssets();
            foreach (var item in assets)
            {
                entries.Add(new ChartEntry((float)item.DollarValue)
                {
                    TextColor = whiteColor,
                    ValueLabel = item.Name,
                    Color = SKColor.Parse(colors.First(x => x.Symbol == item.Symbol).HexColor)
                });
            }
            var chart = new DonutChart { Entries = entries };
            chart.BackgroundColor = whiteColor;
            PortfolioView = chart;
        }

        private Chart _portfolioView;
        public Chart PortfolioView
        {
            get => _portfolioView;
            set
            {
                SetProperty(ref _portfolioView, value);
            }
        }

        private int _coinsHeight;
        public int CoinsHeight
        {
            get => _coinsHeight;
            set
            {
                SetProperty(ref _coinsHeight, value);
            }
        }

        private ObservableCollection<Coin> _assets;
        public ObservableCollection<Coin> Assets
        {
            get => _assets;
            set
            {
                SetProperty(ref _assets, value);
                if(_assets == null)
                {
                    return;
                }
                CoinsHeight = _assets.Count * 85;
            }
        }

        private int _transactionsHeight;
        public int TransactionsHeight
        {
            get => _transactionsHeight;
            set
            {
                SetProperty(ref _transactionsHeight, value);
            }
        }


        private ObservableCollection<Transaction> _latestTransactions;
        public ObservableCollection<Transaction> LatestTransactions
        {
            get => _latestTransactions;
            set
            {
                SetProperty(ref _latestTransactions, value);
                if(_latestTransactions == null)
                {
                    return;
                }
                HasTransactions = _latestTransactions.Count > 0;
                if(_latestTransactions.Count == 0)
                {
                    TransactionsHeight = 430;
                    return;
                }
                TransactionsHeight = _latestTransactions.Count * 85;
            }
        }

        public ICommand AddNewTransactionCommand
        {
            get => new Command(async () => await AddNewTransaction());
        }

        private async Task AddNewTransaction()
        {
            await _navigationService.PushAsync<AddTransactionViewModel>();
        }
        private bool _hasTransactions;
        public bool HasTransactions
        {
            get => _hasTransactions;
            set
            {
                SetProperty(ref _hasTransactions, value);
            }
        }

        private decimal _portfolioValue;
        public decimal PortfolioValue
        {
            get => _portfolioValue;
            set
            {
                SetProperty(ref _portfolioValue, value);
            }
        }
        
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }

        public ICommand RefreshAssetsCommand
        {
            get => new Command(async () => await InitializeAsync(true));
        }
    }
}
