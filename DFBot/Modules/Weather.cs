using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.Modules
{
    [DataContract(Name = "main")]
    internal class WeatherAPIJson
    {
        [DataMember(Name = "placeholder1")]
        public string Placeholder1 { get; set; }

        [DataMember(Name = "placeholder2")]
        public string Placeholder2 { get; set; }

    }

    [Group("weather")]
    public class Weather : ModuleBase
    {
        [Command, Alias("help")]
        public async Task DefaultWeatherCommandAsync()
        {
            await ReplyAsync("Default");
        }

        [Command("current")]
        public async Task GetWeatherCurrentAsync()
        {
            await ReplyAsync("Get Current Weather");
        }

        [Command("forcast")]
        public async Task GetWeatherForecastAsync()
        {
            await ReplyAsync("Get Weather Forecast");
        }
    }
}
