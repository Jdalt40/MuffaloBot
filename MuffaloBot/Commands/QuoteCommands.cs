﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MuffaloBot.Modules;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MuffaloBot.Commands
{
    public class QuoteCommands : BaseCommandModule
    {
        [Command("quotes"), Aliases("quote", "listquotes"), Description("List all the available quote commands.")]
        public async Task ListQuotesAsync(CommandContext ctx)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Listing all quotes:");
            JObject data = ctx.Client.GetExtension<JsonDataModule>().data;
            foreach (var item in data["quotes"])
            {
                JProperty pair = (JProperty)item;
                stringBuilder.Append($"`{pair.Name}` ");
            }
            await ctx.RespondAsync(stringBuilder.ToString());
        }
    }
}
