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
      <div class="dialog-content">
        <div class="form form-1">
          <div class="w-1/2 grid-left">
            <div class="grid-item" id="employeeCode">
              <div class="input-title">
                Mã
                <div style="color: red; padding-left: 4px">&nbsp; *</div>
              </div>
              <div class="input">
                <input type="text" ref="employeeCode" />
              </div>
            </div>
            <div class="grid-item" id="fullName">
              <div class="input-title">
                Tên
                <div style="color: red; padding-left: 4px">&nbsp; *</div>
              </div>
              <div class="input">
                <input type="text" />
              </div>
            </div>
            <div class="grid-item column-2" id="departmentName">
              <div class="input-title">
                Đơn vị
                <div style="color: red; padding-left: 4px">&nbsp; *</div>
              </div>
              <div class="input">
                <AutoComplete/>
              </div>
            </div>
            <div class="grid-item column-2" id="positionName">
              <div class="input-title">Chức danh</div>
              <div class="input">
                <input type="text" />
              </div>
            </div>
          </div>
          <!-- -->
          <!-- -->
          <!------- Grid-RIGHT  ----------->
          <!-- -->
          <!-- -->
          <div class="w-1/2 grid-right">
            <div class="grid-item" id="dateOfBirth">
              <div class="input-title">Ngày sinh</div>
              <div class="input">
                <input type="date" />
              </div>
            </div>
            <div class="grid-item column-2" id="gender">
              <div class="input-title">Giới tính</div>
              <div class="select-gender">
                <label for="1" class="flex">
                  <input type="radio" id="1" value="1" name="gender" />
                  <div class="custom-radio radio-border">
                    <div class="radio-content"></div>
                  </div>
                  <span>Nam</span>
                </label>

                <label for="0" class="flex">
                  <input type="radio" id="0" value="0" name="gender" />
                  <div class="custom-radio radio-border">
                    <div class="radio-content"></div>
                  </div>
                  <span>Nữ</span>
                </label>

                <label for="2" class="flex">
                  <input type="radio" id="2" value="2" name="gender" />
                  <div class="custom-radio radio-border">
                    <div class="radio-content"></div>
                  </div>
                  <span>Khác</span>
                </label>
              </div>
            </div>
            <div class="grid-item column-2" id="identityNo">
              <div class="input-title">Số CMND</div>
              <div class="input">
                <input type="text" />
              </div>
            </div>
            <div class="grid-item" id="identityDate">
              <div class="input-title">Ngày cấp</div>
              <div class="input">
                <input type="date" />
              </div>
            </div>
            <div class="grid-item column-3" id="identityPlace">
              <div class="input-title">Nơi cấp</div>
              <div class="input">
                <input type="text" />
              </div>
            </div>
          </div>
        </div>
        <!--  -->
        <!--  -->
        <!-- -------------FORM-2------------------ -->
        <!--  -->
        <!--  -->
        <!--  -->
        <div class="form form-2">
          <div class="grid-item column-4" id="address">
            <div class="input-title">Địa chỉ</div>
            <div class="input">
              <input type="text" />
            </div>
          </div>
          <div class="grid-item" id="phoneNumber">
            <div class="input-title">Điện thoại di động</div>
            <div class="input">
              <input type="text" />
            </div>
          </div>
          <div class="grid-item" id="fixedLine">
            <div class="input-title">Điện thoại cố định</div>
            <div class="input">
              <input type="text" />
            </div>
          </div>
          <div class="grid-item" id="email">
            <div class="input-title">Email</div>
            <div class="input">
              <input type="text" />
            </div>
          </div>
          <div class="grid-item"></div>
          <div class="grid-item" id="accountNumber">
            <div class="input-title">Tài khoản ngân hàng</div>
            <div class="input">
              <input type="text" />
            </div>
          </div>
          <div class="grid-item" id="bankName">
            <div class="input-title">Tên ngân hàng</div>
            <div class="input">
              <input type="text" />
            </div>
          </div>
          <div class="grid-item" id="bankBranch">
            <div class="input-title">Chi nhánh</div>
            <div class="input">
              <input type="text" />
              <input
                name="End-Tabindex"
                style="height: 0; border: none; padding: 0"
                @focus="autoFocusInput"
              />
            </div>
          </div>
          <div class="grid-item"></div>
        </div>
      </div>
      <div class="dialog-bottom">
        <div class="btn btn-save" @click="btnSaveOnClick">Cất</div>
        <div class="btn btn-save-and-add" @click="btnSaveAndAddOnClick">
          Cất và thêm
        </div>
        <div class="btn btn-cancel" @click="btnCloseOnClick">Hủy</div>
      </div>
    </div>
  </div>
