<template>
    <div style="width: 100%;background:#ededed;min-height: calc(100vh - 200px); ">
      <el-row :gutter="10">
        <el-col :xs="{span: 22, offset: 1}" :sm="{span: 22, offset: 1}" :md="{span: 10, offset: 7}"
            :lg="{span: 10, offset: 7}" :xl="{span: 10, offset:7}">
           
            <div v-for="(item,indexArticle) in articleList" :key="indexArticle">
               <!-- <el-link :underline="false" :href="'detail/'+item.Id" type="primary"  > 
              </el-link> -->
              <el-card class="box-card">
                <div slot="header" class="clearfix">
                  <span><el-tag type="success">{{item.ClassName}}</el-tag><text  class="title">{{item.Title}}</text> </span>
                  <a style="float: right; font-size: 15px;" :href="'article/detail/'+item.Id+'?item='+item">【阅读全文】</a>
                </div>
                <div class="text item">
                  <div class="article-content" v-html="item.ContentHtml"> </div> 
                </div>
              </el-card>
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
  
        }
      },
      async asyncData({ params }) {
        var { id } = params;
        var result = await axios.get(`http://localhost:8081/api/Article?pageIndex=1&pageSize=10`)
        marked.setOptions({
          renderer: new marked.Renderer(),
          gfm: true,
          tables: true,
          breaks: false,
          pedantic: false,
          sanitize: false,
          smartLists: true,
          smartypants: false,
          highlight: function(code, lang) {
            return highlight.highlightAuto(code).value
          }
        })
        var article=result.data.Data.Data;
        article.forEach(item=>{
          const markHtml = marked(item.Content)
          item.ContentHtml=markHtml;
        })  
        return { articleList: article}
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
          -webkit-line-clamp: 3;
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