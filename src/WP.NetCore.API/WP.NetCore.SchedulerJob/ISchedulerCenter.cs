using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Model.EntityModel;

namespace WP.NetCore.SchedulerJob
{
    public interface ISchedulerCenter
    {
        /// <summary>
        /// 开启任务调度
        /// </summary>
        Task StartScheduleAsync();

        /// <summary>
        /// 停止任务调度
        /// </summary>
        Task StopScheduleAsync();

        /// <summary>
        /// 添加任务
        /// </summary>
        Task AddScheduleJobAsync(ScheduleJob scheduleJob);

        /// <summary>
        /// 立即执行
        /// </summary>
        /// <param name="scheduleJob"></param>
        /// <returns></returns>
        Task TriggerJobAsync(string jobGroup, string jobName);

        /// <summary>
        /// 暂停任务
        /// </summary>
        Task PauseJobAsync(string jobGroup, string jobName);

        /// <summary>
        /// 恢复任务
        /// </summary>
        /// <param name="sysSchedule"></param>
        /// <returns></returns>
        Task ResumeJobAsync(string jobGroup, string jobName);

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="jobGroup"></param>
        /// <param name="jobName"></param>
        /// <returns></returns>
        Task DeleteJobAsync(string jobGroup, string jobName);
    }
}
