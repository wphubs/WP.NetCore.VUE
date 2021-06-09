import request from '@/utils/request'


export function getArticleList(params) {
    return request({
      url: 'Article',
      method: 'get',
      params
    })
  }