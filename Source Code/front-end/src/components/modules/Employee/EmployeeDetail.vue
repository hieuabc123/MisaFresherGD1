<template>
  <div class="dialog employee-detail">
    <!-- phần mờ phía sau dialog -->
    <div class="modal"></div>
    <!--Nội dung phần dialog -->
    <div class="dialog-detail">
      <div class="dialog-header">
        <div class="dialog-header-title">
          <div class="mi mi-24 icon-close X" @click="btnCloseOnClick"></div>
          <div class="mi mi-24 icon-question"></div>
        </div>
        <div class="dialog-header-content">
          <div class="popup-title">Thông tin nhân viên</div>
          <div class="dialog-type">
            <input type="checkbox" id="customer" />
            <label for="customer">
              <div class="mi custom-checkbox"></div>
            </label>
            <label for="customer" class="type-text">Là khách hàng</label>
          </div>
          <div class="dialog-type">
            <input type="checkbox" id="production" />
            <label for="production">
              <div class="mi custom-checkbox"></div>
            </label>
            <label for="production" class="type-text">Là nhà cung cấp</label>
          </div>
        </div>
      </div>
      <div class="dialog-content"></div>
      <div class="dialog-bottom"></div>
    </div>
  </div>
</template>

<script>
export default {
  name: "EmployeeDetail",
};
</script>

<style lang="scss" scoped>
$dialog_width: 900px;
$dialog_height: auto;
@mixin flex-content-center {
  display: flex;
  align-items: center;
  justify-content: center;
}

//#region 2. Căn layout bố cục cho Nội dung thẻ Dialog ********/
.dialog {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 10;
  @include flex-content-center();
  // Css phần modal
  .modal {
    position: absolute;
    width: 100%;
    height: 100%;
    background: #000;
    opacity: 0.4;
  }
  // CSS phần nội dung
  .dialog-detail {
    position: absolute;
    background: #fff;
    width: $dialog_width;
    height: $dialog_height;
    .dialog-header {
      position: relative;
      top: 0;
      width: 100%;
      height: 74px;
      box-sizing: border-box;
    }
    .dialog-content {
      position: relative;
      top: 0;
      left: 0;
      width: 100%;
      height: auto;
    }
    .dialog-bottom {
      position: relative;
      top: 0;
      left: 0;
      width: 100%;
      height: auto;
    }
  }
}
//#endregion 2

//#region 3. Nội dung bên trong phần header ********/
.dialog-header {
  display: flex;
  //** phần nội dung thẻ header ---------------------**** */
  .dialog-header-content {
    position: absolute;
    left: 0;
    height: 100%;
    width: calc(100% - 74px);
    display: flex;
    align-items: center;
    padding-left: 32px;
    .popup-title {
      font-size: 24px;
      color: #111;
      font-weight: 700;
      font-family: inherit;
    }
    .dialog-type {
      display: flex;
      align-items: center;
      margin-left: 24px;
    }
    .type-text {
      margin-left: 8px;
      cursor: pointer;
    }
  }
  //** Nội dung phần góc phải của header ----------------------------** */
  .dialog-header-title {
    position: absolute;
    right: 0;
    height: 100%;
    width: 74px;
    display: flex;
    align-items: center;
  }
}
//#endregion 3

//#region 4. Nội dung bên trong phần content *********/
.dialog-content{
    
}
//#endregion
</style>