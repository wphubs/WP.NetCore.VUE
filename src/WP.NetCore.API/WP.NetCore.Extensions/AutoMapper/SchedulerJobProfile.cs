using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NetCore.Model.Dto.ScheduleJob;
using WP.NetCore.Model.EntityModel;

namespace WP.NetCore.Extensions.AutoMapper
{
 
    public class SchedulerJobProfile : Profile
    {
        public SchedulerJobProfile()
        {
            CreateMap<AddScheduleJobDto, ScheduleJob>();
            CreateMap<UpdateScheduleJobDto, ScheduleJob>();
        }

    }
}
