using RestEase;
using System.Threading.Tasks;
using WeatherAppExample.Models;

namespace WeatherAppExample.Api
{
    public interface ISteamChecker
    {
        [Get]
        Task<SteamChecker> GetSteam([Query("key")]string api,
            string steamids);


    }
}