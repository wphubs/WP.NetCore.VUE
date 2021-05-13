import request from '@/utils/request'


export function addRole(data) {
  return request({
    url: 'role',
    method: 'post',
    data
  })
}

export function setRoleMenu(data) {
  return request({
    url: 'role/SetRoleMenu',
    method: 'post',
    data
  })
}


export function getRoleMenu(params) {
  return request({
    url: 'role/GetRoleMenu',
    method: 'get',
    params
  })
}


export function updateRole(data) {
  return request({
    url: 'role',
    method: 'put',
    data
  })
}

export function deleteRole(params) {
  return request({
    url: 'role',
    method: 'delete',
    params
  })
}


export function getPage(params) {
  return request({
    url: 'role/GetPage',
    method: 'get',
    params
  })
}
export function getAll(params) {
  return request({
    url: 'role',
    method: 'get',
    params
  })
}