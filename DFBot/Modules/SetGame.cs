using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace DFBot.Modules
{
    public class SetGame : ModuleBase<SocketCommandContext>
    {
        [Command("setgame"), RequireOwner]
        public async Task SetGameAsync(string sMode = "playing", [Remainder]string game = "You Need to set a game")
        {
            var client = Context.Client as DiscordSocketClient;
            int mode = 0;

            switch (sMode.ToLower())
            {
                case "playing":
                    mode = 1;
                    break;
                case "listening":
                    mode = 2;
                    break;
                case "watching":
                    mode = 3;
                    break;
                default:
                    mode = 1;
                    break;
            }

            await client.SetGameAsync(game, null, Discord.StreamType.NotStreaming + mode);
        }
    }
}
