using Discord.Commands;
using System.Threading.Tasks;

namespace DFBot.Modules
{
    [Group("ping")]
    [Summary("A ping tool, to check the bots latency to discord.")]
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command]
        public async Task PingAsync()
        {
            var pingTime = Context.Client.Latency;
            await ReplyAsync($"Ping of {pingTime}ms");
        }
    }
}
