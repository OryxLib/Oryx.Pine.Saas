using Oryx.Saas.Framework.Applications.WebSockets.Interface;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Oryx.Saas.Framework.Applications.WebSockets.Infrastructure
{
    public class OryxWebSocketPoolItem
    {
        public Dictionary<string, string> QueryString { get; set; } = new Dictionary<string, string>();
        public string Path { get; set; }
        public IOryxHandler Handler { get; set; }
        public System.Net.WebSockets.WebSocket OryxWebSocket { get; set; }
        public Guid Token { get; internal set; }

        public ClaimsPrincipal User { get; set; }
        public object UserId { get; internal set; }
    }
}
