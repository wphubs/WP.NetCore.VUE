<template>
    <div >
      <div class="infinite-list" v-infinite-scroll="load" style="overflow:auto;height:50vh;text-align: center; width: 100%;">
        <div v-for="i in count" class="infinite-list-item">{{ i }}</div>
      </div>
          
    </div>
  </template>
  
  <script>

    export default {
      data() {
        return {  
          count: 0
        }
      },
      methods: {
        load () {
        this.count += 2
      }
      },
      asyncData () {
        
          
      },
      mounted() {
        
      },
    }
  </script>
  
  <style scoped>

::-webkit-scrollbar {
        /* display: none; */
    }
  </style>