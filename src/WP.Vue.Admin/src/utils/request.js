import axios from 'axios'
import { MessageBox, Message } from 'element-ui'
import store from '@/store'
import { getToken,getExpTime } from '@/utils/auth'
const service = axios.create({
  baseURL: process.env.VUE_APP_BASE_API, // url = base url + request url
  timeout: 5000 
})
service.interceptors.request.use(
  config => {
    var exp=getExpTime();
    console.log(exp)
    if(exp){
      if(new Date() >new Date(getExpTime())){

        store.dispatch('user/resetToken').then(() => {
          MessageBox.confirm('验证信息已过期，请重新登录', '提示', {
            confirmButtonText: '确定',
            type: 'warning'
          }).then(() => {
            location.reload()
          })
     

          
        })

       
      }
    }

   


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
    //  console.log('code:' + JSON.stringify(response))
    if (!res.Result) {
        Message({
          message: res.Msg,
          type: 'warning',
          duration: 5 * 1000
        })
    
      return Promise.reject(new Error(res.message || 'Error'))
    } else {
      return res.Data
    }
  },
  error => {
    // console.log('err1' + error) // for debug
    Message({
      message: error.message,
      type: 'error',
      duration: 5 * 1000
    })
    return Promise.reject(error)
  }
)

export default service
