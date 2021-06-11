import request from '@/utils/request'


export function addArticle(data) {
  return request({
    url: 'Article',
    method: 'post',
    data
  })
}

export function updateArticle(data) {
  return request({
    url: 'Article',
    method: 'put',
    data
  })
}



export function deleteArticle(params) {
  return request({
    url: 'Article',
    method: 'delete',
    params
  })
}

export function getArticleList(params) {
    return request({
      url: 'Article',
      method: 'get',
      params
    })
  }



  export function getArticleClass() {
    return request({
      url: 'ArticleClass',
      method: 'get',
    })
  }