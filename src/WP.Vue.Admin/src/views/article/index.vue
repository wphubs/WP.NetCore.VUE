<template>
   <div>
<el-card style="width: 100%; text-align: right">
      <span style="margin-left: 20px">
        <el-button type="primary" @click="clickAddUser()" >新增</el-button>
      </span>
    </el-card>

    <el-table :data="dataList" style="width: 100%; margin-top: 10px">
      <el-table-column prop="Title" label="Title"></el-table-column>
      <el-table-column prop="ClassName" label="名称"></el-table-column>
      <el-table-column prop="CreateTime" label="创建时间"></el-table-column>
      <el-table-column label="操作" width="150">
        <template slot-scope="scope">
          <el-button @click="clickEdit(scope.row)"  size="small" type="warning"
            >编辑</el-button
          >
          <el-button size="small" type="danger"   @click="clickDelete(scope.row)"
            >删除</el-button
          >
        </template>
      </el-table-column>
    </el-table>
    <div class="el-page">
      <el-pagination
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
        :current-page="currentPage"
        :page-sizes="[10, 20, 30, 40]"
        :page-size="pageSize"
        layout="total, sizes, prev, pager, next, jumper"
        :total="total"
      >
      </el-pagination>
    </div>

  <!-- <editor
    :initialValue="editorText"
    :options="editorOptions"
    height="500px"
    initialEditType="wysiwyg"
    previewStyle="vertical"
  /> -->

       </div>  

</template>
<script>
import { getArticleList } from "@/api/article";
import "codemirror/lib/codemirror.css";
import "@toast-ui/editor/dist/toastui-editor.css";

import { Editor } from "@toast-ui/vue-editor";

export default {
  components: {
    editor: Editor,
  },
  created(){
    this.refreshData();  
  },
  methods: {
    refreshData() {
      getArticleList({
        pageIndex: this.currentPage,
        pageSize: this.pageSize,
      }).then((res) => {
   this.dataList = [];
          this.dataList = res.Data;
          this.total = res.Total;

      });
    },
    clickEdit(row){

    },
    clickDelete(row){

    },
        handleSizeChange(val) {
      this.pageSize = val;
      this.refreshData();
    },
    handleCurrentChange(val) {
      this.currentPage = val;
      this.refreshData();
    },
  },
  data() {
    return {
      dataList: [],
      currentPage: 1,
      pageSize: 10,
      total: 0,
      dialogVisible: false,
      // editorText: 'This is initialValue.',
      editorOptions: {
        language: "zh_CN",
        hideModeSwitch: true,
        previewStyle: "vertical",
        useCommandShortcut: true,
        useDefaultHTMLSanitizer: true,
        usageStatistics: false,
        hideModeSwitch: false,
        toolbarItems: [
          "heading",
          "bold",
          "italic",
          "strike",
          "divider",
          "hr",
          "quote",
          "divider",
          "ul",
          "ol",
          "task",
          "indent",
          "outdent",
          "divider",
          "table",
          "image",
          "link",
          "divider",
          "code",
          "codeblock",
        ],
      },
    };
  },
};
</script>