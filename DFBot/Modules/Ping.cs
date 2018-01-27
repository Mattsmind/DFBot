using Discord;
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
            EmbedBuilder builder = new EmbedBuilder();

            var pingTime = Context.Client.Latency;

            builder.WithTitle("Ping Reply")
                .WithColor(Color.Magenta)
                .WithDescription($"Ping time of {pingTime}ms");
            
            await ReplyAsync("", false, builder.Build());
        }
    }
}
