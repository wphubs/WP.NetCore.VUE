<template>
  <div>
    <el-row type="flex"
    >
      <el-col  :xs="24" :sm="24" :md="24" :lg=" {span:4, offset: 2}" :xl=" {span:4, offset: 2}"   style="text-align: center;" class="hidden-md-and-down">
        <div class="sticky" style="width: 200px;padding-top: 50px;text-align: center;">
          <div class="options" id="optnCard">
            <p class="options-text disabled">分类</p>
            <div v-for="(itemClass,index) in classList" :key="index">
              <a @click.prevent="clickClass(itemClass)" class="option options-title active">#{{itemClass.ClassName}}</a>
            </div>

          </div>
        </div>
      </el-col>
      <el-col :xs="24" :sm="24" :md="24" :lg="11" :xl="11"  style="padding-top: 100px;">
        <div id="myscoll" ></div>
        <div class="blog-body">
          <div id="articleScoll" v-for="(item,indexArticle) in articleList" :key="indexArticle">
            <section class="section">
              <el-row :gutter="10">
                <el-col :xs="24" :sm="24" :md="18" :lg="18" :xl="18">
                  <div style="display: flex;align-items: center;">
                    <span class="tag is-primary is-medium">{{item.ClassName}}</span>
                  <span style="font-size: 25px;font-weight: bolder;margin-left: 10px;"><a
                    :href="'/article/detail/'+item.Id" style="color: #000;">{{item.Title}}</a></span>
                  </div>
                </el-col>
             
              </el-row>
              <div class="article-content" v-html="item.Content"></div>
              <div style="display: flex;justify-content: flex-end;font-size: 18px;color: #8d8d8d;margin-top: 20px;">{{item.CreateTime}}</div>

            </section>
            <el-divider></el-divider>
          </div>
        </div>

      </el-col>
      <el-col  :xs="24" :sm="24" :md="24" :lg=" {span:4, offset: 1}" :xl="{span:4, offset: 1}" class="hidden-md-and-down" style="padding-top: 100px;">
        <div>
          <div class="weather">
            <img v-if="currentWeather.Fcondition_day=='多云'" src="../../static/images/dy.png"
              style="width: 50px;height: 50px;margin-right: 10px;">
            <img v-else-if="currentWeather.Fcondition_day=='晴'" src="../../static/images/q.png"
              style="width: 50px;height: 50px;margin-right: 10px;">
            <img v-else-if="currentWeather.Fcondition_day=='小雨'" src="../../static/images/xy.png"
              style="width: 50px;height: 50px;margin-right: 10px;">
            <img v-else src="../../static/images/lzy.png" style="width: 50px;height: 50px;margin-right: 10px;">
            {{ currentCity}}
            {{currentData}}
            {{currentWeather.Fcondition_day}} {{currentWeather.Ftemp_high}}° {{currentWeather.Fwind_dir_day}}
          </div>
  
          <div class="card">
            <div class="card-image">
              <figure class="image is-5by3">
                <img src="https://www.wptest.cn/images/1597825271636.jpg" alt="Placeholder image">
              </figure>
            </div>
            <div class="card-content">
              <div class="media">
                <p>今日微语</p>
              </div>
              <div class="content">
                {{todaySentence.Content}}
                <br>
                <div style="font-size: 14px;margin-top: 5px;margin-bottom: 20px;">——
                  <span v-if="todaySentence.Source">《{{todaySentence.Source}}》</span>{{todaySentence.Author}}

                </div>
         
                <time datetime="2016-1-1">11:09 PM - 1 Jan 2021</time>
              </div>
            </div>
          </div>
  
  
          
          <div>
            <nav class="panel" style="margin-top: 50px;">
              <p class="panel-heading">
                简介
              </p>
              <span class="panel-icon">
                  <i class="fas fa-book" aria-hidden="true"></i>
               </span>
              <div style="padding: 0px  10px 20px 10px;;line-height: 25px;" >
                WP.Nuxt.Blog是基于Nuxt.js框架开发的个人博客，通过SSR服务器端渲染生成静态页面。
                并使用.NetCore+Vue前后端分离方式开发了一套搭配使用的后台管理系统，实现了相关的基础功能，如：用户角色管理、动态路由、策略授权、实时通信、以及基于AOP的缓存、事务等。
              <br>
                Github源码地址：
              <a style="color: #409eff;"
                href="https://github.com/wphubs/WP.NetCore.VUE">https://github.com/wphubs/WP.NetCore.VUE</a>
              
              </div>
            </nav>
          </div>


  
          <div>
            <nav class="panel" style="margin-top: 50px;">
              <p class="panel-heading">
                推荐文章
              </p>
              <a class="panel-block is-active" :href="'/article/detail/'+item.Id" style="color: #409eff;"
                v-for="(item,indexArticle) in hotArticleList" :key="indexArticle">
                <span class="panel-icon">
                  <i class="fas fa-book" aria-hidden="true"></i>
                </span>
                {{indexArticle+1}}、{{item.Title}}
              </a>
            </nav>
          </div>
        </div>
       
      </el-col>
    </el-row>

  </div>
