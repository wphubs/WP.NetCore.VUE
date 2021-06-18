using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Model.ViewModel
{
    public class PushMessageViewModel
    {
        public PushMessageViewModel(long id,string content)
        {
            this.Id = id;
            this.Content = content;
        }
        public long Id { get; set; }

        /// <summary>
        /// 消息时间
        /// </summary>
        public System.DateTime? CreateTime { get; set; } = DateTime.Now;


        /// <summary>
        /// 消息内容
        /// </summary>
        public System.String Content { get; set; }
    }
}
