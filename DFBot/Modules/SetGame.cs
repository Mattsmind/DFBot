using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.Modules
{
    public class SetGame : ModuleBase<SocketCommandContext>
    {
        [Command("setgame"), RequireOwner]
        public async Task SetGameAsync([Remainder]string game)
        {
            var client = Context.Client as DiscordSocketClient;

            await client.SetGameAsync(game);
        }
    }
}
