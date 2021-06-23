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

  // Plugins to run before rendering page: https://go.nuxtjs.dev/config-plugins
  plugins: [
    '@/plugins/element-ui','@/plugins/vue-headroom','@/plugins/vue-scrollactive', '@/plugins/vue-markdown.js',
    { src: '@/assets/iconfont.js', ssr: false },    { src: '@/plugins/cnzz.js', ssr: false }
  ],

  // Auto import components: https://go.nuxtjs.dev/config-components
  components: true,

  // Modules for dev and build (recommended): https://go.nuxtjs.dev/config-modules
  buildModules: [
    // https://go.nuxtjs.dev/typescript
    '@nuxt/typescript-build',
  ],

  // Modules: https://go.nuxtjs.dev/config-modules
  modules: [
  ],

  // Build Configuration: https://go.nuxtjs.dev/config-build
  build: {
    transpile: [/^element-ui/],
  },
  loading: '~/components/loading.vue',
}
