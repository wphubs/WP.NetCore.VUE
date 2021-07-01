using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Common.Enums;

namespace WP.NetCore.Model.Dto.ScheduleJob
{
    public class AddScheduleJobDto
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        [Required(ErrorMessage = "标题不能为空")]
        public string JobName { get; set; }
        /// <summary>
        /// 任务分组
        /// </summary>
        [Required(ErrorMessage = "标题不能为空")]
        public string JobGroup { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        [Required(ErrorMessage = "标题不能为空")]
        public JobTypeEnum JobType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string Cron { get; set; }

        /// <summary>
        /// 执行次数（默认无限循环）
        /// </summary>
        public int? SimpleTimes { get; set; }

        /// <summary>
        /// 执行间隔时间，单位秒（如果有Cron，则IntervalSecond失效）
        /// </summary>
        public int? IntervalSecond { get; set; }


        /// <summary>
        /// 触发器类型
        /// </summary>
        [Required(ErrorMessage = "标题不能为空")]
        public TriggerTypeEnum TriggerType { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public bool IsStart { get; set; } = false;

        #region Url
        /// <summary>
        /// 请求url
        /// </summary>
        public string RequestUrl { get; set; }
        /// <summary>
        /// 请求参数（Post，Put请求用）
        /// </summary>
        public string RequestParameters { get; set; }
        /// <summary>
        /// Headers(可以包含如：Authorization授权认证)
        /// 格式：{"Authorization":"userpassword.."}
        /// </summary>
        public string Headers { get; set; }
        /// <summary>
        /// 请求类型
        /// </summary>
        public RequestTypeEnum RequestType { get; set; } = RequestTypeEnum.Post;

        #endregion
    }
}
