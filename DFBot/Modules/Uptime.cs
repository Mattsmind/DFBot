using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.Modules
{
    public class Uptime : ModuleBase<SocketCommandContext>
    {
        [Command("uptime")]
        public async Task UptimeAsync()
        {
            
            DateTime procStartTime = Process.GetCurrentProcess().StartTime;
            DateTime timeNow = DateTime.Now;

            

            await ReplyAsync(procStartTime + "\n" + timeNow);
        }
    }
}
