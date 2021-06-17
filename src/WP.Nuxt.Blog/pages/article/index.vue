<template>
    <div style="width: 100%;background:#ededed;min-height: calc(100vh - 200px);">
      <el-row :gutter="10">
        <el-col :xs="{span: 22, offset: 1}" :sm="{span: 22, offset: 1}" :md="{span: 10, offset: 7}"
            :lg="{span: 10, offset: 7}" :xl="{span: 10, offset:7}">

            <div v-for="(item,indexArticle) in articleList" :key="indexArticle">
              <a :href="'article/detail/'+item.Id">
              <el-card class="box-card">
                <div slot="header" class="clearfix">
                  <span><el-tag  >{{item.ClassName}}</el-tag>
                    
                  <text  class="title">{{item.Title}}</text>
                  <text style="float: right;color:#409EFF;font-size: 15px;">  【阅读全文】</text>
                  </span>
                  
                </div>
                <div class="text item">
                  <div class="article-content" v-html="item.Content"> </div> 
                </div>
              </el-card>
            </a>
            </div>
          
   
        </el-col>
    </el-row>
    </div>
  </template>
  
  <script>
    // import 'codemirror/lib/codemirror.css'
    import marked from 'marked'
    import axios from 'axios'
    
  import highlight from 'highlight.js/lib/highlight'
    export default {
      data() {
        return {
          count: 0
        }
      },
      methods: {
        load () {
        this.count += 2
      }
      },
      async asyncData({ params }) {
        var { id } = params;
        var result = await axios.get(`https://www.wptest.cn/netcore/api/Article/GetArticleList?pageIndex=1&pageSize=20`)
     
        return { articleList: result.data.Data.Data}
      },
      mounted() {
        //  this.$nextTick(() => {
        //           this.$nuxt.$loading.start()
        //           setTimeout(() => this.$nuxt.$loading.finish(), 700)
        //       })
      },
    }
  </script>
  
  <style scoped>
  a {
      color: #409eff;
    }
  
    a:FOCUS {
      color: #409eff;
      ;
  
    }
    .article-content{
      display: -webkit-box;
          -webkit-box-orient: vertical;
          -webkit-line-clamp:4;
          overflow: hidden;
      max-height: 150px;
    }
     .text {
      font-size: 14px;
    }
  
    .item {
      margin-bottom: 18px;
    }
  
    .clearfix:before,
    .clearfix:after {
      display: table;
      content: "";
    }
    .clearfix:after {
      clear: both
    }
  
    .box-card {
      margin-top: 20px;
      width: 100%;
    }
    .detail-body {
      text-align: left;
      border-bottom: 1px solid #f0f0f0;
      color: #666;
      padding: 22px;
      padding-bottom: 10px;
      min-height: 500px;
      padding-top: 40px;
    }
    .title{
      font-size: 18px;
      font-weight: 600;
      margin-left: 10px;
    }
  </style>