<template>
  <div class="app-container">
    <el-card style="width: 100%">
      <div style="font-size: large; font-weight: 600">Workplace</div>
      <div
        style="
          display: flex;
          width: 100%;
          margin-top: 20px;
          align-items: center;
          position: relative;
          display: flex;        "
      >
        <div>
          <pan-thumb
            image="https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png"
            class="panThumb"
          />
          <!-- <el-avatar :size="80" src=""></el-avatar> -->
        </div>
        <div style="padding-left: 100px; line-height: 40px; flex: 1">
          <div style="font-size: 20px">
            {{ getTimeFix() }}，米拉娜，{{ getWelcomeText() }}
          </div>
          <div style="color: #8d8d8d; font-size: 15px">
            前端工程师 <el-divider direction="vertical"></el-divider>某某某公司
            <el-divider direction="vertical"></el-divider> 某某某事业部
          </div>
        </div>
        <div style="width: 200px">
          <span>好好学习</span>
          <el-divider direction="vertical"></el-divider>
          <span>天天向上</span>
        </div>
      </div>
    </el-card>
    <el-row :gutter="40" class="panel-group">
      <el-col :xs="12" :sm="12" :lg="6" class="card-panel-col">
        <div class="card-panel">
          <div class="card-panel-icon-wrapper icon-people">
            <svg-icon icon-class="peoples" class-name="card-panel-icon" />
          </div>
          <div class="card-panel-description">
            <div class="card-panel-text">New Visits</div>
            <count-to
              :start-val="0"
              :end-val="102400"
              :duration="2600"
              class="card-panel-num"
            />
          </div>
        </div>
      </el-col>
      <el-col :xs="12" :sm="12" :lg="6" class="card-panel-col">
        <div class="card-panel">
          <div class="card-panel-icon-wrapper icon-message">
            <svg-icon icon-class="message" class-name="card-panel-icon" />
          </div>
          <div class="card-panel-description">
            <div class="card-panel-text">Messages</div>
            <count-to
              :start-val="0"
              :end-val="81212"
              :duration="3000"
              class="card-panel-num"
            />
          </div>
        </div>
      </el-col>
      <el-col :xs="12" :sm="12" :lg="6" class="card-panel-col">
        <div class="card-panel">
          <div class="card-panel-icon-wrapper icon-money">
            <svg-icon icon-class="money" class-name="card-panel-icon" />
          </div>
          <div class="card-panel-description">
            <div class="card-panel-text">Purchases</div>
            <count-to
              :start-val="0"
              :end-val="9280"
              :duration="3200"
              class="card-panel-num"
            />
          </div>
        </div>
      </el-col>
      <el-col :xs="12" :sm="12" :lg="6" class="card-panel-col">
        <div class="card-panel">
          <div class="card-panel-icon-wrapper icon-shopping">
            <svg-icon icon-class="shopping" class-name="card-panel-icon" />
          </div>
          <div class="card-panel-description">
            <div class="card-panel-text">Shoppings</div>
            <count-to
              :start-val="0"
              :end-val="13600"
              :duration="3600"
              class="card-panel-num"
            />
          </div>
        </div>
      </el-col>
    </el-row>

    <div style="display: flex">
      <div style="width: 450px;">
        <el-card class="box-card-component">
          <div slot="header" class="box-card-header">
            <!-- <img src="https://wpimg.wallstcn.com/e7d23d71-cf19-4b90-a1cc-f56af8c0903d.png"> -->
            <mallki class-name="mallki-text" text="WP.NetCore.VUE" />
            <div class="server-info" >
              
              <div><span class="title">服务器：</span><span>{{ serverInfo.Platform }}</span></div>
              <div>
                <span class="title">.NETCore版本：</span
                ><span>{{ serverInfo.FrameworkDescription }}</span>
              </div>
              <div>
                <span class="title">环境变量：</span
                ><span>{{ serverInfo.EnvironmentName }}</span>
              </div>
              <div>
                <span class="title">系统架构：</span
                ><span>{{ serverInfo.OSArchitecture }}</span>
              </div>
           
              <div>
                <span class="title">内存占用：</span
                ><span>{{ serverInfo.MemoryFootprint }}</span>
              </div>
              <div>
                <span class="title">运行时长：</span><span>{{ serverInfo.WorkingTime }}</span>
              </div>
            </div>
          </div>
          <div>
            <div style="padding-top: 10px" class="progress-item">
              <span>C#</span>
              <el-progress :percentage="60" />
            </div>
            <div class="progress-item">
              <span>Vue</span>
              <el-progress :percentage="26" />
            </div>
            <div class="progress-item">
              <span>JavaScript</span>
              <el-progress :percentage="9" />
            </div>
            <div class="progress-item">
              <span>SCSS</span>
              <el-progress :percentage="2" />
            </div>
          </div>
        </el-card>
      </div>
       <el-card style=" flex: 1;margin-left:20px">
      <div id="main" style="width: 100%; height: 360px"></div>
    </el-card>
    </div>
   
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import CountTo from "vue-count-to";
import * as echarts from "echarts";
import PanThumb from "@/components/PanThumb";
import Mallki from "@/components/TextHoverEffect/Mallki";
import { getServerInfo } from "@/api/home";
import * as signalR from "@aspnet/signalr";
import {
  getToken,
  getExpTime
} from '@/utils/auth'
export default {
  components: {
    CountTo,
    PanThumb,
    Mallki,
  },
  data() {
    return {
      serverInfo: {},
      connection:{},
    };
  },
  created() {
    let that = this;
    that.connection= new signalR.HubConnectionBuilder().withUrl(`${process.env.VUE_APP_BASE_API.replace('/api/','')}/welcomehub`, {
					accessTokenFactory: () =>getToken()
				})
				.build()
        that.connection.on('ReceiveEventAsync', function (res) {
        that.$notify({
          title: '欢迎回来',
          message: res.Content,
          type: 'success',
          duration: 0
        });
       
      });
      that.connection.start();
      that.$notify({
          title: '提示',
          message: '为了演示使用，无权限按钮没有做隐藏处理，可打开自定义指令代码注释实现隐藏',
          type: 'warning',
          duration: 0
        });
  },
  beforeDestroy() {
            this.connection.stop();
  },
  mounted() {
    this.getServer();
    // 基于准备好的dom，初始化echarts实例
    var myChart = echarts.init(document.getElementById("main"));
    var option = {
      title: {
        text: "折线图",
      },
      tooltip: {
        trigger: "axis",
      },
      legend: {
        data: ["邮件营销", "联盟广告", "视频广告", "直接访问", "搜索引擎"],
      },
      grid: {
        left: "3%",
        right: "4%",
        bottom: "3%",
        containLabel: true,
      },
      toolbox: {
        feature: {
          saveAsImage: {},
        },
      },
      xAxis: {
        type: "category",
        boundaryGap: false,
        data: ["周一", "周二", "周三", "周四", "周五", "周六", "周日"],
      },
      yAxis: {
        type: "value",
      },
      series: [
        {
          name: "邮件营销",
          type: "line",
          stack: "总量",
          data: [120, 132, 101, 134, 90, 230, 210],
        },
        {
          name: "联盟广告",
          type: "line",
          stack: "总量",
          data: [220, 182, 191, 234, 290, 330, 310],
        },
        {
          name: "视频广告",
          type: "line",
          stack: "总量",
          data: [150, 232, 201, 154, 190, 330, 410],
        },
        {
          name: "直接访问",
          type: "line",
          stack: "总量",
          data: [320, 332, 301, 334, 390, 330, 320],
        },
        {
          name: "搜索引擎",
          type: "line",
          stack: "总量",
          data: [820, 932, 901, 934, 1290, 1330, 1320],
        },
      ],
    };
    myChart.setOption(option);
  },
  methods: {
    getServer() {
      getServerInfo().then((res) => {
        this.serverInfo = res;
      });
    },
    getWelcomeText() {
      const arr = ["休息一会儿吧", "准备吃什么呢?", "要不要打一把 DOTA"];
      const index = Math.floor(Math.random() * arr.length);
      return arr[index];
    },
    getTimeFix() {
      const time = new Date();
      const hour = time.getHours();
      return hour < 9
        ? "早上好"
        : hour <= 11
        ? "上午好"
        : hour <= 13
        ? "中午好"
        : hour < 20
        ? "下午好"
        : "晚上好";
    },
  },
};
</script>

