using Discord.Net;
using Discord;
using Discord.WebSocket;
using Discord.Interactions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public class Program
{
    public const string TOKEN = "MTAzNDA4Njk3OTA2OTAzMDQzMA.G41B8h.m9X7A8XGMwwTrePjFfJCLmrAu_CRT0XqdNEWVc";
    private DiscordSocketClient theClient;
    private IServiceProvider _serviceProvider;

    static Task Main() => new Program().MainAsync();

    static IServiceProvider CreateProvider()
    {
        var collection = new ServiceCollection();
        //...
        return collection.BuildServiceProvider();
    }

    public async Task MainAsync()
    {
        theClient = new();

        _serviceProvider = CreateProvider();

        // ---------------

        // Run Seq.1
        Console.WriteLine("Starting Bot");

        // Run Seq.2
        await theClient.LoginAsync(TokenType.Bot, TOKEN);
        await theClient.StartAsync();

        var _interactionService = new InteractionService(theClient.Rest);

        // Run Seq.3
        theClient.Ready += async () =>
        {
            Console.WriteLine($"Bot demarré, {DateTime.Now}");

            _interactionService = new InteractionService(theClient);
            await _interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), _serviceProvider);
            await _interactionService.RegisterCommandsToGuildAsync(1031636775011102720);

            theClient.InteractionCreated += async interaction =>
            {
                var scope = _serviceProvider.CreateScope();
                var ctx = new SocketInteractionContext(theClient, interaction);
                await _interactionService.ExecuteCommandAsync(ctx, scope.ServiceProvider);
            };

            StatutRunner();
        };

        await Task.Delay(-1);
    }

    public async void StatutRunner()
    {
        do
        {
            await theClient.SetGameAsync("Salade, tomate, onion ?");
            Thread.Sleep(5000);
            await theClient.SetGameAsync("Quelle sauce ?");
        }
        while (true);
    }

    public async void SendLog(string context)
    {
        IChannel getLogChan;
        getLogChan = theClient.GetChannel(1018125885888540732);

        ITextChannel logChan = (ITextChannel)getLogChan;

        await logChan.SendMessageAsync(context);
    }
}