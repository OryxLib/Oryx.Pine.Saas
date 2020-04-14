using Microsoft.Extensions.DependencyInjection;
using Oryx.Saas.Framework.Applications.WebSockets.Infrastructure;

namespace Oryx.Saas.Framework.Applications.WebSockets.Extension.DependencyInjection
{
    public static class OrxyWebSocketServiceExtension
    {
        public static IServiceCollection AddOryxWebSocket(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                        .AddSingleton<OryxWebSocketPool>()
                       .AddSingleton<OryxWebSocketOptions>()
                       .AddTransient<OryxWebSocketTool>();
        }
    }
}
