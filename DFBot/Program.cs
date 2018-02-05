using Microsoft.Extensions.Configuration;
using System.IO;


namespace DFBot
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("botconfig.json");

            Configuration = configBuilder.Build();
            
            new BotCore().RunBotAsync().GetAwaiter().GetResult();
        }
    }
}
