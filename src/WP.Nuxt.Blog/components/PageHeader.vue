<!--  -->
<template>
    <div >
        
        <span class="span-tag  hvr-bounce-in"><a href="/">首页</a></span>
        <span class="span-tag  hvr-bounce-in"><a href="/article">文章</a></span>
        <span class="span-tag  hvr-bounce-in"><a href="https://www.wptest.cn/admin">进入后台</a></span>
        <span class="span-tag  hvr-bounce-in"><a href="/">关于</a></span>
        <span class="span-tag nav-item">
          <a class="button is-inverted hvr-grow-rotate"  onmouseover="this.style.color='black';"  href="https://github.com/wphubs/WP.NetCore.VUE" target="_blank">
            <span class="icon">
              <i class="iconfont  icon-GitHub1" ></i>
            </span>
            <span>Github</span>
          </a>
        </span>
    </div>
</template>

<script>

    export default {
        //注入组件
        components: {},
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
<style scoped>
    .span-tag {
      margin-left: 20px;
    }
    a:hover{
      color: #FFF;
    }
  </style>