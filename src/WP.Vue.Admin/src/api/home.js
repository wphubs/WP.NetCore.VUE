import request from '@/utils/request'


export function getServerInfo() {
    return request({
      url: 'home/GetServerInfo',
      method: 'get',
    })
  }