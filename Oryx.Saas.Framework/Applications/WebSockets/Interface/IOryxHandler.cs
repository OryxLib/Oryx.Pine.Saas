using Oryx.Saas.Framework.Applications.WebSockets.Infrastructure;
using System.Threading.Tasks;

namespace Oryx.Saas.Framework.Applications.WebSockets.Interface
{
    public interface IOryxHandler
    {
        Task OnReciveMessage(OryxWebSocketMessage msg);
        Task OnClose(OryxWebSocketMessage msg);
    }
}
