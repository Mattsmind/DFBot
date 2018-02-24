using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DFBot.Modules
{
    [Group("useradmin"), Alias("ua","user")]
    public class UserAdministration : ModuleBase<SocketCommandContext>
    {
        [Command, Alias("help")]
        public async Task UserAdminHelpInfoAsync()
        {
            await ReplyAsync("");
        }

        [Command("kick")]
        public async Task KickUserAsync(string username)
        {
            await ReplyAsync("");
        }

        [Command("ban")]
        public async Task BanUserAsync(string username)
        {
            await ReplyAsync("");
        }

        [Command("unban"), Alias("uban", "ub")]
        public async Task UnbanUserAsync(string username)
        {
            await ReplyAsync("");
        }

        [Command("mute")]
        public async Task MuteUserAsync(string username, int time)
        {
            await ReplyAsync("");
        }

        [Command("unmute")]
        public async Task UnmuteUserAsync(string username)
        {
            await ReplyAsync("");
        }
    }
}
