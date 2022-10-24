using Discord;
using Discord.Commands;
using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

public class Commands : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("say", "Envoi un message. ⚠️ Commande reservée aux modérateurs.", false, Discord.Interactions.RunMode.Async)]
    public async Task Say(string input)
    {
        await Context.Channel.SendMessageAsync(input);
    }
}