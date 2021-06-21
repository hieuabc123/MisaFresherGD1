<template>
  <div class="the-navbar" :class="{ narrow: is_narrow }">
    <div class="navbar-header">
      <div class="menu-option">
        <a href class="mi icon-menu-option"></a>
      </div>
      <div class="logo-container">
        <a href="/">
          <img
            src="../../assets/img/Logo_Module_TiengViet_White.66947422.svg"
            height="100%"
          />
        </a>
      </div>
      <div class="mi-24 toggle-menu" @click="is_narrow = !is_narrow">
        <div class="mi icon-toggle-menu"></div>
      </div>
    </div>
    <div class="navbar-content">
      <router-link to="1" class="menu-item">
        <div class="menu-item-icon mi mi-20 icon-dashboard"></div>
        <div class="menu-item-title">Tổng quan</div>
      </router-link>
      <router-link to="2" class="menu-item">
        <div class="menu-item-icon mi mi-20 icon-cash"></div>
        <div class="menu-item-title">Tiền mặt</div>
      </router-link>
      <router-link to="3" class="menu-item">
        <div class="menu-item-icon mi mi-20 icon-bank"></div>
        <div class="menu-item-title">Tiền gửi</div>
      </router-link>
      <router-link to="4" class="menu-item">
        <div class="menu-item-icon mi mi-20 icon-purchase"></div>
        <div class="menu-item-title">Mua hàng</div>
      </router-link>
      <router-link to="5" class="menu-item">
        <div class="menu-item-icon mi mi-20 icon-sale"></div>
        <div class="menu-item-title">Bán hàng</div>
      </router-link>
      <router-link to="6" class="menu-item">
        <div class="menu-item-icon mi mi-20 icon-invoice"></div>
        <div class="menu-item-title">Quản lý hóa đơn</div>
      </router-link>
      <router-link to="7" class="menu-item">
        <div class="menu-item-icon mi mi-20 icon-stock"></div>
        <div class="menu-item-title">Kho</div>
      </router-link>
      <router-link to="8" class="menu-item">
        <div class="menu-item-icon mi mi-20 icon-tool"></div>
        <div class="menu-item-title">Công cụ dụng cụ</div>
      </router-link>
      <router-link to="9" class="menu-item">
        <div class="menu-item-icon mi mi-20 icon-fixed-assets"></div>
        <div class="menu-item-title">Tài sản cố định</div>
      </router-link>
      <router-link to="10" class="menu-item">
        <div class="menu-item-icon mi mi-20 icon-tax"></div>
        <div class="menu-item-title">Thuế</div>
      </router-link>
      <router-link to="11" class="menu-item">
        <div class="menu-item-icon mi mi-20 icon-price"></div>
        <div class="menu-item-title">Giá thành</div>
      </router-link>
      <router-link to="12" class="menu-item">
        <div class="menu-item-icon mi mi-20 icon-general"></div>
        <div class="menu-item-title">Tổng hợp</div>
      </router-link>
      <router-link to="13" class="menu-item">
        <div class="menu-item-icon mi mi-20 icon-budget"></div>
        <div class="menu-item-title">Ngân sách</div>
      </router-link>
      <router-link to="14" class="menu-item">
        <div class="menu-item-icon mi mi-20 icon-report"></div>
        <div class="menu-item-title">Báo cáo</div>
      </router-link>
      <router-link to="15" class="menu-item">
        <div class="menu-item-icon mi mi-20 icon-finance"></div>
        <div class="menu-item-title">Phân tích tài chính</div>
      </router-link>
      <router-link to="Employee" class="menu-item">
        <div class="menu-item-icon mi mi-20 icon-employee"></div>
        <div class="menu-item-title">Nhân viên</div>
      </router-link>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheNavbar",
  data() {
    return {
      is_narrow: false,
    };
  },
};
</script>

<style lang="scss" scope>
@mixin the-navbar {
  z-index: 2;
  position: relative;
  top: 0;
  left: 0;
  height: 100vh;
  width: 180px;
  box-sizing: border-box;
  background: #393a3d;
}

.the-navbar {
  @include the-navbar();
}

/** 1. -----navbar-header------------------------------ */
.the-navbar .navbar-header {
  height: 48px;
  display: flex;
  flex-direction: row;
  align-items: center;
  padding-left: 24px;
  padding-right: 24px;
  .menu-option {
    width: 24px;
    height: 24px;
    margin-right: 10px;
    display: flex;
    align-items: center;
  }
  .logo-container {
    height: 32px;
  }
}

/**-------navbar-content--------------------- */
.navbar-content {
  padding-top: 14px;
}
.navbar-content a {
  color: #fff;
}

.menu-item {
  display: flex;
  height: 42px;
  align-items: center;
  padding-left: 16px;
  &:hover{
    background: #616164;
  }
}
.router-link-exact-active{
  background: #616164;
}
.menu-item-icon {
  margin-right: 8px;
}

.toggle-menu {
  position: absolute;
  right: -40px;
  cursor: pointer;
}

.narrow {
  width: 50px;
  .navbar-header{
    justify-content: center;
  }
  .toggle-menu {
    position: relative;
    right: unset;
    .icon-toggle-menu {
      background-position: -316px -37px;
      width: 16px;
      height: 14px;
    }
  }
  .menu-item-title,
  .menu-option,
  .logo-container {
    display: none !important;
  }
}

</style>
