using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace DFBot.Modules
{
    public class SetGame : ModuleBase<SocketCommandContext>
    {
        [Command("setgame"), RequireOwner]
        public async Task SetGameAsync(int mode = 1, [Remainder]string game = "You Need to set a game")
        {
            var client = Context.Client as DiscordSocketClient;

            await client.SetGameAsync(game, null, Discord.StreamType.NotStreaming + mode);
        }
    }
}
