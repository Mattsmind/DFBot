using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace DFBot.Modules
{
    public class DiceRoller : ModuleBase<SocketCommandContext>
    {
        [Command("roll")]
        public async Task DiceRollerAsync(int sides = 6)
        {
            Random rnd = new Random();
            int roll = rnd.Next(1, sides + 1);
            var result = $"{Context.User.Mention} rolled {roll} on a {sides} sided die.";

            await ReplyAsync(result);
        }
    }
}
