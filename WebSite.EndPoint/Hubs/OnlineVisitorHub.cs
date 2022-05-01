using Application.Visitors.VisitorOnline;
using Microsoft.AspNetCore.SignalR;

namespace WebSite.EndPoint.Hubs
{
    public class OnlineVisitorHub:Hub
    {
        public readonly IVisitorOnlineService _visitorOnlineService;
        public OnlineVisitorHub(IVisitorOnlineService visitorOnlineService)
        {
            _visitorOnlineService = visitorOnlineService;
        }
        public override Task OnConnectedAsync()
        {
            string? VisitorId = Context.GetHttpContext().Request.Cookies["VisitorId"];
            _visitorOnlineService.ConnectUser(VisitorId);
            var count = _visitorOnlineService.GetCount();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            string? VisitorId = Context.GetHttpContext().Request.Cookies["VisitorId"];
            _visitorOnlineService.DisConnectUser(VisitorId);
            var count = _visitorOnlineService.GetCount();
            return base.OnDisconnectedAsync(exception);
        }

    }
}