<style lang="scss" scoped>
.server-info{
  padding-top: 20px;line-height:30px;
  .title{
    font-weight: 700;
    width: 150px;
    margin-right: 15px;
  }
}
.panThumb {
  z-index: 100;
  height: 90px !important;
  width: 90px !important;
  position: absolute !important;
  top: -5px;
  left: 0px;
  border: 1px solid #eeeeee;
  background-color: #fff;
  margin: auto;
  box-shadow: none !important;
  ::v-deep .pan-info {
    box-shadow: none !important;
  }
}
.panel-group {
  margin-top: 18px;

  .card-panel-col {
    margin-bottom: 32px;
  }

  .card-panel {
    height: 108px;
    cursor: pointer;
    font-size: 12px;
    position: relative;
    overflow: hidden;
    color: #666;
    background: #fff;
    box-shadow: 4px 4px 40px rgba(0, 0, 0, 0.05);
    border-color: rgba(0, 0, 0, 0.05);

    &:hover {
      .card-panel-icon-wrapper {
        color: #fff;
      }

      .icon-people {
        background: #40c9c6;
      }

      .icon-message {
        background: #36a3f7;
      }

      .icon-money {
        background: #f4516c;
      }

      .icon-shopping {
        background: #34bfa3;
      }
    }

    .icon-people {
      color: #40c9c6;
    }

    .icon-message {
      color: #36a3f7;
    }

    .icon-money {
      color: #f4516c;
    }

    .icon-shopping {
      color: #34bfa3;
    }

    .card-panel-icon-wrapper {
      float: left;
      margin: 14px 0 0 14px;
      padding: 16px;
      transition: all 0.38s ease-out;
      border-radius: 6px;
    }

    .card-panel-icon {
      float: left;
      font-size: 48px;
    }

    .card-panel-description {
      float: right;
      font-weight: bold;
      margin: 26px;
      margin-left: 0px;

      .card-panel-text {
        line-height: 18px;
        color: rgba(0, 0, 0, 0.45);
        font-size: 16px;
        margin-bottom: 12px;
      }

      .card-panel-num {
        font-size: 20px;
      }
    }
  }
}

@media (max-width: 550px) {
  .card-panel-description {
    display: none;
  }

  .card-panel-icon-wrapper {
    float: none !important;
    width: 100%;
    height: 100%;
    margin: 0 !important;

    .svg-icon {
      display: block;
      margin: 14px auto !important;
      float: none !important;
    }
  }
}
</style>
