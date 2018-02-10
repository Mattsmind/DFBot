using Discord;
using Discord.Commands;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.Modules
{
    [Group("cryptotools"), Alias("ct", "bitcoin", "litecoin", "etherium")]
    [Summary("A set of tools used to retrieve information on various cryptocurrencies.")]
    public class CryptoCurrencyTools : ModuleBase<SocketCommandContext>
    {
        [Command, Alias("help")]
        public async Task CryptoHelpAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Cryptocurrency Tools Help")
                .WithColor(Color.Blue)
                .WithDescription("Still working on it.");

            await ReplyAsync("", false, builder.Build());
        }

        [Command("ticker"), Alias("pc", "price", "pricecheck")]
        [Summary("Returns ticker information.")]
        public async Task BitstampTickerCheckAsync(string currencyPair = "btcusd")
        {
            EmbedBuilder builder = new EmbedBuilder();
            BitstampDataHandler _handler = new BitstampDataHandler();

            JObject _json = _handler.GetSpotPriceData(currencyPair);

            string priceHigh = (string)_json.SelectToken("high");
            string priceLow = (string)_json.SelectToken("low");
            string priceBid = (string)_json.SelectToken("bid");
            string priceAsk = (string)_json.SelectToken("ask");
            string priceOpen = (string)_json.SelectToken("open");
            string priceLast = (string)_json.SelectToken("last");

            builder.WithTitle($"BitStamp Ticker: {currencyPair.ToUpper()}")
                .AddInlineField($"ASK: \t${priceAsk}", $"**HIGH:** \t${priceHigh}\n**OPEN:** \t${priceOpen}")
                .AddInlineField($"BID: \t${priceBid}", $"**LOW:** \t${priceLow}\n**LAST:** \t${priceLast}")
                .WithCurrentTimestamp()
                .WithColor(Color.Blue);

            await ReplyAsync("", false, builder.Build());
        }
    }

    class BitstampDataHandler
    {
        public JObject GetSpotPriceData(string currencyPair)
        {
            string url = $"https://www.bitstamp.net/api/v2/ticker_hour/{currencyPair}/";
            WebRequest req = WebRequest.Create(@url);
            req.Method = "GET";

            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                using (Stream respStream = resp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                    string jString = reader.ReadToEnd();
                    JObject _json = JObject.Parse(jString);

                    return _json;
                }
            }
            else
            {
                return null;
            }
        }
    }
    
    
    /*   class CoinbaseDataHandler
    {
        private static readonly HttpClient _client = new HttpClient();

        public async Task<List<CoinbaseData>> ProcessCoinbaseData(string currencyPair)
        {
            var serializer = new DataContractJsonSerializer(typeof(List<CoinbaseData>));
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var _json = _client.GetStreamAsync($"https://api.coinbase.com/v2/prices/{currencyPair}/spot");
            Console.WriteLine(JObject.Parse(_json));
            var result = serializer.ReadObject(await _json) as List<CoinbaseData>;

            Console.WriteLine(result.Count);
            return result;
        }
    }

    [DataContract(Name = "data")]
    public class CoinbaseData
    {
        [DataMember(Name = "base")]
        public string Base { get; set; }

        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "amount")]
        public string Amount { get; set; }
    }*/
}
