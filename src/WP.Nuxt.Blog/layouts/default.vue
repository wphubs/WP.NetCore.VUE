<template>
  <div>
    <div style="z-index: 999; font-size: 16px;padding-right: 10px;background: #000;display: flex; justify-content: flex-end;height: 70px;align-items: center;">
      <PageHeader />
    </div>
    <Nuxt style="padding-bottom: 30px;" />
    <PageFooter />
  </div>
</template>
<script>
  import PageHeader from '~/components/PageHeader.vue'
  import PageFooter from '~/components/PageFooter.vue'
  export default {
      //注入组件
      components: {
        PageHeader,
        PageFooter
      },
      data() {
          return {

          };
      },
      //监听属性
      computed: {},
      watch: {},
      methods: {

      },
      //生命周期 - 创建完成（可以访问当前this实例）
      created() {

      },
      //生命周期 - 挂载完成（可以访问DOM元素）
      mounted() {

      },
  }
</script>
<style>
  html {
    font-family:
      '微软雅黑',
      -apple-system,
      BlinkMacSystemFont,
      'Segoe UI',
      Roboto,
      'Helvetica Neue',
      Arial,
      sans-serif;
    font-size: 16px;
    word-spacing: 1px;
    -ms-text-size-adjust: 100%;
    -webkit-text-size-adjust: 100%;
    -moz-osx-font-smoothing: grayscale;
    -webkit-font-smoothing: antialiased;
    box-sizing: border-box;
  }

  a {
    color: #FFF;
  }

  a:FOCUS {
    color: #FFF;
    ;

  }

  *,
  *::before,
  *::after {
    box-sizing: border-box;
    margin: 0;
  }

  .button--green {
    display: inline-block;
    border-radius: 4px;
    border: 1px solid #3b8070;
    color: #3b8070;
    text-decoration: none;
    padding: 10px 30px;
  }

  .button--green:hover {
    color: #fff;
    background-color: #3b8070;
  }

  .button--grey {
    display: inline-block;
    border-radius: 4px;
    border: 1px solid #35495e;
    color: #35495e;
    text-decoration: none;
    padding: 10px 30px;
    margin-left: 15px;
  }

  .button--grey:hover {
    color: #fff;
    background-color: #35495e;
  }
</style>