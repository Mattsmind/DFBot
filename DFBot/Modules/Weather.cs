using Discord.Commands;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DFBot.Modules
{

    [Group("weather")]
    public class Weather : ModuleBase
    {
        string baseUrl = "http://api.openweathermap.org/data/2.5/";
        string defaultCityQuery = $"{Program.Configuration["weather.city"]}";
        string defaultCountryQuery = $"{Program.Configuration["weather.country"]}";
        string defaultUnits = $"{Program.Configuration["weather.units"]}";

        string appId = $"{Program.Configuration["weather:appid"]}";

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
        public async Task GetWeatherForecastAsync(string queryCity)
        {
            string forcast = "forcast/daily";
            
            string url = baseUrl + forcast + "?q=" + queryCity;


            await ReplyAsync("Nope.");
        }
    }
}
