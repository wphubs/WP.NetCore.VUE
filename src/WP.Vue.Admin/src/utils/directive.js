import Vue from 'vue';
Vue.directive('has', {
    inserted: function (el, binding, vnode) {
      let btnPermissions = vnode.context.$route.meta.permission

      if (!permissionJudge(binding.value,btnPermissions)) {
               el.parentNode.removeChild(el);
                // el.disabled=true;
              
    
      }
  
      function permissionJudge(value,listPermissions) {
        for (let item of listPermissions) {
          if (item === value) {
            return true;
          }
        }
        return false;
      }
    }
  });