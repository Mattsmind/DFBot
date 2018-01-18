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
            string[] split = arg.Split("d");
            int numOfDice = Convert.ToInt32(split[0]);
            int numSides = Convert.ToInt32(split[2]);

            int[] rolls = null;

            for (int die = 0; die < numOfDice; die++)
            {
                Random rnd = new Random();
                int roll = rnd.Next(1, numSides + 1);

                rolls[die] = roll;
            }
                     
            var result = $"NUMBER OF DICE: {numOfDice} | NUMBER OF SIDES: {numSides} || ROLLS: {rolls}";

            await ReplyAsync(result);
        }
    }
}
