using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DFBot
{
    class BotCore : Program
    {
        public DiscordSocketClient _client;
        public CommandService _commands;
        public IServiceProvider _services;

        public async Task RunBotAsync()
        {

            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string botToken = Configuration["bot:token"];

            //event subscriptions
            _client.Log += Log;

            //load modules
            await RegisterCommandsAsync();

            //login the bot
            await _client.LoginAsync(TokenType.Bot, botToken);
            await _client.StartAsync();

            //set game tag
            string game = Configuration["bot:game"];
            await _client.SetGameAsync(game, null, StreamType.NotStreaming + 1);

            //delay forever       
            await Task.Delay(-1);
        }

        // EVENT HANDLERS
        //Logger
        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.CompletedTask;
        }


        // Register and Handle Command Modules
        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot)
            {
                return;
            }

            int argPos = 0;
            string botPrefix = Configuration["bot:prefix"];

            if (message.HasStringPrefix(botPrefix, ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);

                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ERROR: {result.ErrorReason}");
                    Console.ResetColor();

                    await message.DeleteAsync();
                }
                else if (result.Error == CommandError.UnknownCommand)
                {
                    return;
                }
                else
                {
                    Console.WriteLine($"[{DateTime.Now}][{message.Author.Username}]==> {message.Content}");

                    await message.DeleteAsync();
                }
            }
        }
    }
}
