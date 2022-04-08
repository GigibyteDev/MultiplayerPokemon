using MultiplayerPokemon.Server.Helpers;
using MultiplayerPokemon.Server.Orchestrators;
using MultiplayerPokemon.Server.Orchestrators.Interfaces;
using MultiplayerPokemon.Server.Repositories;
using MultiplayerPokemon.Server.Repositories.Interfaces;
using MultiplayerPokemon.Server.Services;
using MultiplayerPokemon.Server.Services.Interfaces;

namespace MultiplayerPokemon.Server.Extensions
{
    public static class DomainConfigurationExtensions
    {
        public static void ConfigureDomainScopes(this IServiceCollection services)
        {
            services.AddSingleton<ITokenService, JWTService>();
            services.AddSingleton(new SignalRConnectionManager());
            services.AddSingleton<IUserRepository, MemoryUserRepository>();
            services.AddSingleton<IRoomRepository, MemoryRoomRepository>();
            services.AddSingleton<IAuthOrchestrator, AuthOrchestrator>();
            services.AddSingleton<IRoomOrchestrator, RoomOrchestrator>();
        }
    }
}
