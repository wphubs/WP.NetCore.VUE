import Cookies from 'js-cookie'

const TokenKey = 'WP-NetCore-Vue-Token'
const ExpKey = 'WP-NetCore-Vue-Exp'

export function getToken() {
  return Cookies.get(TokenKey)
}

export function setToken(token) {
  return Cookies.set(TokenKey, token)
}
export function removeToken() {
  Cookies.remove(TokenKey)
  Cookies.remove(ExpKey)
}


export function setExpTime(token) {
  return Cookies.set(ExpKey, token)
}

export function getExpTime() {
  return Cookies.get(ExpKey)
}

