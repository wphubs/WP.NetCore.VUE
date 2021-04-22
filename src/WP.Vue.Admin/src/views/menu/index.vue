<!--  -->
<template>
  <div class="app-container">
    <el-card style="width: 100%; text-align: right">
      <!-- <span>
        用户名：
        <el-input style="width: 200px" placeholder="请输入内容"></el-input>
      </span> -->
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
      default-expand-all
      :tree-props="{ children: 'children' }"
    >
      <el-table-column prop="Title" label="名称"> </el-table-column>
      <el-table-column prop="Name" label="路由/"> </el-table-column>
      <el-table-column prop="Component" label="组件地址"> </el-table-column>
      <el-table-column prop="IsButton" label="页面/按钮"> </el-table-column>
      <el-table-column prop="IsHidden" label="是否显示"> </el-table-column>
      <el-table-column prop="CreateTime" label="创建时间"> </el-table-column>
    </el-table>
  </div>
</template>

<script>
import { getAll } from "@/api/menu";
export default {
  //注入组件
  components: {},
  data() {
    return {
      dataList: [],
    };
  },
  //监听属性 类似于data概念
  computed: {},
  watch: {},
  methods: {
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