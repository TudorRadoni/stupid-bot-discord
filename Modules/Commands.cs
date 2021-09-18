using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
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

        // This needs work for the Alias stuff...
        [Command("help")]
        [Summary("Help menu.")]
        public async Task Help()
        {
            string path = Path.GetFullPath("Modules\\Commands.cs");
            
            using (StreamReader commandsFile = File.OpenText(path)) {
                string line;
                string outString = "***Uga buga** I can do:*\n";

                while ((line = commandsFile.ReadLine()) != null) {
                    if (line.TrimStart().StartsWith("[Command")) {
                        string[] cmd = line.Split('"');
                        outString += " - `" + cmd[1] + "`: ";
                        
                    }
                    if(line.TrimStart().StartsWith("[Summary")) {
                        string[] smr = line.Split('"');
                        outString += smr[1] + "\n";
                    }

                    //         This is not working properly!
                    //
                    // if(line.TrimStart().StartsWith("[Alias")) {
                    //     string[] als = line.Split('"');
                    //     outString += "\t- Aliases:";
                    //     for (int i = 1; i < als.Count() - 1; ++i) {
                    //         outString += als[i] + ", "; 
                    //     }
                    //     outString += als[als.Count() - 1] + ".\n";
                    // }
                }
                await ReplyAsync(outString);
            }
        }

        [Command("howfat")]
        [Summary("Tells the user how fat he is or the tagged user, if one has been passed.")]
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

        [Command("maketeam")]
        [Summary("Randomly splits numbers into two teams.")]
        [Alias("howteam")]
        public async Task MakeTeam(byte players)
        {
            string outString;
            if (players < 2) {
                outString = "You are an idiot and you have to fly off the server immediately!";
            }
            else {
                Random rnd = new Random();

                // create a sequence of length 'players'
                int[] sequence = Enumerable.Range(1, players).ToArray();
                // randomise the sequence
                int[] rndSequence = sequence.OrderBy(x => rnd.Next()).ToArray(); 

                // split in half
                int[]  firstTeam = rndSequence.Take((rndSequence.Length + 1) / 2).ToArray();
                int[] secondTeam = rndSequence.Skip((rndSequence.Length + 1) / 2).ToArray();

                // create the output string
                outString = "***Uga buga...** Ah, the teams are:*\n";
                outString += "\t*- Team 1:* ` " + String.Join(", ", firstTeam) + " `\n";
                outString += "\t*- Team 2:* ` " + String.Join(", ", secondTeam) + " `\n";
                outString += "Good luck & have fun!";
            }
            await ReplyAsync(outString);
        }

        // This is not working properly! - bot dcs immediately...
        [Command("join voice", RunMode = RunMode.Async)] 
        [Summary("Invites the user to join a voice channel.")]
        public async Task JoinChannel(IVoiceChannel channel = null)
        {
            // Get the audio channel
            channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;
            if (channel == null) { await Context.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }

            // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.
            var audioClient = await channel.ConnectAsync();
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
        }
    // Group END
}
