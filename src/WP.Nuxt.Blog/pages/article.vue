<template>
  <div style="width: 100%;background:#ededed; ">
    <el-row :gutter="10">
      <el-col :xs="{span: 22, offset: 1}" :sm="{span: 22, offset: 1}" :md="{span: 10, offset: 7}"
          :lg="{span: 10, offset: 7}" :xl="{span: 10, offset:7}">
          <el-card class="box-card" v-for="o in 10" :key="o">
            <div slot="header" class="clearfix">
              <span>文章标题</span>
            </div>
            <div class="text item">
              内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容
            </div>
          </el-card>
      </el-col>
  </el-row>
  </div>
</template>

<script>
  // import 'codemirror/lib/codemirror.css'
  import marked from 'marked'

  export default {
    data() {
      return {

      }
    },
   
    mounted() {
       this.$nextTick(() => {
                this.$nuxt.$loading.start()
                setTimeout(() => this.$nuxt.$loading.finish(), 700)
            })
    },
  }
</script>

<style scoped>
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
</style>