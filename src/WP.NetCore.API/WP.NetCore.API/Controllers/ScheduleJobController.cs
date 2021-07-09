using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WP.NetCore.Common.Helper;
using WP.NetCore.IServices;
using WP.NetCore.Model.Dto.ScheduleJob;
using WP.NetCore.Model.EntityModel;
using WP.NetCore.Repository.EFCore;
using WP.NetCore.SchedulerJob;

namespace WP.NetCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Permission")]
    public class ScheduleJobController : BaseController
    {
        private readonly IScheduleJobService scheduleJobService;
        private readonly ISchedulerCenter schedulerCenter;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ScheduleJobController(IScheduleJobService scheduleJobService,
            ISchedulerCenter schedulerCenter,
            IUnitOfWork uow,
            IMapper mapper)
        {
            this.scheduleJobService = scheduleJobService;
            this.schedulerCenter = schedulerCenter;
            this.uow = uow;
            this.mapper = mapper;
        }



        /// <summary>
        /// 查询任务计划
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleJob>>> Get()
        {
            var list = await scheduleJobService.GetAllAsync();
            return Ok(list);
        }

        /// <summary>
        /// 创建任务计划
        /// </summary>
        /// <param name="scheduleJob"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddScheduleJobDto scheduleJob)
        {
            try
            {
                var objScheduleJob = mapper.Map<ScheduleJob>(scheduleJob);
                await uow.BeginAsync();
                if (objScheduleJob.TriggerType == Common.Enums.TriggerTypeEnum.Cron) objScheduleJob.IntervalSecond = null;
                if (objScheduleJob.TriggerType == Common.Enums.TriggerTypeEnum.Simple) objScheduleJob.Cron = null;
                await scheduleJobService.AddAsync(objScheduleJob);
                await schedulerCenter.AddScheduleJobAsync(objScheduleJob);
                await uow.CommitAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// 修改任务计划
        /// </summary>
        /// <param name="scheduleJob"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateScheduleJobDto scheduleJob)
        {
            try
            {
                var objJob = await scheduleJobService.FirstNoTrackingAsync(scheduleJob.Id);
                if (objJob == null)
                {
                    return BadRequest("任务不存在");
                }
                var objScheduleJob = mapper.Map<ScheduleJob>(scheduleJob);
                await uow.BeginAsync();
                await scheduleJobService.UpdateAsync(objScheduleJob);
                await schedulerCenter.PauseJobAsync(objJob.JobGroup, objJob.JobName);
                await schedulerCenter.AddScheduleJobAsync(objScheduleJob);
                await uow.CommitAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                await uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// 删除任务计划
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> Delete(long jobId)
        {
            await uow.BeginAsync();
            var objJob = await scheduleJobService.FirstNoTrackingAsync(jobId);
            if (objJob == null)
            {
                return BadRequest("任务不存在");
            }
            try
            {
                await uow.BeginAsync();
                await scheduleJobService.DeleteAsync(objJob.Id);
                await schedulerCenter.DeleteJobAsync(objJob.JobGroup, objJob.JobName);
                await uow.CommitAsync();
                return Ok();
            }
            catch (Exception)
            {
                await uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// 恢复任务计划
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpGet("ResumeJob")]
        public async Task<ActionResult> ResumeJob(long jobId)
        {

            var objJob = await scheduleJobService.FirstAsync(jobId);
            if (objJob == null)
            {
                return BadRequest("任务不存在");
            }
            if (objJob.IsStart)
            {
                return Ok();
            }
            objJob.IsStart = true;
            try
            {
                await uow.BeginAsync();
                await schedulerCenter.ResumeJobAsync(objJob.JobGroup, objJob.JobName);
                await uow.CommitAsync();
                return Ok();
            }
            catch (Exception)
            {
                await uow.RollbackAsync();
                throw;
            }
        }


        /// <summary>
        /// 停止任务计划
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpGet("PauseJob")]
        public async Task<ActionResult> PauseJob(long jobId)
        {
            var objJob = await scheduleJobService.FirstAsync(jobId);
            if (objJob == null)
            {
                return BadRequest("任务不存在");
            }
            if (!objJob.IsStart)
            {
                return Ok();
            }
            try
            {
                objJob.IsStart = false;
                await uow.BeginAsync();
                await schedulerCenter.PauseJobAsync(objJob.JobGroup, objJob.JobName);
                await uow.CommitAsync();
                return Ok();
            }
            catch (Exception)
            {
                await uow.RollbackAsync();
                throw;
            }
        }

    }
}
