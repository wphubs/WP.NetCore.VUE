import request from '@/utils/request'


export function getJobList() {
    return request({
      url: 'ScheduleJob',
      method: 'get',
    })
  }