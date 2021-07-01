using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Common;
using WP.NetCore.Common.Enums;
using WP.NetCore.IServices;

namespace WP.NetCore.SchedulerJob.Job
{
    [DisallowConcurrentExecution]
    public class HttpJob : JobBase,IJob
    {
        private readonly IScheduleJobService scheduleJobService;

        public HttpJob(IScheduleJobService scheduleJobService):base(scheduleJobService)
        {
            this.scheduleJobService = scheduleJobService;
        }

        public override async Task ExecuteJob(IJobExecutionContext context)
        {
            var requestParameters = context.JobDetail.JobDataMap.GetString(Constant.REQUESTPARAMETERS);
            var headersString = context.JobDetail.JobDataMap.GetString(Constant.HEADERS);
            var headers = headersString != null ? JsonConvert.DeserializeObject<Dictionary<string, string>>(headersString?.Trim()) : null;
            var requestType = (RequestTypeEnum)int.Parse(context.JobDetail.JobDataMap.GetString(Constant.REQUESTTYPE));
            switch (requestType)
            {
                case RequestTypeEnum.Get:
                    break;
                case RequestTypeEnum.Post:
                    break;
                case RequestTypeEnum.Put:
                    break;
                case RequestTypeEnum.Delete:
                    break;
            }

            await Task.Run(() => Console.WriteLine($"{DateTime.Now}：HttpJob"));
        }
    }
}
