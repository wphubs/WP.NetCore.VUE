import { constantRoutes } from '@/router'
import Layout from '@/layout'
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

/**
 * Filter asynchronous routing tables by recursion
 * @param routes asyncRoutes
 * @param roles
 */
export function filterAsyncRoutes(routes, roles) {
  const res = []

  routes.forEach(route => {
    const tmp = { ...route }
    if (hasPermission(roles, tmp)) {
      if (tmp.children) {
        tmp.children = filterAsyncRoutes(tmp.children, roles)
      }
      res.push(tmp)
    }
  })

  return res
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
const routerTest=[  {
  path: '/user',
  component: Layout,
  children: [
    {
      path: '/user',
      name: 'user',
      component: () => import('@/views/user/index'),
      meta: { title: '用户管理', icon: 'el-icon-lightning' }
    }
  ]
},

{
  path: '/role',
  component: Layout,
  children: [
    {
      path: '/role',
      name: 'role',
      component: () => import('@/views/role/index'),
      meta: { title: '角色管理', icon: 'el-icon-heavy-rain' }
    }
  ]
}];

const actions = {
  generateRoutes({ commit }) {
    return new Promise(resolve => {

      let accessedRoutes=routerTest;
      //console.log('generateRoutes'+JSON.stringify(accessedRoutes))
      // if (roles.includes('admin')) {
        // accessedRoutes = asyncRoutes || []
      // } else {
      //   accessedRoutes = filterAsyncRoutes(asyncRoutes, roles)
      // }
      commit('SET_ROUTES', accessedRoutes)
      resolve(accessedRoutes)
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
