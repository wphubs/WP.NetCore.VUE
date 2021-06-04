<template>
  <div style="width: 100%;">
    <el-row>
      <el-col :xs="{span: 22, offset: 1}" :sm="{span: 22, offset:1}" :md="7" :lg="7" :xl="7">
        <div><strong style="font-weight: bold; color: black;">SSR网站</strong></p>
          <span
            style="display: block;  height: 30px; width: 100%; background-size: 40px;  background-color: #1E1E1E; margin-bottom: -7px; border-radius: 5px; background-position: 10px 10px;"></span><code
            class="hljs"
            style="overflow-x: auto; padding: 16px; color: #DCDCDC; display: -webkit-box; font-family: Operator Mono, Consolas, Monaco, Menlo, monospace; font-size: 12px; -webkit-overflow-scrolling: touch; padding-top: 15px; background: #1E1E1E; border-radius: 5px;">1.克隆代码<br>&nbsp;&nbsp;https://github.com/wphubs/WP.NetCore.VUE.git<br>&nbsp;&nbsp;<br>2.进到目录<br>&nbsp;&nbsp;<span class="hljs-built_in" style="color: #4EC9B0; line-height: 26px;">cd</span>&nbsp;WP.Vue.Blog<br>&nbsp;&nbsp;<br>3.安装依赖<br>&nbsp;&nbsp;yarn&nbsp;install<br>&nbsp;&nbsp;<br>5.开发模式运行<br>&nbsp;&nbsp;yarn&nbsp;run&nbsp;dev<br></code>
        </div>
      </el-col>

    </el-row>
  </div>
</template>

<script>
  // import 'codemirror/lib/codemirror.css'
  import marked from 'marked'

  export default {
    async asyncData() {
      marked.setOptions({
        renderer: new marked.Renderer(),
        gfm: true,
        tables: true,
        breaks: false,
        pedantic: false,
        sanitize: false,
        smartLists: true,
        smartypants: false,
        highlight: function (code, lang) {
          return highlight.highlightAuto(code).value
        }
      })

      const markHtml = marked('')
      return { markHtml }
    },
    data() {
      return {
        pc: { span: 6, offset: 1 },
        text: '11111111',
        markdownOption: {
          bold: true, // 粗体
          italic: true, // 斜体
          header: true, // 标题
          underline: true, // 下划线
          strikethrough: true, // 中划线
          mark: true, // 标记
          superscript: true, // 上角标
          subscript: true, // 下角标
          quote: true, // 引用
          ol: true, // 有序列表
          ul: true, // 无序列表
          link: true, // 链接
          imagelink: true, // 图片链接
          code: true, // code
          table: true, // 表格
          fullscreen: false, // 全屏编辑
          readmodel: false, // 沉浸式阅读
          htmlcode: true, // 展示html源码
          help: true, // 帮助
          undo: true, // 上一步
          redo: true, // 下一步
          trash: true, // 清空
          save: true, // 保存（触发events中的save事件）
          navigation: true, // 导航目录
          alignleft: true, // 左对齐
          aligncenter: true, // 居中
          alignright: true, // 右对齐
          subfield: true, // 单双栏模式
          preview: true // 预览
        },
      }
    },
  }
</script>

<style>
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