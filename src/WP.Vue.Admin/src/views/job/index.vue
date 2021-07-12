<template>
  <div class="app-container">
    <el-card style="width: 100%; text-align: right">
      <span style="margin-left: 20px">
        <el-button type="primary" size="medium"  v-has="'addJob'" @click="clickAddJob()">新增任务</el-button>
        <el-button type="success" size="medium"  v-has="'resumeJob'" @click="clickResumeJob()">开始/恢复</el-button>
        <el-button type="warning" size="medium"  v-has="'pauseJob'" @click="clickPauseJob()">停止任务</el-button>
        <el-button type="danger" size="medium"  v-has="'deleteJob'" @click="clickDeleteJob()">删除任务</el-button>
        <el-button type="info" size="medium"  v-has="'editJob'" @click="clickUpdateJob()">修改任务</el-button>
        <el-button type="primary" size="medium" @click="refreshData()">刷新</el-button>
      </span>
    </el-card>
    <el-table :data="dataList"  @current-change="handleCurrentChange" highlight-current-row style="width: 100%; margin-top: 10px">
      <!-- <el-table-column prop="Id" label="Id"></el-table-column> -->
      <el-table-column prop="JobName" label="任务名"></el-table-column>
      <el-table-column prop="JobGroup" label="任务组"></el-table-column>
      <el-table-column prop="JobType" label="任务类型"  width="100">
        <template slot-scope="scope">
          <el-tag>
            <template v-if="scope.row.JobType==1">Http</template>
            <template v-else-if="scope.row.JobType==2">Assembly</template>
            <template v-else>RabbitMQ</template>
          </el-tag>
        </template> 
      </el-table-column>
      <el-table-column prop="RequestType" label="任务详情">
        <template slot-scope="scope">
            <template v-if="scope.row.RequestType==1">Get</template>
            <template v-else-if="scope.row.RequestType==2">Post</template>
            <template v-else-if="scope.row.RequestType==3">Put</template>
            <template v-else>Delete</template>
             &nbsp;&nbsp;{{scope.row.RequestUrl}}
        </template>
      </el-table-column>
      <el-table-column prop="BeginTime" label="开始时间"></el-table-column>
      <el-table-column prop="EndTime" label="结束时间"></el-table-column>
      <el-table-column prop="TriggerType" label="触发器类型" width="100">
        <template slot-scope="scope">
        <el-tag  >
          <template v-if="scope.row.TriggerType==1">Cron</template>
          <template v-else>Simple</template>
        </el-tag>
      </template>
      </el-table-column>
      <el-table-column prop="Cron" label="表达式/间隔">
        <template slot-scope="scope">
            <template v-if="scope.row.TriggerType==1">{{scope.row.Cron}}</template>
            <template v-else>{{scope.row.IntervalSecond}}(秒)</template>
        </template>
      </el-table-column>
      <el-table-column prop="ExecTimes" label="执行次数" width="100"></el-table-column>
      <el-table-column prop="Description" label="描述"></el-table-column>
      <el-table-column prop="IsStart" label="是否运行">
        <template slot-scope="scope">
            <template v-if="scope.row.IsStart">
              <el-tag type="success">是</el-tag>
            </template>
            <template v-else>
              <el-tag type="danger">否</el-tag>
            </template>
        </template>
      </el-table-column>
      <!-- <el-table-column prop="RequestParameters" label="请求参数"></el-table-column> -->
    </el-table>


    <el-dialog top="50px" title="提示" :visible.sync="dialogVisible" width="800px">
      <el-form :inline="true" :model="jobForm" :rules="rules" ref="jobForm" label-width="100px">
        <el-form-item class="form-inline" label="任务名称" prop="JobName">
          <el-input class="form-input" v-model="jobForm.JobName"></el-input>
        </el-form-item>
        <el-form-item label="任务分组" prop="JobGroup">
          <el-input class="form-input" v-model="jobForm.JobGroup"></el-input>
        </el-form-item>
        <el-form-item class="form-inline" label="触发器类型" prop="TriggerType">
          <el-radio-group @change="formRadioChange" v-model="jobForm.TriggerType" size="medium">
            <el-radio :label="1">Cron</el-radio>
            <el-radio :label="2" >Simple</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="表达式" v-if="jobForm.TriggerType==1" prop="Cron" :rules="{
          required: true, message: '请填写表达式', trigger: 'blur'}">
          <el-input class="form-input" v-model="jobForm.Cron"></el-input>
        </el-form-item>
        <el-form-item label="间隔(秒)" v-if="jobForm.TriggerType==2" prop="IntervalSecond" :rules="{
          required: true, message: '请填写循环间隔', trigger: 'blur'}">
          <el-input class="form-input" v-model="jobForm.IntervalSecond"></el-input>
        </el-form-item>
        <el-form-item class="form-inline" label="开始时间" prop="BeginTime">
          <el-date-picker class="form-input" type="datetime" placeholder="开始时间" v-model="jobForm.BeginTime">
          </el-date-picker>
        </el-form-item>
        <el-form-item class="form-inline" label="结束时间" prop="EndTime">
          <el-date-picker class="form-input" type="datetime" placeholder="结束时间" v-model="jobForm.EndTime">
          </el-date-picker>
        </el-form-item>
        <el-form-item prop="JobType" label="任务类型">
          <el-radio-group style="width:100%;" v-model="jobForm.JobType" size="medium">
            <el-radio :label="1">Http</el-radio>
            <el-radio :label="2" disabled>Assembly</el-radio>
            <el-radio :label="3" disabled>RabbitMQ</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="请求类型" prop="RequestType">
          <el-radio-group style="width:400px;" v-model="jobForm.RequestType">
            <el-radio :label="1">Get</el-radio>
            <el-radio :label="2">Post</el-radio>
            <el-radio :label="3">Put</el-radio>
            <el-radio :label="4">Delete</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="请求地址" prop="RequestUrl">
          <el-input style="width:610px;" v-model="jobForm.RequestUrl"></el-input>
        </el-form-item>
        <el-form-item label="请求头" prop="Headers">
          <el-input class="form-input" v-model="jobForm.Headers"></el-input>
        </el-form-item>
        <el-form-item label="请求参数" prop="RequestParameters">
          <el-input class="form-input" v-model="jobForm.RequestParameters"></el-input>
        </el-form-item>
        <el-form-item label="备注" prop="desc">
          <el-input type="textarea" style="width:610px;" v-model="jobForm.Description"></el-input>
        </el-form-item>
        <el-form-item style="text-align: right;width: 100%;padding-right: 50px;">
          <el-button type="primary" @click="submitForm('jobForm')">保存</el-button>
          <el-button @click="dialogVisible = false">取消</el-button>
        </el-form-item>
      </el-form>
    </el-dialog>

  </div>
