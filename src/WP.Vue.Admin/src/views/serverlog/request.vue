<!--  -->
<template>
  <div class="app-container">
    <el-card style="width: 100%; text-align: right">
      <span style="margin-left: 20px">
        <el-button type="primary" @click="click()">刷新</el-button>
      </span>
    </el-card>

    <el-table :data="dataList" style="width: 100%; margin-top: 10px">
      <el-table-column prop="Id" label="Id"></el-table-column>
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
  </div>
</template>

<script>
import { getRequestLog } from "@/api/serverlog";
export default {
  //注入组件
  components: {},
  data() {
    return {
      dataList: [],
      currentPage: 1,
      pageSize: 10,
      total: 0,
    };
  },
  //监听属性 类似于data概念
  computed: {},
  watch: {},
  methods: {
    refreshData() {
      getRequestLog().then((res) => {
        console.log(JSON.stringify(res))
   
           this.dataList = res.Data;
        this.total = res.Total;
      });
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
  //生命周期 - 创建完成（可以访问当前this实例）
  created() {
    this.refreshData();
  },
  //生命周期 - 挂载完成（可以访问DOM元素）
  mounted() {},
};
</script>
<style>
</style>