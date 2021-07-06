<template>
    <div class="app-container">
      <el-card style="width: 100%; text-align: right">
        <span style="margin-left: 20px">
          <el-button type="primary" @click="refreshData()">刷新</el-button>
        </span>
      </el-card>
  
      <el-table :data="dataList" style="width: 100%; margin-top: 10px">
        <el-table-column prop="ConnectionId" label="任务ID" width="200">
          <template slot-scope="scope">
            {{ scope.row.Properties.JobId }}
          </template>          
        </el-table-column>
        <el-table-column prop="ConnectionId" label="任务分组" width="200">
            <template slot-scope="scope">
              {{ scope.row.Properties.JobGroup }}
            </template>          
          </el-table-column>
          <el-table-column prop="ConnectionId" label="任务名称" width="200">
            <template slot-scope="scope">
              {{ scope.row.Properties.JobName }}
            </template>          
          </el-table-column>
          <el-table-column prop="ConnectionId" label="状态" width="200">
      

              <template slot-scope="scope">
                <el-tag v-if="scope.row.Properties.Result" type="success">成功</el-tag>
            <el-tag  v-else type="danger">失败</el-tag>
             </template>      
              
          </el-table-column>
          <el-table-column prop="ConnectionId" label="执行结果" width="400">
            <template slot-scope="scope">
              {{ scope.row.Properties.Message }}
            </template>          
          </el-table-column>

        <el-table-column prop="Elapsed" label="任务耗时" width="100">
          <template slot-scope="scope">
            {{ scope.row.Properties.Time }}ms
          </template>
        </el-table-column>

        <el-table-column prop="Timestamp" label="时间"></el-table-column>

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
  import { getJobLog } from "@/api/serverlog";
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
        getJobLog({
          pageIndex: this.currentPage,
          pageSize: this.pageSize,
        }).then((res) => {
          this.dataList = res.Data;
          console.log(222+JSON.stringify(this.dataList));
          this.total = res.Total;
        })
        .catch(err=>{
          console.log(222+JSON.stringify(err));
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