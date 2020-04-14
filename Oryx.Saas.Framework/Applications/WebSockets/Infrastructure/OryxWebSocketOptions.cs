using Oryx.Saas.Framework.Applications.WebSockets.Interface;
using System.Collections.Generic;

namespace Oryx.Saas.Framework.Applications.WebSockets.Infrastructure
{
    public class OryxWebSocketOptions
    {
        public Dictionary<string, IOryxHandler> OptionsDic { get; set; } = new Dictionary<string, IOryxHandler>();

        public void Register(string path, IOryxHandler handler)
        {
            OptionsDic.Add(path, handler);
        }
    }
}
