using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace DFBot.Modules
{
    [Group("roll")]
    [Summary("A simple dice roller.")]
    public class DiceRoller : ModuleBase<SocketCommandContext>
    {
        [Command]
        public async Task DiceRollerAsync(string arg = "1d6")
        {
            EmbedBuilder builder = new EmbedBuilder();

            string[] split = arg.Split("d", StringSplitOptions.RemoveEmptyEntries);
            int numOfDice = Convert.ToInt32(split[0]);
            int numSides = Convert.ToInt32(split[1]);

            int[] rolls = new int[numOfDice];

            for (int die = 0; die < rolls.Length; die++)
            {
                Random rnd = new Random();
                int roll = rnd.Next(1, numSides + 1);

                rolls[die] = roll;
            }

            int total = 0;
            foreach (int num in rolls)
            {
                total += num;
            }
                     
            //var result = $"NUMBER OF DICE: {numOfDice} | NUMBER OF SIDES: {numSides} || ROLLS: " + string.Join(", ", rolls) + " || TOTAL: " + total;

            builder.WithTitle($"{Context.User.Username} rolled {numOfDice}, {numSides} sided dice.")
                .WithDescription("**ROLLS:** " + string.Join(", ", rolls) + "\n\n**TOTAL OF ALL DICE:** " + total)
                .WithColor(Color.Blue);

            await ReplyAsync("", false, builder.Build());
        }
    }
}