</template>

<script>
import AutoComplete from '../../Items/AutoComplete.vue';
export default {
  name: "EmployeeDetail",
  components: { AutoComplete },
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
    border-radius: 4px;
    overflow: hidden;
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
      right: 0;
      height: auto;
      display: flex;
      flex-direction: column;
      padding-left: 32px;
      padding-right: 32px;
    }
    .dialog-bottom {
      position: relative;
      top: 0;
      left: 0;
      width: 100%;
      height: 80px;
      display: flex;
      align-items: center;
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
.dialog-content {
  .popup-title {
    font-size: 24px;
    color: #111;
    font-weight: 700;
    font-family: inherit;
  }
  .dialog-type {
    display: flex;
    align-items: center;
  }
  .type-text {
    margin-left: 8px;
    cursor: pointer;
  }
  .dialog-type {
    margin-left: 24px;
  }
  .form {
    position: relative;
    width: 100%;
  }
  .form-1 {
    display: grid;
    grid-template-columns: calc(50% - 13px) auto;
    grid-gap: 26px;
  }
  .form-1 .grid-left {
    box-sizing: border-box;
    /* border: 1px solid black; */
    display: grid;
    grid-template-columns: 150px auto;
    grid-column-gap: 6px;
    grid-row-gap: 12px;
  }
  .form-1 .grid-right {
    box-sizing: border-box;
    /* border: 1px solid black; */
    display: grid;
    grid-template-columns: 160px auto 160px;
    grid-column-gap: 6px;
    grid-row-gap: 12px;
  }
  .form-2 {
    margin-top: 32px;
    display: grid;
    grid-template-columns: calc(25%) calc(25%) calc(25%) auto;
    grid-column-gap: 6px;
    grid-row-gap: 12px;
    padding-bottom: 40px;
    border-bottom: 1px solid #c9ccd2;
  }
  .input-title {
    height: 22px;
    display: flex;
    padding-left: 0px;
    font-weight: 600;
  }
  .column-2 {
    grid-column: span 2;
  }
  .column-3 {
    grid-column: span 3;
  }
  .column-4 {
    grid-column: span 4;
  }
  #gender {
    padding-left: 16px;
    padding-right: 32px;
  }
  .select-gender {
    display: flex;
    justify-content: space-between;
    align-items: center;
    height: 32px;
  }
}

//#endregion

/* -------Button,INPUT,SELECT------------------------------------------- */
.btn-save,
.btn-cancel {
  border: 1px solid #8d9096;
  box-sizing: border-box;
}
.btn-save:hover,
.btn-cancel:hover {
  background-color: #d2d3d6;
}
.btn-save-and-add {
  color: #fff;
  background: #2ca01c;
}
.btn-save-and-add:hover {
  background-color: #35bf22;
}
.btn-save {
  position: absolute;
  right: 162px;
}
.btn-cancel {
  position: absolute;
  left: 32px;
}
.btn-save-and-add {
  position: absolute;
  right: 32px;
}
.input {
  position: relative;
}
input {
  padding-left: 8px;
  padding-right: 8px;
  border-radius: 3px;
}
input:focus {
  outline: none;
  border: 1px solid #2ca01c;
}
input[type="radio"] {
  display: none;
}
input[type="radio"]:checked + .radio-border {
  border: 1px solid #2ca01c;
}
input[type="radio"]:checked + .custom-radio .radio-content {
  background-color: #2ca01c;
}
.custom-radio {
  margin-right: 8px;
}
.radio-border {
  border: 1px solid #babec5;
  border-radius: 50%;
  width: 18px;
  height: 18px;
  display: flex;
  align-items: center;
  justify-content: center;
}
.radio-content {
  width: 12px;
  height: 12px;
  border-radius: 50%;
}
.flex {
  display: flex;
}
</style>