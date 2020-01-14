using Newtonsoft.Json;

namespace WeatherAppExample.Models
{
    public class SteamChecker
    {
        [JsonProperty("steamid")]
        public long Id { get; set; }
    }
}