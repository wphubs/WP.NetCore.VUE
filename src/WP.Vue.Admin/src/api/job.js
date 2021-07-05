import request from '@/utils/request'


export function getJobList() {
    return request({
      url: 'ScheduleJob',
      method: 'get',
    })
  }


  export function addJob(data) {
    return request({
      url: 'ScheduleJob',
      method: 'post',
      data
    })
  }
  

  export function updateJob(data) {
    return request({
      url: 'ScheduleJob',
      method: 'put',
      data
    })
  }
  
  export function deleteJob(params) {
    return request({
      url: 'ScheduleJob',
      method: 'delete',
      params
    })
  }
  

  export function pauseJob(params) {
    return request({
      url: 'ScheduleJob/PauseJob',
      method: 'get',
      params
    })
  }
  

  export function resumeJob(params) {
    return request({
      url: 'ScheduleJob/ResumeJob',
      method: 'get',
      params
    })
  }
  