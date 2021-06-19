<template>
  <div class="the-header">
    
    <div class="header-branch">
      <div class="header-branch-name">
        CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - DỊCH VỤ QUI PHÚC
      </div>
      <div class="header-branch-icon mi-14">
        <div class="mi icon-branch"></div>
      </div>
    </div>
    <div class="mi mi-24 icon-bell"></div>
    <div class="account-info">
      <div class="mi mi-32 icon-avatar"></div>
      <div class="user-name">Nguyễn Trung Hiếu</div>
      <div class="mi icon-dropdown"></div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheHeader",
};
</script>

<style lang="scss" scope>
@mixin the-header {
  position: absolute;
  height: 48px;
  left: 0;
  right: 0;
  top: 0;
  box-sizing: border-box;
  display: flex;
  align-items: center;
  background: #ffffff;
  padding-left: 8px;
}

.the-header {
  @include the-header();
}

.header-branch {
  display: flex;
  align-items: center;
  margin-left: 40px;
}
.header-branch-icon {
  margin-left: 8px;
}
.header-branch-name {
  font-size: 14px;
  color: #111;
  font-family: notosans-bold;
}
.account-info {
  margin-right: 8px;
  display: flex;
  align-items: center;
}

</style>