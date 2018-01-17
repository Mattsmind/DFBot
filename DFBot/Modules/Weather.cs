using Discord.Commands;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace DFBot.Modules
{

    [Group("weather")]
    public class Weather : ModuleBase
    {
        private string baseUrl = "http://api.openweathermap.org/data/2.5/";
        private string defaultCityQuery = $"{Program.Configuration["weather.city"]}";
        private string defaultCountryQuery = $"{Program.Configuration["weather.country"]}";
        private string defaultUnits = $"{Program.Configuration["weather.units"]}";

        private string appId = $"{Program.Configuration["weather:appid"]}";

        [Command, Alias("help")]
        public async Task DefaultWeatherCommandAsync()
        {
            await ReplyAsync("I'm Still not working.");
        }

        [Command("current")]
        public async Task GetWeatherCurrentAsync()
        {
            await ReplyAsync("I don't work either.");
        }

        [Command("forcast")]
        public async Task GetWeatherForecastAsync(string queryCity, string queryCountry, string queryUnits)
        {
            string forcast = "forcast/daily";
            
            string url = baseUrl + forcast + "?q=" + queryCity + "," + queryCountry + "&cnt=3&units=" + queryUnits + "&appid=" + appId;

            WebRequest req = WebRequest.Create(@url);
            req.Method = "GET";

            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;



            await ReplyAsync("Nope.");
        }
    }
}
