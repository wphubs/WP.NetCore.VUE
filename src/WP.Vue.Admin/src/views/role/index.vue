<template>
  <div class="app-container">
    <el-card style="width:100%;text-align: right;">
      <!-- <span>
        用户名：
        <el-input style="width: 200px" placeholder="请输入内容"></el-input>
      </span> -->
      <span style="margin-left: 20px">
        <el-button type="primary">查询</el-button>
        <el-button type="primary" @click="clickAdd()">新增</el-button>
      </span>
    </el-card>
    <el-table :data="dataList" style="width: 100%;margin-top: 10px;">
      <el-table-column prop="Id" label="Id"></el-table-column>
      <!-- <el-table-column prop="Avatar" label="头像"></el-table-column> -->
      <el-table-column prop="RoleName" label="角色"></el-table-column>
      <el-table-column prop="CreateTime" label="创建时间"></el-table-column>
      <el-table-column label="操作" width="150">
        <template slot-scope="scope">
          <el-button @click="clickEdit(scope.row)" size="small" type="warning"
            >编辑</el-button>
          <el-button  size="small" type="danger"  @click="clickDelete(scope.row)">删除</el-button>
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

    <el-dialog title="提示" :visible.sync="dialogVisible" width="500px">
      <el-form
        :model="roleForm"
        :rules="rules"
        ref="roleForm"
        label-width="100px"
      >
        <el-form-item label="角色" prop="RoleName">
          <el-input style="width: 300px" v-model="roleForm.RoleName"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="submitForm('roleForm')"
            >保存</el-button>
          <el-button @click="dialogVisible = false">取消</el-button>
        </el-form-item>
      </el-form>
    </el-dialog>
  </div>
</template>
<script>
import { getPage, addRole, updateRole,deleteRole } from "@/api/role";
import md5 from 'js-md5'
export default {
  components: {},
  data() {
    return {
      roleForm: {
        Id: "",
        RoleName: "",
      },
      rules: {
        RoleName: [
          { required: true, message: "请输入角色名", trigger: "blur" },
        ],
      },
      dataList: [],
      currentPage: 1,
      pageSize: 10,
      total: 0,
      dialogVisible: false,
    };
  },
  computed: {},
  created() {
    this.refreshData();
  },
  methods: {
    clickDelete(row){
          deleteRole({Id:row.Id}).then((res) => {
              this.$message({
                message: "删除成功",
                type: "success",
              });
              this.refreshData();
            });
    },
    clickAdd() {
      this.roleForm = { Id: "",RoleName: ""};
      this.dialogVisible = true;
    },
    submitForm(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          if (this.roleForm.Id == "") {
            addRole(this.roleForm).then((res) => {
              this.$message({
                message: "添加成功",
                type: "success",
              });
              this.dialogVisible = false;
              this.refreshData();
            });
          } else {
            updateRole(this.roleForm).then((res) => {
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
      this.roleForm = JSON.parse(JSON.stringify(row));
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
      getPage({ pageIndex: this.currentPage, pageSize: this.pageSize }).then(
        (res) => {
          console.log("getList:" + JSON.stringify(res));
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