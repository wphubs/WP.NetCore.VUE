import request from '@/utils/request'



export function getRequestLog(params) {
    return request({
      url: 'ServerLog/GetRequestLog',
      method: 'get',
      params
    })
  }
  