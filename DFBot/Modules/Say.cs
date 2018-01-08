using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.Modules
{
    public class Say : ModuleBase<SocketCommandContext>
    {
        [Command("say")]                
        public async Task SayAsync([Remainder]string arg = "What would you like me to say?")
        {
            await ReplyAsync(arg);
        }
    }
}
