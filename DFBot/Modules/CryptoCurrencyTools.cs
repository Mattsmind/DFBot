using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.Modules
{
    [Group("cryptotools"), Alias("bitcoin", "ct", "btc")]
    [Summary("A set of tools used to retrieve information on various cryptocurrencies.")]
    class CryptoCurrencyTools : ModuleBase<SocketCommandContext>
    {
        [Command, Alias("help")]
        public async Task CryptoHelpAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();

            await ReplyAsync("", false, builder.Build());
        }

        [Command("pricecheck"), Alias("pc", "price")]
        [Summary("Returns pricing information.")]
        public async Task PriceCheckAsync(string currencyPair = "btcusd")
        {
            EmbedBuilder builder = new EmbedBuilder();

            await ReplyAsync("", false, builder.Build());
        }
    }

    class CryptoCoinData
    {

    }
}
