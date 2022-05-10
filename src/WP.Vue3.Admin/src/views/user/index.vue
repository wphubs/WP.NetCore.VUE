<template>
  <PageWrapper title="关于">
    <div class="p-4">
      <BasicTable
        :canResize="false"
        title="RefTable示例"
        titleHelpMessage="使用Ref调用表格内方法"
        ref="tableRef"
        :api="api"
        :columns="columns"
        rowKey="id"
        :rowSelection="{ type: 'checkbox' }"
      />
    </div>
  </PageWrapper>
</template>

<script lang="ts">
  import { getUserList } from '/@/api/sys/user';
  import { defineComponent, ref, unref } from 'vue';
  import { BasicTable, TableActionType } from '/@/components/Table';

  //const userList = await getUserList({ pageIndex: 1, pageSize: 10 });
  export default defineComponent({
    components: { BasicTable },
    setup() {
      const tableRef = ref<Nullable<TableActionType>>(null);
      function getTableAction() {
        const tableAction = unref(tableRef);
        if (!tableAction) {
          throw new Error('tableAction is null');
        }
        return tableAction;
      }
      function changeLoading() {
        getTableAction().setLoading(true);
        setTimeout(() => {
          getTableAction().setLoading(false);
        }, 1000);
      }
      return {
        tableRef,
        api: getUserList,
        columns: [{title: 'ID',dataIndex: 'id',fixed: 'left',width: 200}],
        changeLoading,
      };
    },
  });
</script>
<style></style>
