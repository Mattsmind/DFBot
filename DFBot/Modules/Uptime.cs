using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

            TimeSpan uptime = timeNow.Subtract(procStartTime);
            CultureInfo culture = new CultureInfo("en-US");
            string format = @"hh\:mm\:ss";

            await ReplyAsync(uptime.ToString(format, culture));
        }
    }
}
