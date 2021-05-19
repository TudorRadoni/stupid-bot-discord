using System;
using System.IO;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace stupid_bot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Summary("Pings the stupid bot.")]
        [Alias("smol brain")]
        public Task Ping()
            => ReplyAsync("UUUGA BUUUUGAAAAAAAAAAAAA!");
        
        [Command("help")]
        [Summary("Help menu.")]
        public async Task Help() {
            string path = @"C:\My Stuff\Projects\VS Code\Stupid Bot (Discord)\stupid-bot-discord-main\Modules\Commands.cs";
            using (StreamReader commandsFile = File.OpenText(path)) {
                string line;
                string outString = "*Uga buga i can do:*\n";
                while ((line = commandsFile.ReadLine()) != null) {
                    if (line.TrimStart().StartsWith("[Command")) {
                        string[] cmd = line.Split('"');
                        outString += " - `" + cmd[1] + "`: ";
                    }
                    if(line.TrimStart().StartsWith("[Summary")) {
                        string[] smr = line.Split('"');
                        outString += smr[1] + "\n";
                    }
                }
                await ReplyAsync(outString);
            }
        }

        [Command("howfat")]
        [Summary("Tells the user how fat he is, or the user parameter, if one passed.")]
        [Alias("am i fat?", "tell how fat")]
        public async Task HowFat(SocketUser user = null)
        {
            Random numberGen = new Random();
            if (user == null) {
                string fatness = "You are " + (numberGen.Next(1, 102)).ToString() + "% fat.";
                await Context.Channel.SendMessageAsync(fatness);
            }
            else {
                var userInfo = user ?? Context.Client.CurrentUser;
                await ReplyAsync($"{userInfo.Username} is {(numberGen.Next(1, 102)).ToString()} % fat.");
            }
        }
    }

    [Group("do math")]
    public class HmmModule : ModuleBase<SocketCommandContext>
    {
        [Command("square")]
        [Summary("Squares a number.")]
        public async Task SquareAsync(float num)
        {
            string[] stupidMsg = new string[]
            {
                "Waaait, you srsly don't know that?!",
                "C'mon, mate, you can't square " + num + "?",
                "Yooo, learn some math, brooo!",
                "M8, this is soooo easy! Look!",
                "Did you know seminars are mandatory?!",
                "Aaahh, right... I already forgot: you were the dumb guy, right?",
                "Attend some classes, my dear!",
                "Windows button > \"Calculator\". Leave me alone already!"
            };

            Random numberGen = new Random();
            await Context.Channel.SendMessageAsync(stupidMsg[(numberGen.Next(0 , stupidMsg.Length))] + $"\n{num} ^ 2 = {Math.Pow(num, 2)} \uD83D\uDC4C"); // equivalent to "👌"
        }
        
        [Command("sqrt")]
        [Summary("Returns the square root of a number.")]
        [Alias("square root of")]
        public async Task SqrtAsync(float num)
        {
            string[] stupidMsg = new string[]
            {
                "Waaait, you srsly don't know that?!",
                "C'mon, mate, you can't compute the square root of " + num + "?",
                "Yooo, learn some math, brooo!",
                "M8, this is soooo easy! Look!",
                "Did you know you seminars are mandatory?!",
                "Aaahh, right... I already forgot: you were the dumb guy, right?",
                "Attend some classes, my dear!",
                "Windows button > \"Calculator\". Leave me alone already!"
            };

            Random numberGen = new Random();
            await Context.Channel.SendMessageAsync(stupidMsg[(numberGen.Next(0 , stupidMsg.Length))] + $"\n{num} ^ (1/2) = {Math.Sqrt(num)} \uD83D\uDC4C"); // equivalent to "👌"
        }

        [Command("userinfo")]
        [Summary("Returns info about the current user, or the user parameter, if one passed.")]
        [Alias("user", "whois")]
        public async Task UserInfoAsync(SocketUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
        }        
    }
}