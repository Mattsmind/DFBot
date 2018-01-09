using Discord.Commands;
using System.Threading.Tasks;

namespace DFBot.Modules
{
    public class Hello : ModuleBase<SocketCommandContext>
    {
        [Command("hello")]
        public async Task HelloAsync()
        {
            await ReplyAsync($"Hello, {Context.User.Mention}!");
        }
    }
}
