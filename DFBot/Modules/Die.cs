using Discord.Commands;
using System;
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
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Context.User} called Die at {DateTime.Now}. TERMINATING!");
            Console.ResetColor();

            Environment.Exit(0);
            
        }
    }
}