</template>

<script>
  import Rellax from "rellax";
  // import axios from "axios";
  export default {
    data() {
      return {
        currentData: '',
        currentWeather: '',
        currentCity: '',
      };
    },
    methods: {
    
      async clickClass(item) {
        var { data } = await this.$axios.get(`${this.baseURL}Article/GetArticleList?classId=${item.Id}&pageIndex=1&pageSize=20`);
        this.articleList = data.Data;
      },
    },
    async asyncData({ params, $config, $axios }) {

      var { data: articleClass } = await $axios.get(
        `${$config.baseURL}ArticleClass`
      );
        
      articleClass.unshift({ "ClassName": "全部", "Id": '' })
      var { data: article } = await $axios.get(
        `${$config.baseURL}Article/GetArticleList?pageIndex=1&pageSize=20`
      );
           
      var { data: hotArticle } = await $axios.get(
        `${$config.baseURL}Article/GetHotArticleList`
      );
      
         var { data: nothingData } = await $axios.get(
        `${$config.baseURL}Nothing`
      );
        
      return { baseURL: $config.baseURL, articleList: article.Data, classList: articleClass, hotArticleList: hotArticle.Data,todaySentence:nothingData };
    },
    head() {
      return {
        link: [
          { rel: 'stylesheet', href: 'nekoscroll.css' },
        ],
        script: [
          {
            charset: 'utf-8',
            src: 'nekoscroll.min.js',
            body: true
          },
        ]
      }
    },
    mounted() {
      //  this.$nextTick(() => {
      //           this.$nuxt.$loading.start()
      //           setTimeout(() => this.$nuxt.$loading.finish(), 700)
      //       })
      if (process.browser) {
        $("#myscoll").nekoScroll({
          top: '70px',
          zoom: 0.7,
          blog_body: ".blog-body",
        });

      }
      this.$axios.get(`/api/mojiweather/forecast.php`)
        .then(res => {
          var result = res.data.data;
          this.currentData = result.time.monthG
          this.currentWeather = result.forecastDayList[1]
          this.currentCity = result.city.pname + ' ' + result.city.name
        })


      var rellax = new Rellax('.rellax', {
        breakpoints: [576, 768, 1201]
      });
    },
  };
</script>

<style scoped>
  .section {
    padding: 0.5rem 1.5rem;
}
a{word-break: break-all;}

  .weather{
    height: 80px;display: flex;align-items: center;width: 100%;
                  justify-content: center;
                  border-radius: 10px;
                  color: #fff;
                  font-size: 16px;margin-bottom: 50px;
                  padding: 20px;
                  background: rgba(0, 0, 0, .2)
  }
  .article-content {
    display: -webkit-box;
    -webkit-box-orient: vertical;
    -webkit-line-clamp: 4;
    overflow: hidden;
    max-height: 150px;
    margin-top: 10px;
  }

  .heading {
    font-size: 27px;
    font-weight: bolder;
  }

  /* .section {
  position: relative;
} */
  /* section.san-francisco {
  margin-top: 100px;
} */
  .options {
    border-radius: 12px;
    border: 1px solid #ccc;
    background-color: white;
    width: 90%;
    padding: 2.5rem;
    margin-top: 4rem;
    height: auto;
  }

  .options a {
    display: block;
  }

  .options-text {
    font-size: medium;
    text-transform: uppercase;
    letter-spacing: 0.1rem;
    margin: 1rem 0;
  }

  .options-text--margin {
    margin-top: 3rem;
  }

  .options-title {
    font-size: 20px;
    padding: 1rem 0;
    color: #222;
  }

  .sticky {
    position: sticky;
    top: 0;
    z-index: 4;
  }

  .option:hover {
    color: #409eff;
  }
</style>