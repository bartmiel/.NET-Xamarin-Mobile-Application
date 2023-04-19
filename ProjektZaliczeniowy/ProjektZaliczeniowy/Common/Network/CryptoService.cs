using ProjektZaliczeniowy.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

//Polaczenie z API CoinGecko - aktualny kurs walut
namespace ProjektZaliczeniowy.Common.Network
{
    public interface ICryptoService
    {
        Task<List<Coin>> GetLatestPrices();
    }
    public class CryptoService : ICryptoService
    {
        private INetworkService _networkService;
        private const string PRICES_ENDPOINT = "simple/price?ids=bitcoin%2Cbitcoin-cash%2Cdash%2Cethereum%2Ceos%2Clitecoin%2Cmonero%2Cripple%2Cstellar&vs_currencies=usd";

        public CryptoService(INetworkService networkService)
        {
            _networkService = networkService;
        }
        public async Task<List<Coin>> GetLatestPrices()
        {
            var url = Constants.CRYPTO_API + PRICES_ENDPOINT;
            var result = await _networkService.GetAsync<Dictionary<string, Dictionary<string, double?>>>(url);
            var coins = Coin.GetAvailableAssets();
            foreach(var item in coins)
            {
                Dictionary<string, double?> coinPrices = result[item.Name.Replace(' ', '-').ToLower()];
                double? coinPrice = coinPrices["usd"];
                item.Price = coinPrice.HasValue ? coinPrice.Value : 0;
            }
            return coins;
        }
    }
}
