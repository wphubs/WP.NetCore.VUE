<template>
   <div>
        <el-row  type="flex" class="row-bg" justify="space-around">
            <el-col :span="3" :offset="2" style="text-align: center;" >
                <div class="sticky" style="width: 200px;padding-top: 50px;text-align: center;">
                    <div class="options" id="optnCard">
                      <p class="options-text disabled">分类</p>
                      <div v-for="(itemClass,index) in classList" :key="index">
                            <a @click.prevent="clickClass(itemClass)" class="option options-title active">#{{itemClass.ClassName}}</a>
                      </div>
                     
                    </div>
                  </div>
            </el-col>
            <el-col :span="10" style="padding-top: 70px;" >
              <div id="myscoll"></div>
              <div class="blog-body">
              <div  id="articleScoll" v-for="(item,indexArticle) in articleList" :key="indexArticle">
                <section class="section" >
                  <div style="display: flex;align-items: center;width: 100%;">
                    <div style="flex: 1;display: flex;align-items: center;">
                      <span class="tag is-primary is-medium">{{item.ClassName}}</span>
                      <span style="font-size: 25px;font-weight: bolder;margin-left: 10px;"><a :href="'/article/detail/'+item.Id" style="color: #000;">{{item.Title}}</a></span></div>
                    <div style="width: 200px;font-size: 18px;color: #8d8d8d;">{{item.CreateTime}}</div>
                  </div>
                <div class="article-content" v-html="item.Content"></div> 
              </section>
              <el-divider></el-divider>
            </div>
          </div>           

            </el-col>
            <el-col :span="5"  :offset="2" style="padding-top: 100px;">

              <div class="card">
                <div class="card-image">
                  <figure class="image is-5by3">
                    <img src="https://www.wptest.cn/images/1597825271636.jpg" alt="Placeholder image">
                  </figure>
                </div>
                <div class="card-content">
                  <div class="media">
                      <p>今日微语-</p>
                  </div>
                  <div class="content">
                    没有太晚的开始，不如就从今天行动。总有一天，那个一点一点可见的未来，会在你心里，也在你的脚下慢慢清透。生活，从不亏待每一个努力向上的人。
                    <br> <br>
                    <time datetime="2016-1-1">11:09 PM - 1 Jan 2021</time>
                  </div>
                </div>
              </div>

   
              <div >
                <nav class="panel" style="margin-top: 50px;min-height: 300px;">
                  <p class="panel-heading">
                    博客简介
                  </p>
                  <div class="panel-block" >
                    <span class="panel-icon">
                      <i class="fas fa-book" aria-hidden="true"></i>
                    </span>
                    <div>
                      <p>WP.Nuxt.Blog是基于Nuxt.js框架开发的个人博客，通过SSR服务器端渲染生成静态页面。并使用.NetCore+Vue前后端分离方式开发了一套搭配使用的后台管理系统，实现了相关的基础功能，如：用户角色管理、动态路由、策略授权、实时通信、以及基于AOP的缓存、事务等。</p>
                      <br>
                      <p>Github源码地址：<a style="color: #409eff;" href="https://github.com/wphubs/WP.NetCore.VUE">https://github.com/wphubs/WP.NetCore.VUE</a></p>
              
                    </div>
                    </div>
                </nav>
              </div>

              <div>
                <nav class="panel" style="margin-top: 50px;">
                  <p class="panel-heading">
                    热门文章
                  </p>
                  <a class="panel-block is-active"  :href="'/article/detail/'+item.Id" style="color: #409eff;" v-for="(item,indexArticle) in hotArticleList" :key="indexArticle">
                    <span class="panel-icon">
                      <i class="fas fa-book" aria-hidden="true"></i>
                    </span>
                    {{indexArticle+1}}、{{item.Title}}
                  </a>
                </nav>
              </div>
            </el-col>
          </el-row>
          
    </div>
</template>
  
  <script>
import Rellax from "rellax";

import axios from "axios";
export default {
  data() {
    return {
      value: new Date()
    };
  },
  methods: {
    async clickClass(item){
        var {data} = await axios.get(`${this.baseURL}Article/GetArticleList?classId=${item.Id}&pageIndex=1&pageSize=20`);
        this.articleList=data.Data; 
    },
  },
  async asyncData({ params, $config }) {
    var { data: articleClass } = await axios.get(
      `${$config.baseURL}ArticleClass`
    );
    var { data: article } = await axios.get(
      `${$config.baseURL}Article/GetArticleList?pageIndex=1&pageSize=20`
    );
    var { data: hotArticle } = await axios.get(
      `${$config.baseURL}Article/GetHotArticleList`
    );
    return {baseURL:$config.baseURL, articleList: article.Data, classList: articleClass,hotArticleList:hotArticle.Data };
  },
  head() {
    return {
      link: [
        { rel: 'stylesheet', href:'nekoscroll.css'},
     ],
      script: [
        {
          charset: 'utf-8',
          src:  'nekoscroll.min.js',
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
        top:'70px',
        zoom:0.8, //绳子长度的缩放值
        blog_body:".blog-body",
							});
    } 


    var rellax = new Rellax('.rellax', {
     breakpoints:[576, 768, 1201]
    });
  },
};
</script>
  
  <style scoped>
    
    .article-content{
      display: -webkit-box;
          -webkit-box-orient: vertical;
          -webkit-line-clamp:4;
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
  color:#409eff;
}

</style>