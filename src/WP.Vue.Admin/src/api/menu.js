import request from '@/utils/request'

export function getMenuList(params) {
  return request({
    url: 'Menu',
    method: 'get',
    params
  })
}