<template>
  <div class="app-container">
    <el-card style="width: 100%; text-align: right">
      <span style="margin-left: 20px">
        <el-button type="primary"  v-has="'addRole'" @click="clickAdd()">新增</el-button>
      </span>
    </el-card>
    <el-table :data="dataList" style="width: 100%; margin-top: 10px">
      <el-table-column prop="JobName" label="任务名"></el-table-column>
      <el-table-column prop="JobGroup" label="任务组"></el-table-column>
      <el-table-column prop="JobType" label="任务类型"></el-table-column>
      <el-table-column prop="BeginTime" label="开始时间"></el-table-column>
      <el-table-column prop="EndTime" label="结束时间"></el-table-column>
      <el-table-column prop="Cron" label="表达式"></el-table-column>
      <el-table-column prop="SimpleTimes" label="循环次数"></el-table-column>
      <el-table-column prop="ExecTimes" label="执行次数"></el-table-column>
      <el-table-column prop="TriggerType" label="触发器类型"></el-table-column>
    <el-table-column prop="Description" label="描述"></el-table-column>
     <el-table-column prop="IsStart" label="是否运行"></el-table-column>
     <el-table-column prop="RequestUrl" label="请求地址"></el-table-column>
    <el-table-column prop="RequestParameters" label="请求参数"></el-table-column>
    <el-table-column prop="RequestType" label="请求类型"></el-table-column>
      <el-table-column prop="Id" label=""></el-table-column>
    </el-table>
   
  </div>
</template>
<script>
import {
  getJobList,
} from "@/api/job";
export default {
  components: {},
  data() {
    return {

      dataList: [],

    };
  },
  computed: {},
  created() {
    this.refreshData();
  },
  methods: {
    refreshData() {
      getJobList().then((res) => {
          console.log(JSON.stringify(res))
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

</style>