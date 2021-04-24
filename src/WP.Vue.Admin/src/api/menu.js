import request from '@/utils/request'

export function getMenuList(params) {
  return request({
    url: 'menu/GetRoleRouter',
    method: 'get',
    params
  })
}

export function getAll(params) {
  return request({
    url: 'menu',
    method: 'get',
    params
  })
}


export function addMenu(data) {
  return request({
    url: 'menu',
    method: 'post',
    data
  })
}