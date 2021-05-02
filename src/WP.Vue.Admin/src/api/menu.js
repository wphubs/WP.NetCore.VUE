import request from '@/utils/request'

export function getMenuList(params) {
  return request({
    url: 'menu/GetRoleRouter',
    method: 'get',
    params
  })
}

export function deleteMenu(params) {
  return request({
    url: 'menu',
    method: 'delete',
    params
  })
}


export function updateMenu(data) {
  return request({
    url: 'menu',
    method: 'put',
    data
  })
}


export function getAll(params) {
  return request({
    url: 'menu',
    method: 'get',
    params
  })
}


export function getPageMenuList(params) {
  return request({
    url: 'menu/GetPageMenuList',
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