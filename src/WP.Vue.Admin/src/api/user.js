import request from '@/utils/request'

export function login(data) {
  return request({
    url: 'login',
    method: 'post',
    data
  })
}

export function getInfo(token) {
  return request({
    url: 'user/GetUserInfo',
    method: 'get',
    params: { token }
  })
}

export function addUser(data) {
  return request({
    url: 'user',
    method: 'post',
    data
  })
}

export function updateUser(data) {
  return request({
    url: 'user',
    method: 'put',
    data
  })
}

export function deleteUser(params) {
  return request({
    url: 'user',
    method: 'delete',
    params
  })
}

export function logout() {
  return request({
    url: '/vue-admin-template/user/logout',
    method: 'post'
  })
}

export function getList(params) {
  return request({
    url: 'user',
    method: 'get',
    params
  })
}