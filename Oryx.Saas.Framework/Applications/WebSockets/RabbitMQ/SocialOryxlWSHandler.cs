using Oryx.Saas.Framework.Applications.WebSockets.Infrastructure;
using Oryx.Saas.Framework.Applications.WebSockets.Interface;
using Oryx.Saas.Framework.Applications.WebSockets.RabbitMQ;
using Oryx.Saas.Framework.Database.RabbitMQ;
using System.Threading.Tasks;

namespace Social.WebSocket
{
    public class SocialOryxlWSHandler : IOryxHandler
    {
        public RabbitMQClient rabbitMQClient;
        public SocialMsgManager socialMsgManager;

        public SocialOryxlWSHandler(
            RabbitMQClient _rabbitMQClient,
            SocialMsgManager _socialMsgManager
            )
        {
            rabbitMQClient = _rabbitMQClient;
            socialMsgManager = _socialMsgManager;
        }

        public async Task OnClose(OryxWebSocketMessage msg)
        {

        }

        public async Task OnReciveMessage(OryxWebSocketMessage msg)
        {
            await Task.Run(() =>
          {
              var userId = msg.Query[SocialMsgManager.SocialMsgWSClientKey];
              socialMsgManager.SendMssage(userId, msg.Message);
          });
        }
    }
}
