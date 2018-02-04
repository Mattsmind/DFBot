using Discord.Commands;
using System.Threading.Tasks;
using System.Net;
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using Discord;

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
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Weather Help")
                .WithColor(Color.DarkOrange)
                .WithDescription("__**;weather current**__ _<city>_ _<country/country code>_ : " +
                " Get the current weather for _city_ in _country_. If not provided, default location will be used." +
                "\n\n__**;weather forecast**__ _<city>_  _<country/country code>_ : " +
                " Gets a 9 hour forecast for given _city_ in _country_. With no city or country provided, will get data for " +
                "the default city set in the botconfig.json file.");

            await ReplyAsync("", false, builder.Build());
        }

        [Command("current"), Alias("cw")]
        [Summary("Gets the current weather information for a given area.")]
        public async Task GetWeatherCurrentAsync(string city = null, string country = null)
        {
            EmbedBuilder builder = new EmbedBuilder();

            string reqType = "weather";
            string additionalParams = "";

            JObject _json = JObject.Parse(weatherData.GetWeatherData(reqType, additionalParams, city, country));

            string cwCity = (string)_json.SelectToken("name");
            string cwCountry = (string)_json.SelectToken("sys.country");
            string temp = (string)_json.SelectToken("main.temp");
            string tempMax = (string)_json.SelectToken("main.temp_max");
            string tempMin = (string)_json.SelectToken("main.temp_min");
            string condition = (string)_json.SelectToken("weather[0].description");
            DateTime sunrise = weatherData.ConvertUnixTimestampToDateTime((double)_json.SelectToken("sys.sunrise"));
            DateTime sunset = weatherData.ConvertUnixTimestampToDateTime((double)_json.SelectToken("sys.sunset"));
            DateTime date = weatherData.ConvertUnixTimestampToDateTime((double)_json.SelectToken("dt"));

            builder.WithTitle($"WEATHER: {date.ToShortDateString()}")
                .WithDescription($"Weather for {cwCity}, {cwCountry}")
                .AddInlineField("Temprature", $"**Temp:** {temp} °F\n**High:** {tempMax} °F\n**Low:** {tempMin} °F")
                .AddInlineField("Other Info", $"**Condition:** {condition}\n**Sunrise:** {sunrise.ToShortTimeString()}\n**Sunset:** {sunset.ToShortTimeString()}")
                .WithColor(Color.Red);

            await ReplyAsync("", false, builder.Build());
        }

        [Command("forecast"), Alias("forcast", "fc")]
        [Summary("Gets a 3 day weather forecast for a given area.")]
        public async Task GetWeatherForecastAsync(string city = null, string country = null)
        {
            EmbedBuilder builder = new EmbedBuilder();

            string reqType = "forecast";
            string additionalParams = "&cnt=3";

            JObject _json = JObject.Parse(weatherData.GetWeatherData(reqType, additionalParams, city, country));

            string fcCity = (string)_json.SelectToken("city.name");
            string fcCountry = (string)_json.SelectToken("city.country");
            string[] tempMax = new string [3];
            string[] tempMin = new string[3];
            string[] conditions = new string[3];
            DateTime[] timeOfDay = new DateTime[3];

            for (int i = 0; i <= 2; i++)
            {
                tempMax[i] = (string)_json.SelectToken($"list[{i}].main.temp_max");
                tempMin[i] = (string)_json.SelectToken($"list[{i}].main.temp_min");
                conditions[i] = (string)_json.SelectToken($"list[{i}].weather[0].description");
                timeOfDay[i] = weatherData.ConvertUnixTimestampToDateTime((double)_json.SelectToken($"list[{i}].dt"));
            }

            builder.WithTitle("WEATHER FORECAST")
                .WithDescription($"YOUR 9 HOUR FORECAST FOR {fcCity.ToUpper()}, {fcCountry.ToUpper()}")
                .AddInlineField($"{timeOfDay[0].ToShortTimeString()}", $"**High Temp**: {tempMax[0]} °F\n**Low Temp**: {tempMin[0]} °F\n**Conditions**: {conditions[0]}")
                .AddInlineField($"{timeOfDay[1].ToShortTimeString()}", $"**High Temp**: {tempMax[1]} °F\n**Low Temp**: {tempMin[1]} °F\n**Conditions**: {conditions[1]}")
                .AddInlineField($"{timeOfDay[2].ToShortTimeString()}", $"**High Temp**: {tempMax[2]} °F\n**Low Temp**: {tempMin[2]} °F\n**Conditions**: {conditions[2]}")
                .WithColor(Color.LightOrange);

            await ReplyAsync("", false, builder.Build());
        }
    }

    public class WeatherDataHandler
    {

        private string defaultCity = Program.Configuration["weather:city"];
        private string defaultCountry = Program.Configuration["weather:country"];

        private string units = Program.Configuration["weather:units"];
        private string baseUrl = "http://api.openweathermap.org/data/2.5/";
        private string appId = Program.Configuration["weather:appid"];

        public string GetWeatherData(string reqType, string additionalParams, string city = null, string country = null)
        {
            string url = URLBuilder(reqType, additionalParams, city, country);
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

        public DateTime ConvertUnixTimestampToDateTime(double unixTimeStamp)
        {
            DateTime covertedTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            covertedTime = covertedTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return covertedTime;
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
