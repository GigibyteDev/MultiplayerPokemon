using MultiplayerPokemon.Server.Extensions;
using MultiplayerPokemon.Server.Hubs;
using MultiplayerPokemon.Server.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.ConfigureDomainScopes();

builder.Configuration.InitializeAppSettings();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapHub<RoomHub>("/room");
app.MapFallbackToFile("index.html");

app.UseMiddleware<JwtMiddleware>();

app.Run();
