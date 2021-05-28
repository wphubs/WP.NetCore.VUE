import request from '@/utils/request'


export function addRole(data) {
  return request({
    url: 'role',
    method: 'post',
    data
  })
}

export function setPermission(data) {
  return request({
    url: 'role/setPermission',
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


export function getRoleList() {
  return request({
    url: 'role',
    method: 'get',
    
  })
}




export function getPermission(params) {
  return request({
    url: 'role/GetPermission',
    method: 'get',
    params
  })
}