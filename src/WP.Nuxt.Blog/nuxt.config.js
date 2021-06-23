export default {
  // Global page headers: https://go.nuxtjs.dev/config-head
  head: {
    title: 'WP.Nuxt.Blog',
    // htmlAttrs: {
    //   lang: 'en'
    // },
    meta: [
      { charset: 'utf-8' },
      { name: 'viewport', content: 'width=device-width, initial-scale=1' },
      { hid: 'description', name: 'description', content: '' }
    ],
    script: [
      { src: 'https://cdn.bootcss.com/jquery/1.11.3/jquery.min.js' },
      // { src: 'https://s9.cnzz.com/z_stat.php?id=1280044313&web_id=1280044313', ssr: false }
    ],
    link: [
      { rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' }
    ]
  },

  // Global CSS: https://go.nuxtjs.dev/config-css
  css: [
    'element-ui/lib/theme-chalk/index.css', 'animate.css/animate.css','bulma/css/bulma.css','hover.css/css/hover.css',
    '@/assets/iconfont.css',
  ],
  plugins: [
    '@/plugins/element-ui','@/plugins/vue-headroom','@/plugins/vue-scrollactive', '@/plugins/vue-markdown.js',
    { src: '@/assets/iconfont.js', ssr: false },    { src: '@/plugins/cnzz.js', ssr: false }
  ],
  components: true,

  buildModules: [
    '@nuxt/typescript-build',
  ],
  modules: [
  ],
  build: {
    transpile: [/^element-ui/],
  },
  loading: '~/components/loading.vue',
  publicRuntimeConfig: {
    baseURL:  'http://localhost:8081/api/'
  },
  privateRuntimeConfig: {
    apiSecret: 'www.baidu.com'
  }
}
