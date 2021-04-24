<!--  -->
<template>
  <div class="app-container">
    <el-card style="width: 100%; text-align: right">
      <span style="margin-left: 20px">
        <el-button type="primary">查询</el-button>
        <el-button type="primary" @click="clickAdd()">新增</el-button>
      </span>
    </el-card>
    <el-table
      :data="dataList"
      style="width: 100%; margin-bottom: 20px; margin-top: 10px"
      row-key="Id"
      border
      :tree-props="{ children: 'children' }"
    >
      <el-table-column prop="Title" label="名称">
        <template slot-scope="scope">
          <i :class="scope.row.Icon"></i> {{ scope.row.Title }}
        </template>
      </el-table-column>
      <el-table-column prop="Component" label="组件/事件"> </el-table-column>
      <el-table-column label="API地址"> </el-table-column>
      <el-table-column prop="IsButton" label="页面/按钮">
        <template slot-scope="scope">
          <el-tag v-if="scope.row.IsButton" type="warning">按钮</el-tag>
          <el-tag v-else type="success">页面</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="IsHidden" label="是否显示">
        <template slot-scope="scope">
          <el-tag v-if="scope.row.IsHidden && !scope.row.IsButton" type="danger"
            >否</el-tag
          >
          <el-tag
            v-if="!scope.row.IsHidden && !scope.row.IsButton"
            type="success"
            >是</el-tag
          >
        </template>
      </el-table-column>
      <el-table-column prop="CreateTime" label="创建时间"> </el-table-column>
      <el-table-column label="操作">
        <template slot-scope="scope">
          <el-button size="mini" @click="handleEdit(scope.$index, scope.row)"
            >编辑</el-button
          >
          <el-button
            size="mini"
            type="danger"
            @click="handleDelete(scope.$index, scope.row)"
            >删除</el-button
          >
        </template>
      </el-table-column>
    </el-table>
    <el-dialog title="提示" :visible.sync="dialogVisible" width="500px">
      <el-form
        :model="ruleForm"
        :rules="rules"
        ref="ruleForm"
        label-width="100px"
        style="padding-right: 50px"
      >
        <el-form-item label="名称" prop="Title">
          <el-input v-model="ruleForm.Title"></el-input>
        </el-form-item>

        <el-form-item label="类型" prop="IsButton">
          <el-radio-group v-model="ruleForm.IsButton">
            <el-radio :label="false">页面</el-radio>
            <el-radio :label="true">按钮</el-radio>
          </el-radio-group>
        </el-form-item>

        <el-form-item label="组件" prop="Component">
          <el-input v-model="ruleForm.Component"></el-input>
        </el-form-item>
        <el-form-item label="图标" prop="Icon">
          <el-input v-model="ruleForm.Icon"></el-input>
        </el-form-item>

        <el-form-item label="是否隐藏" prop="IsHidden">
          <el-switch v-model="ruleForm.IsHidden"></el-switch>
        </el-form-item>
        <el-form-item label="排序" prop="Sort">
          <el-input v-model="ruleForm.Sort"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="submitForm('ruleForm')"
            >立即创建</el-button
          >
          <el-button @click="resetForm('ruleForm')">重置</el-button>
        </el-form-item>
      </el-form>
    </el-dialog>
  </div>
</template>

<script>
import { getAll, addMenu } from "@/api/menu";
export default {
  //注入组件
  components: {},
  data() {
    return {
      dataList: [],
      dialogVisible: false,
      ruleForm: {
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
        Icon: [{ required: true, message: "请输入菜单图标", trigger: "blur" }],
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
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          console.log(JSON.stringify(this.ruleForm));
          addMenu(this.ruleForm).then((res) => {
            console.log("addMenu:" + JSON.stringify(res));
            
          });
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
      this.dialogVisible = true;
    },
    refreshData() {
      getAll().then((res) => {
        console.log("getList:" + JSON.stringify(res));
        this.dataList = res;
      });
    },
  },
  //生命周期 - 创建完成（可以访问当前this实例）
  created() {
    this.refreshData();
  },
};
</script>
<style>
</style>