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

        [Command("forecast"), Alias("forcast", "fc")]
        [Summary("Gets a 3 day weather forecast for a given area.")]
        public async Task GetWeatherForecastAsync(string city = null, string country = null)
        {
            string reqType = "forecast";
            string additionalParams = "&cnt=1";

            await ReplyAsync($"`{weatherData.GetWeatherData(reqType, additionalParams, city, country)}`");

        }
    }

    public class WeatherDataHandler
    {

        private string defaultCity = Program.Configuration["weather:city"];
        private string defaultCountry = Program.Configuration["weather:country"];

        private string units = $"{Program.Configuration["weather:units"]}";
        private string baseUrl = "http://api.openweathermap.org/data/2.5/";
        private string appId = $"{Program.Configuration["weather:appid"]}";

        public string GetWeatherData(string reqType, string additionalParams, string city = null, string country = null)
        {
            string url = URLBuilder(reqType, additionalParams, city, country);
            Console.WriteLine(url);
            WebRequest req = WebRequest.Create(@url);
            req.Method = "GET";

            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                using (Stream respStream = resp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                    string _json = reader.ReadToEnd();
                    JObject result = JObject.Parse(_json);

                    return result.ToString();
                }
            }
            else
            {
                Console.WriteLine(string.Format("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription));
                return "ERROR: Something went wrong!";
            }
        }

        private string URLBuilder(string requestType, string additionalParams = "", string city = null, string countryCode = null)
        {
            string cityUrlSeg;
            string countryUrlSeg;

            if (countryCode != null)
            {
                cityUrlSeg = city;
                countryUrlSeg = countryCode;
            }
            else if (city != null)
            {
                cityUrlSeg = city;
                countryUrlSeg = "";
            }
            else
            {
                cityUrlSeg = defaultCity;
                countryUrlSeg = "," + defaultCountry;
            }

            string url = $"{baseUrl}{requestType}?q={cityUrlSeg}{countryUrlSeg}&units={units}{additionalParams}&APPID={appId}";
            return url;
        }
    }
}
