using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace DFBot.Modules
{
    public class Hello : ModuleBase<SocketCommandContext>
    {
        [Command("hello"), Alias("code", "mygit", "myrepo", "source")]
        public async Task HelloAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Hello I'm DF-Bot!")
                .WithDescription("**Greetings, " + Context.User.Mention + "**\n\n" +
                "My code can be found at https://github.com/Mattsmind/DFBot")
                .WithColor(Color.DarkPurple);

            await ReplyAsync("", false, builder.Build());
        }
    }
}
