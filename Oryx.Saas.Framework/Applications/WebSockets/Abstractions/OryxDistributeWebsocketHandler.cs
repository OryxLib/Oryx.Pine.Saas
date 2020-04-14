using Oryx.Saas.Framework.Applications.WebSockets.Infrastructure;
using Oryx.Saas.Framework.Applications.WebSockets.Interface;
using Oryx.Saas.Framework.Applications.WebSockets.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Saas.Framework.Applications.WebSockets.Abstractions
{
    public abstract class OryxDistributeWebsocketHandler : IOryxHandler
    {
        public SocialMsgManager socialMsgManager;

        public OryxDistributeWebsocketHandler(
            SocialMsgManager _socialMsgManager
            )
        {
            socialMsgManager = _socialMsgManager;
        }
        public abstract Task OnClose(OryxWebSocketMessage msg);

        public abstract Task OnReciveMessage(OryxWebSocketMessage msg);
    }
}
