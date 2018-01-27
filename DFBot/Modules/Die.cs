﻿using Discord.Commands;
using System.Threading.Tasks;

namespace DFBot.Modules
{
    [Group("die"), RequireOwner]
    public class Die : ModuleBase<SocketCommandContext>
    {
        [Command]
        public async Task DieAsync()
        {
            await ReplyAsync("Terminating....");
            await Context.Client.StopAsync();
        }
    }
}
