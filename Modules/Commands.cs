using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace stupid_bot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("piiing")]
        public async Task Piiing() {
            ReplyAsync("Pooong!");
        }

        [Command("sabin")]
        public async Task Sabin() {
            ReplyAsync("Everybody hates Sabin...");
        }

        [Command("?")]
        public async Task QuestionMark() {
            ReplyAsync("Yes, I'm online. What is it this time?!");
        }

        [Command("howfat")]
        public async Task HowFat() {
            Random numberGen = new Random();
            string fatness = "You are " + (numberGen.Next(1, 102)).ToString() + "% fat.";
        
            ReplyAsync(fatness);
        }
    }
}