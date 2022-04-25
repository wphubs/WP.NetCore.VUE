import router from './router'
import store from './store'
import { Message } from 'element-ui'
import NProgress from 'nprogress' // progress bar
import 'nprogress/nprogress.css' // progress bar style
import { getToken } from '@/utils/auth' // get token from cookie
import getPageTitle from '@/utils/get-page-title'

NProgress.configure({ showSpinner: false }) // NProgress Configuration

const whiteList = ['/login', '/auth-redirect'] // no redirect whitelist


var getRouter;
router.beforeEach(async(to, from, next) => {
  // start progress bar
  NProgress.start()

  // set page title
  document.title = getPageTitle(to.meta.title)

  // determine whether the user has logged in
  const hasToken = getToken()

  if (hasToken) {
   
    if (to.path === '/login') {
      // if is logged in, redirect to the home page
      next({ path: '/' })
      NProgress.done() // hack: https://github.com/PanJiaChen/vue-element-admin/pull/2939
    } else {
      const hasRoles = store.getters.roles && store.getters.roles.length > 0
      if (hasRoles) {
        next()
      } else {
        try {
          if (!getRouter) {
            await store.dispatch('user/getInfo')
            console.log('1')
            var accessRoutes = await store.dispatch('permission/generateRoutes')
            console.log('2:'+JSON.stringify(accessRoutes))
            accessRoutes.push({ path: '*', redirect: '/404', hidden: true });
            getRouter=accessRoutes;
            router.addRoutes(accessRoutes)
            saveObjArr('router', getRouter) //存储路由到localStorage
            next({ ...to, replace: true })
          }else{
            getRouter = getObjArr('router') //拿到路由
            next()
          }
        } catch (error) {
          console.log('error')
          await store.dispatch('user/resetToken')
          Message.error(error || 'Has Error')
          next(`/login`)
          NProgress.done()
        }
      }
    }
  } else {
    /* has no token*/

    if (whiteList.indexOf(to.path) !== -1) {
      next() // in the free login whitelist, go directly
    } else {
      next(`/login`)
      NProgress.done()
    }

    //if (whiteList.indexOf(to.path) !== -1) {
      // in the free login whitelist, go directly
      next()
    // } else {
    //   // other pages that do not have permission to access are redirected to the login page.
    //   next(`/login?redirect=${to.path}`)
    //   NProgress.done()
    // }
  }
})

function saveObjArr(name, data) { //localStorage 存储数组对象的方法
  localStorage.setItem(name, JSON.stringify(data))
}

function getObjArr(name) { //localStorage 获取数组对象的方法
  return JSON.parse(window.localStorage.getItem(name));

}

router.afterEach(() => {
  // finish progress bar
  NProgress.done()
})
