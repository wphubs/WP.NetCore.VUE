import { constantRoutes } from '@/router'
import Layout from '@/layout'
const _import = require('../../router/_import_' + process.env.NODE_ENV) //获取组件的方法
import { getMenuList } from "@/api/menu";
/**
 * Use meta.role to determine if the current user has permission
 * @param roles
 * @param route
 */
function hasPermission(roles, route) {
  if (route.meta && route.meta.roles) {
    return roles.some(role => route.meta.roles.includes(role))
  } else {
    return true
  }
}


const state = {
  routes: [],
  addRoutes: []
}

const mutations = {
  SET_ROUTES: (state, routes) => {
    state.addRoutes = routes
    state.routes = constantRoutes.concat(routes)
  }
}
function filterAsyncRouter(asyncRouterMap) { //遍历后台传来的路由字符串，转换为组件对象
  const accessedRouters = asyncRouterMap.filter(route => {
    if (route.component) {
      // 
      if (route.component === 'Layout') { //Layout组件特殊处理
        route.component = Layout
      } else {
        route.component = _import(route.component)
      }
    }
    if (route.children && route.children.length) {
      route.children = filterAsyncRouter(route.children)
    }
    return true
  })

  return accessedRouters
}

const actions = {
  generateRoutes({ commit }) {
    return new Promise(resolve => {
      //let accessedRoutes=routerTest;
      //console.log('generateRoutes'+JSON.stringify(accessedRoutes))
      // if (roles.includes('admin')) {
        // accessedRoutes = asyncRoutes || []
      // } else {
      //   accessedRoutes = filterAsyncRoutes(asyncRoutes, roles)
      // }
      // commit('SET_ROUTES', accessedRoutes)
      // resolve(accessedRoutes)
      getMenuList().then((res) => {
          var accessedRoutes=res;
          accessedRoutes = filterAsyncRouter(accessedRoutes)
          accessedRoutes.push( {
            path: 'apidoc',
            component: Layout,
            children: [
              {
                path: 'https://www.wptest.cn/netcore/index.html',
                meta: { title: '接口文档', icon: 'el-icon-link' }
              }
            ]
          })
          accessedRoutes.push( {
            path: 'bloghome',
            component: Layout,
            children: [
              {
                path: 'https://www.wptest.cn',
                meta: { title: '博客首页', icon: 'el-icon-link' }
              }
            ]
          })
          accessedRoutes.push( {
            path: 'Github',
            component: Layout,
            children: [
              {
                path: 'https://github.com/wphubs/WP.NetCore.VUE',
                meta: { title: 'Github源码', icon: 'el-icon-link' }
              }
            ]
          })
          commit('SET_ROUTES', accessedRoutes)
          resolve(accessedRoutes)
        }
      );
    })
  }
}


export default {
  namespaced: true,
  state,
  mutations,
  actions
}
