﻿using Discord.Commands;
using System.Threading.Tasks;

namespace DFBot.Modules
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync()
        {
            var pingTime = Context.Client.Latency;
            await ReplyAsync($"PING! {pingTime}ms");
        }
    }
}
