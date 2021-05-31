<!--  -->
<template>
  <div class="app-container">
    <el-card style="width: 100%; text-align: right">
      <span style="margin-left: 20px">
        <!-- <el-button type="primary">查询</el-button> -->
        <el-button type="primary"  v-has="'addMenu'" @click="clickAdd()">新增</el-button>
      </span>
    </el-card>
    <el-table :data="dataList" style="width: 100%; margin-bottom: 20px; margin-top: 10px" row-key="Id" border
      :tree-props="{ children: 'children' }">
      <el-table-column prop="Title" label="名称">
        <template slot-scope="scope">
          <i :class="scope.row.Icon"></i> {{ scope.row.Title }}
        </template>
      </el-table-column>
      <el-table-column prop="Component" label="组件/事件"> </el-table-column>
      <el-table-column prop="Url" label="API地址"> </el-table-column>
      <el-table-column prop="IsButton" label="页面/按钮">
        <template slot-scope="scope">
          <el-tag v-if="scope.row.IsButton" type="warning">按钮</el-tag>
          <el-tag v-else type="success">页面</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="IsHidden" label="是否显示">
        <template slot-scope="scope">
          <el-tag v-if="scope.row.IsHidden && !scope.row.IsButton" type="danger">否</el-tag>
          <el-tag v-if="!scope.row.IsHidden && !scope.row.IsButton" type="success">是</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="CreateTime" label="创建时间"> </el-table-column>
      <el-table-column label="操作">
        <template slot-scope="scope">
          <el-button size="mini" @click="clickEdit(scope.row)"  v-has="'editMenu'" type="warning">编辑</el-button>
          <el-button size="mini" type="danger" @click="clickDelete(scope.row)"  v-has="'deleteMenu'">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-dialog title="提示" :visible.sync="dialogVisible" width="500px">
      <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-width="100px" style="padding-right: 50px">
        <el-form-item label="名称" prop="Title">
          <el-input v-model="ruleForm.Title"></el-input>
        </el-form-item>

        <el-form-item label="上级页面" prop="ParentId">
          <el-select style="width: 100%;" v-model="ruleForm.ParentId" placeholder="请选择">
            <el-option v-for="item in pageMenu" :key="item.Id" :label="item.Title" :value="item.Id">
              <span style="float: left">{{ item.Title }}</span>
              <span style="float: right; color: #8492a6; font-size: 13px">{{ item.Component }}</span>
            </el-option>
          </el-select>
        </el-form-item>


        <el-form-item label="类型" prop="IsButton">
          <el-radio-group v-model="ruleForm.IsButton">
            <el-radio :label="false">页面</el-radio>
            <el-radio :label="true">按钮</el-radio>
          </el-radio-group>
        </el-form-item>

        <el-form-item :label="ruleForm.IsButton?'事件':'组件'" prop="Component">
          <el-input v-model="ruleForm.Component"></el-input>
        </el-form-item>
        <el-form-item label="图标" prop="Icon">
          <el-input v-model="ruleForm.Icon" :disabled="ruleForm.IsButton"></el-input>
        </el-form-item>

        <el-form-item label="是否隐藏" prop="IsHidden">
          <el-switch v-model="ruleForm.IsHidden"></el-switch>
        </el-form-item>
        <el-form-item label="排序" prop="Sort">
          <el-input v-model="ruleForm.Sort"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="submitForm('ruleForm')">保存</el-button>
          <el-button @click="resetForm('ruleForm')">重置</el-button>
        </el-form-item>
      </el-form>
    </el-dialog>
  </div>
</template>

<script>
  import { getMenuTree, addMenu, getList,updateMenu,deleteMenu } from "@/api/menu";
  export default {
    //注入组件
    components: {},
    data() {
      return {
        dataList: [],
        pageMenu: [],
        dialogVisible: false,
        ruleForm: {
          Id: null,
          Title: "",
          Component: "",
          ParentId: "",
          IsHidden: false,
          Icon: "",
          IsButton: false,
          Sort: "",
        },
        rules: {
          Title: [{ required: true, message: "请输入名称", trigger: "blur" }],
          Component: [
            { required: true, message: "请输入组件或事件名称", trigger: "blur" },
          ],
          ParentId: [
            { required: true, message: "请选择上级菜单", trigger: "blur" },
          ],
          IsButton: [
            { required: true, message: "请选择是否为按钮", trigger: "blur" },
          ],
        },
      };
    },
    //监听属性 类似于data概念
    computed: {},
    watch: {},
    methods: {
      clickDelete(row){
        console.log(JSON.stringify(row))
        this.$confirm('确认要删除吗?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          deleteMenu({Id:row.Id}).then((res) => {
            this.$message({
            type: 'success',
            message: '删除成功!'
          });
                this.refreshData();
               
              });
       
        }).catch(() => {
        });
      },
      clickEdit(row) {
        this.ruleForm = JSON.parse(JSON.stringify(row));
        this.dialogVisible = true;
      },
      submitForm(formName) {
        this.$refs[formName].validate((valid) => {
          if (valid) {
            console.log(JSON.stringify(this.ruleForm))
            if (this.ruleForm.Id==null){
  
                console.log('add')
                addMenu(this.ruleForm).then((res) => {
                this.$message({
                  message: '新增成功',
                  type: 'success'
                });
                this.refreshData();
                this.dialogVisible = false;
              });
              }
            else {
              console.log('update')
              updateMenu(this.ruleForm).then((res) => {
                this.$message({
                  message: '修改成功',
                  type: 'success'
                });
                this.refreshData();
                this.dialogVisible = false;
              });
            }
          } else {
            console.log("error submit!!");
            return false;
          }
        });
      },
      resetForm(formName) {
        this.$refs[formName].resetFields();
      },
      clickAdd() {
        this.ruleForm= {
          Id: null,
          Title: "",
          Component: "",
          ParentId: "",
          IsHidden: false,
          Icon: "",
          IsButton: false,
          Sort: "",
        },
        this.dialogVisible = true;
      },
      refreshData() {
        getMenuTree().then((res) => {
          console.log("getList:" + JSON.stringify(res));
          this.dataList = res;
        });
      },
      getPageMenuList() {
        getList().then((res) => {
          console.log("pageMenu:" + JSON.stringify(res));
          this.pageMenu = res;
        });
      },
    },
    //生命周期 - 创建完成（可以访问当前this实例）
    created() {
      this.refreshData();
      this.getPageMenuList();
    },
  };
</script>
<style>
</style>