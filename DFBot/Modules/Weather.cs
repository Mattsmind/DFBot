using Discord.Commands;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net;
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace DFBot.Modules
{

    [Group("weather")]
    [Summary("A collection of tools to get weather information from openweathermap.org")]
    public class Weather : ModuleBase<SocketCommandContext>
    {
        private string baseUrl = "http://api.openweathermap.org/data/2.5/";
        private string defaultCityQuery = $"{Program.Configuration["weather:city"]}";
        private string defaultCountryQuery = $"{Program.Configuration["weather:country"]}";
        private string defaultUnits = $"{Program.Configuration["weather:units"]}";

        private string appId = $"{Program.Configuration["weather:appid"]}";

        [Command, Alias("help")]
        [Summary("Provides the help information for the weather module.")]
        public async Task DefaultWeatherCommandAsync()
        {
            await ReplyAsync("I'm Still not working.");
        }

        [Command("current")]
        [Summary("Gets the current weather information for a given area.")]
        public async Task GetWeatherCurrentAsync()
        {
            await ReplyAsync("I don't work either.");
        }

        [Command("forecast")]
        [Summary("Gets a 3 day weather forecast for a given area.")]
        public async Task GetWeatherForecastAsync()
        {
            string url = $"{baseUrl}forecast?q={defaultCityQuery},{defaultCountryQuery}&cnt=1&units={defaultUnits}&APPID={appId}";
           // Console.WriteLine(url);
            WebRequest req = WebRequest.Create(@url);
            req.Method = "GET";

            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            if (resp.StatusCode == HttpStatusCode.OK )
            {
                using (Stream respStream = resp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                    string _json = reader.ReadToEnd();
                    JObject result = JObject.Parse(_json);

                    await ReplyAsync($"`{result}`");
                }
            }
            else
            {
                Console.WriteLine(string.Format("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription));
            }

        }
    }
}
