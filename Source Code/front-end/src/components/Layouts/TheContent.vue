<template>
  <div class="the-content">
      <router-view></router-view>
  </div>
</template>

<script>
export default {
  name: "TheContent",
};
</script>

<style lang="scss" scope>
.the-content{
    position: absolute;
    top: 48px;
    left: 0;
    right: 0;
    bottom: 0;
    box-sizing: border-box;
    background: #f8f8f8;
    overflow: hidden;  
}
</style>