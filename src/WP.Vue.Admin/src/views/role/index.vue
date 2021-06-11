<template>
  <div class="app-container">
    <el-card style="width: 100%; text-align: right">
      <!-- <span>
        用户名：
        <el-input style="width: 200px" placeholder="请输入内容"></el-input>
      </span> -->
      <span style="margin-left: 20px">
        <!-- <el-button type="primary">查询</el-button> -->
        <el-button type="primary"  v-has="'addRole'" @click="clickAdd()">新增</el-button>
      </span>
    </el-card>
    <el-table :data="dataList" style="width: 100%; margin-top: 10px">
      <el-table-column prop="RoleName" label="角色"></el-table-column>
      <el-table-column prop="CreateTime" label="创建时间"></el-table-column>
      <el-table-column label="操作" width="250">
        <template slot-scope="scope">
          <el-button
            @click="clickSetRole(scope.row)"
            icon="el-icon-setting"
            size="small"
            type="primary" v-has="'getPermission'"
            >权限</el-button
          >
          <el-button @click="clickEdit(scope.row)" v-has="'editRole'" size="small" type="warning"
            >编辑</el-button
          >
          <el-button size="small" type="danger" v-has="'deleteRole'" @click="clickDelete(scope.row)"
            >删除</el-button
          >
        </template>
      </el-table-column>
    </el-table>
   

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
          <el-button type="primary" @click="submitForm('roleForm')"  v-has="'setPermission'"
            >保存</el-button
          >
          <el-button @click="dialogVisible = false">取消</el-button>
        </el-form-item>
      </el-form>
    </el-dialog>

    <el-dialog title="权限设置" :visible.sync="dialogRoleVisible" width="500px" >
      <div style="height: 500px; overflow-y:scroll">
        <el-tree
        :data="dataTree"
        show-checkbox
        node-key="Id"
        ref="tree"
         :default-checked-keys="currentRoleMenu"
        default-expand-all
        check-strictly
        :props="props"
        :expand-on-click-node="false"
      >
        <span class="custom-tree-node" slot-scope="{ node, data }">
          <span>{{ node.label }}</span>
          <span style="padding: 20px">
            <el-tag v-if="data.IsButton" type="warning">按钮</el-tag>
            <el-tag v-else type="success">页面</el-tag>
          </span>
        </span>
      </el-tree>
      </div>
      <span slot="footer" class="dialog-footer" style="padding-right: 30px">
        <el-button @click="dialogVisible = false">取 消</el-button>
        <el-button type="primary" @click="clickSaveRole()">确 定</el-button>
      </span>
    </el-dialog>
  </div>
</template>
<script>
import {
  getRoleList,
  addRole,
  updateRole,
  deleteRole,
  setPermission,
  getRoleMenu,
  getPermission
} from "@/api/role";
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
      props: {
        label: "Title",
        children: "children",
      },
      dataTree: [],
      dataList: [],
      dialogVisible: false,
      dialogRoleVisible: false,
      currentData: {},
      currentRoleMenu: [],
    };
  },
  computed: {},
  created() {
    this.refreshData();
    this.getRoleTreeData();
  },
  methods: {
    clickSaveRole() {
      // console.log(JSON.stringify(this.$refs.tree.getHalfCheckedKeys()))
      var params = {
        RoleId: this.currentData.Id,
        MenuId: [...this.$refs.tree.getCheckedKeys(),...this.$refs.tree.getHalfCheckedKeys()],
      };
      setPermission(params).then((res) => {
        this.$message({
          message: "保存成功",
          type: "success",
        });
        this.dialogRoleVisible = false;
        this.refreshData();
      });
    },
    clickSetRole(row) {
      this.currentData = row;
      getPermission({roleId:row.Id}).then((res) => {
        this.dataTree = res[0];
        this.currentRoleMenu=res[1];
        this.dialogRoleVisible = true;
      });
    },
    clickDelete(row) {
      deleteRole({ Id: row.Id }).then((res) => {
        this.$message({
          message: "删除成功",
          type: "success",
        });
        this.refreshData();
      });
    },
    clickAdd() {
      this.roleForm = { Id: "", RoleName: "" };
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
    getRoleTreeData() {
      // getPermission().then((res) => {
      //   console.log("getPermission:" + JSON.stringify(res));
      //   this.dataTree = res;
      // });
    },
    refreshData() {
      getRoleList().then((res) => {
          this.dataList = res;
        }
      );
    },
  },
  mounted() {},
  beforeCreate() {},
};
</script>
<style>
.custom-tree-node {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: space-between;
  font-size: 15px;
  padding-right: 8px;
}
.el-tree-node__content {
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-align: center;
  -ms-flex-align: center;
  align-items: center;
  height: 35px;
  cursor: pointer;
}
</style>