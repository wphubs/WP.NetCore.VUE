using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.Common.Helper;
using WP.NetCore.Model.ViewModel;

namespace WP.NetCore.API.Hubs
{

    [Authorize]
    public class WelcomeHub : Hub<IClientEvent>
    {

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.ReceiveEventAsync(new PushMessageViewModel(new Snowflake().NextId(), "这是一条来自服务端SignalR推送的实时消息"));
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
