using Oryx.Saas.Framework.Applications.WebSockets.Infrastructure;
using Oryx.Saas.Framework.Applications.WebSockets.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Saas.Framework.Applications.WebSockets.Abstractions
{
    public abstract class OryxWebSocketHandler : IOryxHandler
    {
        public OryxWebSocketPool oryxWebSocketPool;
        public OryxWebSocketHandler(OryxWebSocketPool _oryxWebSocketPool)
        {
            oryxWebSocketPool = _oryxWebSocketPool;
        }
        public abstract Task OnClose(OryxWebSocketMessage msg);

        public abstract Task OnReciveMessage(OryxWebSocketMessage msg);
    }
}
