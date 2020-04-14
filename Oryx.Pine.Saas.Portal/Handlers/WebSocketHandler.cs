using Oryx.Saas.Framework.Applications.WebSockets.Infrastructure;
using Oryx.Saas.Framework.Applications.WebSockets.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.Pine.Saas.Portal.Handlers
{
    public class WebSocketHandler : IOryxHandler
    {
        public async Task OnClose(OryxWebSocketMessage msg)
        {

        }

        public async Task OnReciveMessage(OryxWebSocketMessage msg)
        {
                
        }
    }
}
