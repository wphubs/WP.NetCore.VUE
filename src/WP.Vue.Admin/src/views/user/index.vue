<template>
  <div class="app-container">
    <el-card style="width: 100%; text-align: right">
      <span style="margin-left: 20px">
        <el-button type="primary">查询</el-button>
        <el-button type="primary" @click="clickAddUser()">新增</el-button>
      </span>
    </el-card>
    <el-table :data="dataList" style="width: 100%; margin-top: 10px">
      <!-- <el-table-column prop="Id" label="Id"></el-table-column> -->
      <!-- <el-table-column prop="Avatar" label="头像"></el-table-column> -->
      <el-table-column prop="UserName" label="用户名"></el-table-column>
      <el-table-column prop="Name" label="名称"></el-table-column>
      <el-table-column prop="RolesName" label="角色">
        <template slot-scope="scope">
          <el-tag v-for="item in scope.row.RolesName" :key="item" style="margin-bottom: 5px;">{{item}}</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="IsEnable" label="启用">
        <template slot-scope="scope">
          <el-tag v-if="scope.row.IsEnable" type="success">启用</el-tag>
          <el-tag v-else type="danger">禁用</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="SexText" label="性别"> </el-table-column>
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
    <el-dialog title="用户" :visible.sync="dialogVisible" width="550px">
      <el-form
        :model="userForm"
        :rules="rules"
        ref="userForm"
        label-width="100px"
      >
        <el-form-item label="用户名" prop="UserName">
          <el-input style="width: 300px" v-model="userForm.UserName"></el-input>
        </el-form-item>
        <el-form-item label="姓名" prop="Name">
          <el-input style="width: 300px" v-model="userForm.Name"></el-input>
        </el-form-item>
        <el-form-item label="密码" prop="Password">
          <el-input
            style="width: 300px"
            v-model="userForm.Password"
            show-password
          ></el-input>
        </el-form-item>

        <el-form-item label="角色" prop="Roles">
          <el-select style="width: 300px" v-model="userForm.Roles" multiple placeholder="请选择角色">
          <el-option
            v-for="item in roleList"
            :key="item.Id"
            :label="item.RoleName"
            :value="item.Id">
          </el-option>
        </el-select>
      </el-form-item>
        <el-form-item label="性别" prop="sex">
          <el-radio-group v-model="userForm.Sex">
            <el-radio :label="1">男</el-radio>
            <el-radio :label="2">女</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="是否启用">
          <el-switch v-model="userForm.IsEnable"></el-switch>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="submitForm('userForm')" style="width:120px">保存</el-button
          >
          <el-button @click="dialogVisible = false" style="width:120px">取消</el-button>
        </el-form-item>
      </el-form>
    </el-dialog>
  </div>
</template>
<script>
import { getList, addUser, updateUser, deleteUser } from "@/api/user";
import { getAll } from "@/api/role";
import md5 from "js-md5";
export default {
  components: {},
  data() {
    return {
      userForm: {
        Id: "",
        UserName: "",
        Name: "",
        Password: "",
        Sex: 1,
        IsEnable: true,
        Roles:[],
      },
      currentPwd: "",
      rules: {
        UserName: [
          { required: true, message: "请输入用户名", trigger: "blur" },
        ],
        Name: [{ required: true, message: "请输入姓名", trigger: "blur" }],
        Roles: [{ required: true, message: "请选择角色", trigger: "blur" }],
        Password: [{ required: true, message: "请输入密码", trigger: "blur" }],
        Sex: [{ required: true, message: "请选择性别", trigger: "blur" }],
      },
      dataList: [],
      currentPage: 1,
      pageSize: 10,
      total: 0,
      dialogVisible: false,
      roleList: [],
    };
  },
  computed: {},
  created() {
    this.refreshData();
    this.getRoleList();
    console.log(JSON.stringify(this.$route.meta))
  },
  methods: {
    getRoleList() {
      getAll().then((res) => {
        this.roleList = res;
        console.log('role:'+JSON.stringify(this.roleList));
      });
    },
    clickDelete(row) {
      deleteUser({ Id: row.Id }).then((res) => {
        this.$message({
          message: "删除成功",
          type: "success",
        });
        this.refreshData();
      });
    },
    clickAddUser() {
      this.currentPwd = "";
      this.userForm = {
        Id: "",
        UserName: "",
        Name: "",
        Password: "",
        Sex: 1,
        IsEnable: true,
      };
      this.dialogVisible = true;
    },
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          if (this.userForm.Id == "") {
            this.userForm.Password = md5(this.userForm.Password);
            addUser(this.userForm).then((res) => {
              this.$message({
                message: "添加成功",
                type: "success",
              });
              this.dialogVisible = false;
              this.refreshData();
            });
          } else {
            if (this.currentPwd != this.userForm.Password) {
              this.userForm.Password = md5(this.userForm.Password);
            }
            updateUser(this.userForm).then((res) => {
              this.$message({
                message: "修改成功",
                type: "success",
              });
              this.dialogVisible = false;
              this.refreshData();
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
    clickEdit(row) {
      this.userForm = JSON.parse(JSON.stringify(row));
      this.currentPwd = this.userForm.Password;
      this.dialogVisible = true;
    },
    handleSizeChange(val) {
      this.pageSize = val;
      this.refreshData();
    },
    handleCurrentChange(val) {
      this.currentPage = val;
      this.refreshData();
    },
    refreshData() {
      getList({ pageIndex: this.currentPage, pageSize: this.pageSize }).then(
        (res) => {
          this.dataList = [];
          this.dataList = res.Data;
          this.total = res.Total;
        }
      );
    },
  },
  mounted() {},
  beforeCreate() {},
};
</script>
<style>
</style>