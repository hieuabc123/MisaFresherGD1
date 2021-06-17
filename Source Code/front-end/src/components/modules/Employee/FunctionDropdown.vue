<template>
  <div class="function" :style="{ right: right + 'px', top: top + 'px' }">
    <div
      class="function-content"
      
      v-click-outside="onFocusOut"
    >
      <div class="function-item" @click="duplicateOnClick()">Nhân bản</div>
      <div class="function-item">Xóa</div>
      <div class="function-item">Ngưng Sử Dụng</div>
    </div>
  </div>
</template>

<script>
import vClickOutside from "v-click-outside";
export default {
  name: "FunctionDropdown",
  directives: {
    clickOutside: vClickOutside.directive,
  },
  props: {
    id:{
      default:null,
    },
    right: {
      default: 0,
    },
    top: {
      default: 0,
    },
    is_click_out_side: {
      default: false,
    },
  },
  methods: {
    onFocusOut() {
      // debugger // eslint-disable-line no-debugger
      if(this.is_click_out_side ==true)
      this.$emit("onClickOutside");
    },
    duplicateOnClick(){
      this.$emit("duplicateOnClick",this.id);
      this.$emit("onClickOutside");
    }
  },
  mounted() {
  },
};
</script>

<style lang="scss" scoped>
.function {
  position: absolute;
}
.function-content {
  padding: 2px 1px;
  position: absolute;
  width: 120px;
  height: 90px;
  border-radius: 3px;
  background-color: #fff;
  border: 1px solid #babec5;
  display: flex;
  flex-direction: column;
}
.function-item {
  position: relative;
  height: 30px;
  width: 100%;
  background: #fff;
}
</style>