using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.Modules
{
    [Group("die"), RequireOwner]
    class Die : ModuleBase<SocketCommandContext>
    {
        [Command]
        public async Task DieAsync()
        {
            await ReplyAsync("Terminating....");
            await Context.Client.StopAsync();
        }
    }
}