</template>
<script>
  import {
    getJobList,
    addJob,
    resumeJob,
    pauseJob,
    updateJob,
    deleteJob
  } from "@/api/job";
  export default {
    components: {},
    data() {
      return {
        jobForm: {
          Id: "",
          JobName: "",
          JobGroup: "",
          JobType: 1,
          BeginTime: "",
          EndTime: "",
          Cron: "",
          SimpleTimes: null,
          ExecTimes: null,
          IntervalSecond: 0,
          TriggerType: 1,
          Description: "",
          IsStart: true,
          RequestUrl: "",
          RequestParameters: "",
          Headers: "",
          RequestType: 1,
        },
        dialogVisible: false,
        rules: {
          JobName: [
            { required: true, message: "请输入任务名称", trigger: "blur" },
          ],
          JobGroup: [
            { required: true, message: "请输入任务分组", trigger: "blur" },
          ],
          JobType: [
            { required: true, message: "请选择任务类型", trigger: "blur" },
          ],
          TriggerType: [
            { required: true, message: "请选择触发器类型", trigger: "blur" },
          ],
          // Cron: [
          //   { required: true, message: "请填写表达式", trigger: "blur" },
          // ],
          // SimpleTimes: [
          //   { required: true, message: "请填写间隔", trigger: "blur" },
          // ],
          BeginTime: [
            { required: true, message: "请选择开始时间", trigger: "blur" },
          ],
          EndTime: [
            { required: true, message: "请选择结束时间", trigger: "blur" },
          ],
          RequestType: [
            { required: true, message: "请选择请求类型", trigger: "blur" },
          ],
          RequestUrl: [
            { required: true, message: "请输入请求地址", trigger: "blur" },
          ],
        },
        dataList: [],
        currentRow :{},

      };
    },
    computed: {},
    created() {
      this.refreshData();
    },
    methods: {
      formRadioChange(e){
        console.log(JSON.stringify(e))
      },
      clickUpdateJob(){
        this.jobForm=JSON.parse(JSON.stringify(this.currentRow))
        this.dialogVisible = true;
      },
      clickDeleteJob(){
        this.$confirm('确定要删除任务吗?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          deleteJob({jobId:this.currentRow.Id}).then((res) => {
                this.$message({
                  message: "删除成功",
                  type: "success",
                });
                this.refreshData();
              });

        }).catch(() => {
        
        });
      },
      clickResumeJob(){
        resumeJob({jobId:this.currentRow.Id}).then((res) => {
                this.$message({
                  message: "恢复成功",
                  type: "success",
                });
                this.refreshData();
              });
      },
      clickPauseJob(){
        this.$confirm('确定要停止任务吗?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          pauseJob({jobId:this.currentRow.Id}).then((res) => {
                this.$message({
                  message: "停止成功",
                  type: "success",
                });
                this.refreshData();
              });

        }).catch(() => {
        
        });
      },
      submitForm(formName) {
        this.$refs[formName].validate((valid) => {
          if (valid) {
            if (this.jobForm.Id == "") {
              addJob(this.jobForm).then((res) => {
                this.$message({
                  message: "添加成功",
                  type: "success",
                });
                this.dialogVisible = false;
                this.refreshData();
              });

            } else {
              updateJob(this.jobForm).then((res) => {
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
      clickAddJob() {
        this.jobForm={
          Id: "",
          JobName: "",
          JobGroup: "",
          JobType: 1,
          BeginTime: "",
          EndTime: "",
          Cron: "",
          SimpleTimes: 0,
          ExecTimes: 0,
          IntervalSecond: 0,
          TriggerType: 1,
          Description: "",
          IsStart: true,
          RequestUrl: "",
          RequestParameters: "",
          Headers: "",
          RequestType: 1,
        },
        this.dialogVisible = true;
      },
      handleCurrentChange(val) {
        this.currentRow = val;
      },
      refreshData() {
        getJobList().then((res) => {
          console.log(JSON.stringify(res))
          this.dataList = res;
        }
        );
      },
    },
    mounted() { },
    beforeCreate() { },
  };
</script>
<style scoped>
  .form-inline {
    width: 350px;
  }

  .form-input {
    width: 250px;
  }
</style>