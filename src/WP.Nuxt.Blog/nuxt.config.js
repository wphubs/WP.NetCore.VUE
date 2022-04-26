export default {
  // Global page headers: https://go.nuxtjs.dev/config-head
  head: {
    title: 'i tell you',
    htmlAttrs: {
      lang: 'zh'
    },
    meta: [
      { charset: 'utf-8' },
      { name: 'viewport', content: 'width=device-width, initial-scale=1' },
      { hid: 'description', name: 'description', content: '' }
    ],
    script: [
      // { src: 'https://cdn.bootcss.com/jquery/1.11.3/jquery.min.js' },
      { src: 'https://cdn.bootcdn.net/ajax/libs/jquery/3.5.1/jquery.min.js' },
      { src: 'https://zz.bdstatic.com/linksubmit/push.js' },
      { src: 'https://hm.baidu.com/hm.js?0c5af6448f81440980cfbc5e2771c5a0' },
    ],
    link: [
      { rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' }
    ]
  },
 
  // Global CSS: https://go.nuxtjs.dev/config-css
  css: [
    'element-ui/lib/theme-chalk/index.css', 'animate.css/animate.css','bulma/css/bulma.css','hover.css/css/hover.css',
    '@/assets/iconfont.css','element-ui/lib/theme-chalk/display.css'
  ],
  plugins: [
    '@/plugins/element-ui','@/plugins/vue-headroom','@/plugins/vue-scrollactive', '@/plugins/vue-markdown.js',
    { src: '@/assets/iconfont.js', ssr: false },       { src: '@plugins/uweb.js', ssr: false },
  ],
  components: true,

  buildModules: [
    '@nuxt/typescript-build',
  ],
  modules: [  '@nuxtjs/axios','@nuxtjs/proxy','@nuxtjs/sitemap', '@nuxtjs/robots'],
  axios: {
    proxy: true
  },
  proxy: {
    '/api/': {
      target: 'http://www.moji.com/',
      changeOrigin: true, // 表示是否跨域
      pathRewrite: {
        '^/api/': '/'
      }
    }
  },
  build: {
    transpile: [/^element-ui/],
    vendor: ["axios"]
  },
  loading: '~/components/loading.vue',
  publicRuntimeConfig: {
    baseURL:  'http://localhost:8081/api/'
    //baseURL:  'https://www.wptest.cn/netcore/api/'
  },
}
