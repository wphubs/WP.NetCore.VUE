<template>
  <div>
    <div style="position:absolute;top: 20px;right:  50px;z-index: 999;color: #fff; font-size: 16px;  ">
      <span class="span-tag  hvr-bounce-in"><a href="/">首页</a></span>
      <span class="span-tag  hvr-bounce-in"><a href="/article">文章</a></span>
      <span class="span-tag  hvr-bounce-in">进入后台</span>
      <span class="span-tag  hvr-bounce-in">关于</span>
      <span class="span-tag nav-item">
        <a class="button is-inverted hvr-grow-rotate" href="https://github.com/wphubs/WP.NetCore.VUE" target="_blank">
          <span class="icon">
            <!-- <img src="../static/images/github.png" height="30" width="50" alt=""> -->
            <i class="iconfont  icon-GitHub1" ></i>
            
            <!-- <i class="fab fa-github"></i> -->
          </span>
          <span>Github</span>
        </a>
      </span>
    </div>
    <Nuxt />
  </div>
</template>
<style scoped>
  .span-tag {
    margin-left: 20px;
  }
  a:hover{
    color: #FFF;
  }
</style>
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