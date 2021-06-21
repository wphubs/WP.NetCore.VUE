import axios from 'axios'
import {
  MessageBox,
  Message
} from 'element-ui'
import store from '@/store'
import {
  getToken,
  getExpTime
} from '@/utils/auth'
const service = axios.create({
  baseURL: process.env.VUE_APP_BASE_API, // url = base url + request url
  timeout: 5000
})
service.interceptors.request.use(
  config => {
    if (store.getters.token) {
      config.headers['Authorization'] = 'Bearer ' + getToken()
    }
    return config
  },
  error => {
    console.log(error) // for debug
    return Promise.reject(error)
  }
)
service.interceptors.response.use(
  response => {
    const res = response.data
    return res
  },
  error => {
    console.log('err1' + JSON.stringify(error.response.data)) // for debug
    if(error.response.status==429){
      Message({
        message: '访问过于频繁,歇会吧',
        type: 'error',
        duration: 5 * 1000
      })
    }else if(error.response.status==401){
      store.dispatch('user/resetToken').then(() => {
        MessageBox.confirm('验证信息已过期，请重新登录', '提示', {
          confirmButtonText: '确定',
          type: 'warning'
        }).then(() => {
          location.reload()
        })
      })
    }
    else{
      Message({
        message: error.response.data,
        type: 'error',
        duration: 5 * 1000
      })
    }
  
    return Promise.reject(error)
  }
)

export default service
