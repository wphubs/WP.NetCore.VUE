using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WP.NetCore.Common;
using WP.NetCore.Common.Enums;
using WP.NetCore.Common.Helper;
using WP.NetCore.IServices;

namespace WP.NetCore.SchedulerJob.Job
{
    [DisallowConcurrentExecution]
    public class HttpJob : JobBase,IJob
    {
        private readonly IScheduleJobService scheduleJobService;
        private readonly HttpHelper httpHelper;

        public HttpJob(IScheduleJobService scheduleJobService, HttpHelper httpHelper) :base(scheduleJobService)
        {
            this.scheduleJobService = scheduleJobService;
            this.httpHelper = httpHelper;
        }

        public override async Task<(bool Result,string Msg)> ExecuteJob(IJobExecutionContext context)
        {
            var requestUrl = context.JobDetail.JobDataMap.GetString(JobConstant.RequestUrl)?.Trim();
            var requestParameters = context.JobDetail.JobDataMap.GetString(JobConstant.RequestParameters);
            var headersString = context.JobDetail.JobDataMap.GetString(JobConstant.Headers);
            var headers = headersString != null ? JsonConvert.DeserializeObject<Dictionary<string, string>>(headersString?.Trim()) : null;
            var requestType = (RequestTypeEnum)int.Parse(context.JobDetail.JobDataMap.GetString(JobConstant.RequestType));
            HttpResponseMessage response = new HttpResponseMessage();
            switch (requestType)
            {
                case RequestTypeEnum.Get:
                    response = await httpHelper.GetAsync(requestUrl, headers);
                    break;
                case RequestTypeEnum.Post:
                    response = await httpHelper.PostAsync(requestUrl, requestParameters, headers);
                    break;
                case RequestTypeEnum.Put:
                    response = await httpHelper.PutAsync(requestUrl, requestParameters, headers);
                    break;
                case RequestTypeEnum.Delete:
                    response = await httpHelper.DeleteAsync(requestUrl, headers);
                    break;
            }
            var result = HttpUtility.HtmlEncode(await response.Content.ReadAsStringAsync());
            if (response.IsSuccessStatusCode)
            {
                result = result.Length > 50 ? result.Substring(0, 50) + "..." : result;
                await Task.Run(() => Console.WriteLine($"{DateTime.Now}：{requestType}  {requestUrl}  {requestParameters}"));
                return (true, result);
            }
            else 
            {
                var detail = new StringBuilder();
                detail.AppendFormat("<p style=\"font-weight:bold; color: red\"> 任务类型： {0}  </p>", nameof(HttpJob));
                detail.AppendFormat("<p style=\"font-weight:bold; color: red\"> 请求方式： {0}  </p>", requestType.GetRequestTypeEnumDesc());
                detail.AppendFormat("<p style=\"font-weight:bold; color: red\"> 请求地址： {0}  </p>", requestUrl);
                detail.AppendFormat("<p style=\"font-weight:bold; color: red\"> 请求结果： {0}  </p>", result);
                return (false, detail.ToString());
            }
            
          
        }
    }
}
