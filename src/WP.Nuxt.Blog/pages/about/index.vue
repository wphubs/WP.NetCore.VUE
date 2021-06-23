<template>
    <div style="width: 100%;background:#ededed;min-height: 100vh; ">
        <el-row :gutter="10">
          <el-col :xs="{span: 22, offset: 1}" :sm="{span: 22, offset: 1}" :md="{span: 14, offset: 5}"
              :lg="{span: 14, offset: 5}" :xl="{span: 14, offset:5}">
              <div class="center head">
                <el-avatar :size="200" src="https://www.wptest.cn/images/tempFiles/headimage.jpg"></el-avatar>
              </div>
              <div class="center content">
                Hi,I'm @Wang
              </div>
              <div class="center content">
                C#/.NetCore/Vue/SQL/Uniapp/JS/HTML/CSS/WinForm
              </div>
              <div class="center content">
                <i class="iconfont icon-zuobiao" style="font-size: 20px;">&nbsp;Beijing「北京」</i>&nbsp;&nbsp;&nbsp;
                <i class="iconfont icon-youxiang" style="font-size: 20px;">&nbsp;648779619@qq.com</i>&nbsp;&nbsp;&nbsp;
                <i class="iconfont icon-GitHub1" style="font-size: 20px;">&nbsp;<a href="https://github.com/wphubs/WP.NetCore.VUE" target="_blank" onmouseover="this.style.color='black';" >Github</a></i>
              </div>
              
          </el-col>
      </el-row>
      </div>
  </template>
  
  <script>
    export default {
      data() {
        return {
          count: 0
        }
      },
    
      methods: {
     
      },
      mounted() {
     
      },
    }
  </script>
  
  <style scoped>
    .center{
        display: flex;width: 100%;justify-content: center;  
    }
    .head{
        margin-top:50px;
    }
    .content{
        margin-top:30px;
        font-size: 20px;

    }
    a{
      color: #000;
    }
  </style>