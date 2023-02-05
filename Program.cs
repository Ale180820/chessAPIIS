using Autofac;
using Autofac.Extensions.DependencyInjection;
using chessAPI;
using chessAPI.business.interfaces;
using chessAPI.models.game;
using chessAPI.models.player;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using Serilog.Events;

//Serilog logger (https://github.com/serilog/serilog-aspnetcore)
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("chessAPI starting");
    var builder = WebApplication.CreateBuilder(args);

    var connectionStrings = new connectionStrings();
    builder.Services.AddOptions();
    builder.Services.Configure<connectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
    builder.Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);

    // Two-stage initialization (https://github.com/serilog/serilog-aspnetcore)
    builder.Host.UseSerilog((context, services, configuration) => configuration.ReadFrom
             .Configuration(context.Configuration)
             .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning).ReadFrom
             .Services(services).Enrich
             .FromLogContext().WriteTo
             .Console());

    // Autofac como inyección de dependencias
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new chessAPI.dependencyInjection<int, int>()));
    var app = builder.Build();
    app.UseSerilogRequestLogging();
    app.UseMiddleware(typeof(chessAPI.customMiddleware<int>));
    app.MapGet("/", () =>
    {
        return "hola mundo";
    });
    //PLAYER
    app.MapPost("player", 
    [AllowAnonymous] async(IPlayerBusiness<int> bs, clsNewPlayer newPlayer) => Results.Ok(await bs.addPlayer(newPlayer)));

    app.MapGet("player", 
    [AllowAnonymous] async(IPlayerBusiness<int> bs, int id) => Results.Ok(await bs.getPlayer(id)));

    app.MapPut("player", 
    [AllowAnonymous] async(IPlayerBusiness<int> bs, clsPlayer<int> updatePlayer) => {
        var result = await bs.putPlayer(updatePlayer);
        if (result != null)
        {
            return Results.Ok(result);
        }else{
            return Results.NotFound("Ha ocurrido un error en la actualización del registro");
        }
        });
    //GET
    app.MapPost("game", 
    [AllowAnonymous] async(IGameBusiness<int> bs, clsNewGame newGame) => Results.Ok(await bs.addGame(newGame)));

    app.MapGet("game", 
    [AllowAnonymous] async(IGameBusiness<int> bs, int id) => Results.Ok(await bs.getGame(id)));

    app.MapPut("game", 
    [AllowAnonymous] async(IGameBusiness<int> bs, clsGame<int> updateGame) => {
        var result = await bs.putGame(updateGame);
        if (result != null)
        {
            return Results.Ok("Asignación exitosa");
        }else{
            return Results.Conflict("Existen jugadores que pertenecen a ambos equipos");
        }
        });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "chessAPI terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
