﻿using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Exceptions;

namespace MuffaloBotNetFramework2.DiscordComponent
{
    class MuffaloBotExceptionHandler : IClientModule
    {
        public void BindToClient(DiscordClient client)
        {
            client.ClientErrored += HandleClientError;
            client.GetCommandsNext().CommandErrored += HandleClientError;
        }

        public void InitializeFronJson(JObject jObject)
        {
        }

        public async Task HandleClientError(CommandErrorEventArgs e)
        {
            if (e.Exception is CommandNotFoundException || e.Exception is UnauthorizedException) return;
            await HandleClientError(e.Exception, "Command " + (e.Command?.Name ?? "unknown"));
        }

        public Task HandleClientError(ClientErrorEventArgs e)
        {
            return HandleClientError(e.Exception, "Event " + e.EventName);
        }
        public async Task HandleClientError(Exception e, string action)
        {
            await Console.Out.WriteLineAsync(e.ToString());
            DiscordEmbedBuilder builder = new DiscordEmbedBuilder();
            builder.WithTitle("Unhandled exception");
            builder.WithDescription($"Action: {action}\n```\n{e.ToString()}```");
            builder.WithColor(DiscordColor.Red);
            DiscordChannel channel = await MuffaloBot.discordClient.CreateDmAsync(MuffaloBot.discordClient.CurrentApplication.Owner);
            await MuffaloBot.discordClient.SendMessageAsync(channel, embed: builder.Build());
        }
    }
}