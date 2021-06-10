<template>
  <div class="app-container">
    <el-card style="width: 100%; text-align: right">
      <span style="margin-left: 20px">
        <el-button type="primary" @click="clickAddAricle()">新增</el-button>
      </span>
    </el-card>

    <el-table :data="dataList" style="width: 100%; margin-top: 10px">
      <el-table-column prop="Title" label="标题"></el-table-column>
      <el-table-column prop="ClassName" label="分类"></el-table-column>
      <el-table-column prop="CreateTime" label="创建时间"></el-table-column>
      <el-table-column label="操作" width="150">
        <template slot-scope="scope">
          <el-button @click="clickEdit(scope.row)" size="small" type="warning"
            >编辑</el-button
          >
          <el-button size="small" type="danger" @click="clickDelete(scope.row)"
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

    <el-dialog title="提示" :visible.sync="dialogVisible" width="80%" top="20px">
      <el-form
        :model="articleForm"
        :rules="rules"
        ref="articleForm"
        label-width="100px"
      >
        <el-form-item label="标题" prop="Title">
          <el-input style="width: 300px" v-model="articleForm.Title"></el-input>
        </el-form-item>
        <el-form-item label="分类" prop="ClassId">
          <el-select   style="width: 300px" v-model="articleForm.ClassId" placeholder="请选择">
            <el-option
              v-for="item in articleClass"
              :key="item.Id"
              :label="item.ClassName"
              :value="item.Id">
            </el-option>
          </el-select>


        </el-form-item>


           <el-form-item label="内容" prop="Content">
             <editor ref="toastuiEditor" style="z-index:99999" 
                 @blur="onEditorBlur"
                :initialValue="articleForm.Content"
                :options="editorOptions"
                height="450px"
                initialEditType="wysiwyg"
                previewStyle="vertical"
              />
        </el-form-item>
         

        <el-form-item>
          <el-button
            type="primary"
            @click="submitForm('articleForm')"
            >保存</el-button
          >
          <el-button @click="dialogVisible = false">取消</el-button>
        </el-form-item>
      </el-form>
    </el-dialog>

      
         
        
  </div>
</template>
<script>
import { getArticleList,getArticleClass,addArticle } from "@/api/article";
import "codemirror/lib/codemirror.css";
import "@toast-ui/editor/dist/toastui-editor.css";
// import '@toast-ui/editor/dist/i18n/zh-cn';
import '@toast-ui/editor/dist/i18n/zh-cn';

import { Editor } from "@toast-ui/vue-editor";

export default {
  components: {
    editor: Editor,
  },
 mounted() {
   
  },
  created() {
    this.refreshData();
  },
  methods: {
 submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          console.log(JSON.stringify(this.articleForm))
          if (this.articleForm.Id == "") {
            addArticle(this.articleForm).then((res) => {
              this.$message({
                message: "添加成功",
                type: "success",
              });
              this.dialogVisible = false;
              this.refreshData();
            });
          } else {
            // updateRole(this.articleForm).then((res) => {
            //   this.$message({
            //     message: "修改成功",
            //     type: "success",
            //   });
            //   this.dialogVisible = false;
            //   this.refreshData();
            // });
          }
        } else {
          console.log("error submit!!");
          return false;
        }
      });
    },
      onEditorBlur() {
         let markdown = this.$refs.toastuiEditor.invoke('getMarkdown');
        this.articleForm.Content=markdown;
      },
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
    clickAddAricle(){
       getArticleClass().then((res) => {
         console.log(JSON.stringify(res))
         this.articleClass=res;
        this.dialogVisible=true;
      });
        
    },
    clickEdit(row) {},
    clickDelete(row) {},
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
      articleClass:[],
      dialogVisible:false,
      articleForm: {
        Id: "",
        Title: "",
        Content: "",
        ClassId: "",
      },
      rules: {
        Title: [
          { required: true, message: "请输入文章标题", trigger: "blur" },
        ],
         Content: [
          { required: true, message: "请输入文章内容", trigger: "blur" },
        ],
         ClassId: [
          { required: true, message: "请选择分类", trigger: "blur" },
        ],
      },
      dataList: [],
      currentPage: 1,
      pageSize: 10,
      total: 0,
      dialogVisible: false,
      // editorText: 'This is initialValue.',
      editorOptions: {
        language: "zh-CN",
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

<style >
.tui-tooltip {
    position: absolute;
    background-color: #222;
    z-index: 99999999;
    opacity: 0.8;
    color: #fff;
    padding: 2px 5px;
    font-size: 10px;
}
.tui-editor-defaultUI .te-mode-switch-section {
    background-color: #f9f9f9;
    border-top: 1px solid #e5e5e5;
    height: 45px;
    font-size: 10px;
}
</style>