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
              <div class="center content" style="font-size: 15px;">
                #足球&nbsp;&nbsp;&nbsp;#F1&nbsp;&nbsp;&nbsp;#海贼王&nbsp;&nbsp;&nbsp;#DOTA
              </div>
              <div class="center content">
                <i class="iconfont icon-zuobiao" style="font-size: 20px;">&nbsp;Beijing「北京」</i>&nbsp;&nbsp;&nbsp;
                <i class="iconfont icon-youxiang" style="font-size: 20px;">&nbsp;648779619@qq.com</i>&nbsp;&nbsp;&nbsp;
                <i class="iconfont icon-GitHub1" style="font-size: 20px;">&nbsp;<a href="https://github.com/wphubs/WP.NetCore.VUE" target="_blank" onmouseover="this.style.color='black';" >Github</a></i>
              </div>
          </el-col>
      </el-row>
      <el-row  type="flex" class="row-bg" justify="space-around" style="margin-top: 100px;text-align: center;">
        <el-col :xs="22" :sm="22" :md="5" :lg="5" :xl="5">
          <img  src="https://www.wptest.cn/images/barca.jpg" style="height: 200px;width: 350px;">
        </el-col>
        <el-col :xs="22" :sm="22" :md="5" :lg="5" :xl="5">
          <img  src="https://www.wptest.cn/images/f1.jpg"  style="height: 200px;width: 350px;" >
        </el-col>
        <el-col :xs="22" :sm="22" :md="5" :lg="5" :xl="5">
          <img  src="https://www.wptest.cn/images/hz.jpg" style="height: 200px;width: 350px;" >
        </el-col>
        <el-col :xs="22" :sm="22" :md="5" :lg="5" :xl="5">
          <img  src="https://www.wptest.cn/images/dota2.jpeg" style="height: 200px;width: 350px;" >
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