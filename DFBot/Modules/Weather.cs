using Discord.Commands;
using System.Threading.Tasks;
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
        private WeatherDataHandler weatherData = new WeatherDataHandler();

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
        public async Task GetWeatherForecastAsync(string city = null, string country = null)
        {
            string reqType = "forecast";
            string additionalParams = "&cnt=1";

            WebRequest req = WebRequest.Create(@weatherData.URLBuilder(reqType, city, country, additionalParams));
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

    public class WeatherDataHandler
    {

        public string defaultCity = Program.Configuration["weather:city"];
        public string defaultCountry = Program.Configuration["weather:country"];

        private string units = $"{Program.Configuration["weather:units"]}";
        private string baseUrl = "http://api.openweathermap.org/data/2.5/";
        private string appId = $"{Program.Configuration["weather:appid"]}";

        public string URLBuilder(string reuqestType, string city = null, string countryCode = null, string additionalParams = null)
        {
            string cityUrlSeg;
            string countryUrlSeg;

            if (city == null)
            {
                cityUrlSeg = defaultCity;
            }
            else
            {
                cityUrlSeg = city;
            }

            if (countryCode == null)
            {
                countryUrlSeg = defaultCountry;
            }
            else
            {
                countryUrlSeg = countryCode;
            }

            string url = $"{baseUrl}{reuqestType}?q={cityUrlSeg},{countryUrlSeg}&units={units}{additionalParams}&APPID={appId}";
            return url;
        }
    }
}
