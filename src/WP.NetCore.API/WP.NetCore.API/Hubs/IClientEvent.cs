using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.Model.ViewModel;

namespace WP.NetCore.API.Hubs
{
    public interface IClientEvent
    {
        /// <summary>
        /// 接收方消息事件
        /// </summary>
        /// <returns></returns>
        Task ReceiveEventAsync(PushMessageViewModel obj);


    }
}
