<template>
  <div class="dialog employee-detail">
    <!-- phần mờ phía sau dialog -->
    <div class="modal"></div>
    <!--Nội dung phần dialog -->
    <div class="dialog-detail">
      <div class="dialog-header">
        <div class="dialog-header-title">
          <div class="mi mi-24 icon-close X" @click="btnXOnClick()"></div>
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
                <input
                  type="text"
                  ref="employeeCode"
                  v-model="p_employee.employeeCode"
                  @mouseover="hoverInEmployeeCode"
                  @mouseleave="required.event.is_hover = false"
                  :class="{'validate': !required.employeeCode.check}"
                />
              </div>
            </div>
            <div class="grid-item" id="fullName">
              <div class="input-title">
                Tên
                <div style="color: red; padding-left: 4px">&nbsp; *</div>
              </div>
              <div class="input">
                <input
                  type="text"
                  v-model="p_employee.fullName"
                  @mouseover="hoverInFullName"
                  @mouseleave="required.event.is_hover = false"
                  :class="{'validate': !required.fullName.check}"
                />
              </div>
            </div>
            <div class="grid-item column-2" id="departmentName">
              <div class="input-title">
                Đơn vị
                <div style="color: red; padding-left: 4px">&nbsp; *</div>
              </div>
              <div class="input" @mouseover="hoverInDepartment" @mouseleave="required.event.is_hover = false">
                <AutoComplete
                  :options="departments"
                  :value_key="'departmentId'"
                  :label_key="'departmentName'"
                  :model_value.sync="p_employee.departmentId"
                  ref="autocomplete"
                  :validate_class="{'validate':!required.departmentId.check}"
                />
              </div>
            </div>
            <div class="grid-item column-2" id="positionName">
              <div class="input-title">Chức danh</div>
              <div class="input">
                <input type="text" v-model="p_employee.positionName" />
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
                <input type="date" v-model="p_employee.dateOfBirth" />
              </div>
            </div>
            <div class="grid-item column-2" id="gender">
              <div class="input-title">Giới tính</div>
              <div class="select-gender">
                <label for="1" class="flex">
                  <input
                    type="radio"
                    id="1"
                    value="1"
                    name="gender"
                    v-model="p_employee.gender"
                  />
                  <div class="custom-radio radio-border">
                    <div class="radio-content"></div>
                  </div>
                  <span>Nam</span>
                </label>

                <label for="0" class="flex">
                  <input
                    type="radio"
                    id="0"
                    value="0"
                    name="gender"
                    v-model="p_employee.gender"
                  />
                  <div class="custom-radio radio-border">
                    <div class="radio-content"></div>
                  </div>
                  <span>Nữ</span>
                </label>

                <label for="2" class="flex">
                  <input
                    type="radio"
                    id="2"
                    value="2"
                    name="gender"
                    v-model="p_employee.gender"
                  />
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
                <input type="text" v-model="p_employee.identityNo" />
              </div>
            </div>
            <div class="grid-item" id="identityDate">
              <div class="input-title">Ngày cấp</div>
              <div class="input">
                <input type="date" v-model="p_employee.identityDate" />
              </div>
            </div>
            <div class="grid-item column-3" id="identityPlace">
              <div class="input-title">Nơi cấp</div>
              <div class="input">
                <input type="text" v-model="p_employee.identityPlace" />
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
              <input type="text" v-model="p_employee.address" />
            </div>
          </div>
          <div class="grid-item" id="phoneNumber">
            <div class="input-title">Điện thoại di động</div>
            <div class="input">
              <input type="text" v-model="p_employee.phoneNumber" />
            </div>
          </div>
          <div class="grid-item" id="fixedLine">
            <div class="input-title">Điện thoại cố định</div>
            <div class="input">
              <input type="text" v-model="p_employee.fixedLine" />
            </div>
          </div>
          <div class="grid-item" id="email">
            <div class="input-title">Email</div>
            <div class="input">
              <input type="text" v-model="p_employee.email" />
            </div>
          </div>
          <div class="grid-item"></div>
          <div class="grid-item" id="accountNumber">
            <div class="input-title">Tài khoản ngân hàng</div>
            <div class="input">
              <input type="text" v-model="p_employee.accountNumber" />
            </div>
          </div>
          <div class="grid-item" id="bankName">
            <div class="input-title">Tên ngân hàng</div>
            <div class="input">
              <input type="text" v-model="p_employee.bankName" />
            </div>
          </div>
          <div class="grid-item" id="bankBranch">
            <div class="input-title">Chi nhánh</div>
            <div class="input">
              <input type="text" v-model="p_employee.bankBranch" />
              <input
                name="End-Tabindex"
                style="height: 0; border: none; padding: 0"
                @focus="autoFocusEmployeeCode()"
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
        <div class="btn btn-cancel">Hủy</div>
      </div>
    </div>
    <div
      class="required"
      :style="{
        left: +position_required.left + 'px',
        top: position_required.top + 'px',
      }"
      v-if="required.event.is_hover"
    >
      {{ required.event.message }}
    </div>
    

  </div>
</template>

<script>
import axios from "axios";
import AutoComplete from "../../Items/AutoComplete.vue";
import { baseUrl } from "../../../config/dev.env";
export default {
  name: "EmployeeDetail",
  components: { AutoComplete },
  props: {
    // Kiểu trạng thái của form thêm mới hay là sửa, nhân bản ...
    is_open: {
      default: false,
    },
    p_form_mode: {
      default: null,
    },
    departments: {
      default: [],
    },
    /**
     * Đối tượng employee Truyền vào
     */
    p_employee: {
      type: Object,
      default: null,
    },
  },
  data() {
    return {
      EmployeeDetail: {
        departments: [],
        employee: {},
      },
      required: {
        employeeCode: {
          check: true,
          message: null,
        },
        fullName: {
          check: true,
          message: null,
        },
        departmentId: {
          check: true,
          message: null,
        },
        event:{
          is_hover:false,
          message:null
        }
      },
      position_required: {
        top: 0,
        left: 0,
        time_out: null
      },
    };
  },
  methods: {
    //#region I. Xử lý các sự kiện
    //#region 1. Sự kiện hover
    /**
     * Hover vào input Mã nhân viên
     * Created By: NTHIEU (17/06/2021)
     */
    hoverInEmployeeCode(event) {
      if(this.required.employeeCode.check == false){
        this.required.event.message = this.required.employeeCode.message;
        this.getPositionOfMouse(event);
      }
      else this.required.event.is_hover = false;
    },

    /**
     * Hover vào input Tên nhân viên
     * Created By: NTHIEU (17/06/2021)
     */
    hoverInFullName(event) {
      if(this.required.fullName.check == false){
        this.required.event.message = this.required.fullName.message;
        this.getPositionOfMouse(event);
        
      }
      else this.required.event.is_hover = false;
      
    },

    /**
     * Hover vào Đơn vị, phòng ban
     * Created By: NTHIEU (17/06/2021)
     */
    hoverInDepartment(event) {
      if(this.required.departmentId.check == false){
        this.required.event.message = this.required.departmentId.message;
        this.getPositionOfMouse(event);
      }
      else this.required.event.is_hover = false;
    },

    /**
     * Lấy vị trí của chuột
     * Created By: NTHIEU (17/06/2021)
     */
    getPositionOfMouse(event) {
      var vue = this;
      clearTimeout(this.time_out);
      this.time_out = setTimeout(function () {
        vue.position_required.left = event.clientX + 5;
        vue.position_required.top = event.clientY + 5;
        vue.required.event.is_hover = true;
      }, 700);
    },
    //#endregion 1
    /**
     * Sự kiện click vào nút X tắt dialog
     * Created By: NTHIEU (16/06/2021)
     */
    btnXOnClick() {
      this.closeDialogEmployeeDetail();
    },
    /**
     * Sự kiện click vào nút Cất
     * Created By: NTHIEU (17/06/2021)
     */
    async btnSaveOnClick() {
      if (this.validate()) {
        switch (this.p_form_mode) {
          case "add":
            await this.addNewEmployee();
            break;
          case "edit":
            await this.updateEmployee();
            break;
          case "duplicate":
            await this.updateEmployee();
            break;
        }
      }
    },
    /**
     * Sự kiện click vào nút Cất và Thêm
     * Created By: NTHIEU (17/06/2021)
     */
    async btnSaveAndAddOnClick() {
      if (this.validate()) {
        switch (this.p_form_mode) {
          case "add":
            await this.addNewEmployee();
            break;
          case "edit":
            await this.updateEmployee();
            break;
          case "duplicate":
            await this.updateEmployee();
            break;
        }
      }
    },

    //#endregion I

    //#region II. method
    /**
     * Đóng form dialog Nhân viên
     * Created By: NTHIEU (16/06/2021)
     */
    closeDialogEmployeeDetail() {
      var isOpen = false;
      this.$emit("update:is_open", isOpen);
    },

    /**
     * Insert New Employee to Server
     * Created By: NTHIEU (17/06/2021)
     */
    async addNewEmployee() {
      var url = baseUrl + "/employees";
      var data = this.p_employee;
      await axios
        .post(url, data)
        .then((res) => {
          if (res.data.statusCode >= 400 && res.data.statusCode < 500)
            console.log(res.data);
          if (res.data.statusCode == 200) {
            console.log(res.data);
          }
        })
        .catch((error) => {
          error;
        });
    },

    /**
     * Update Employee to Server
     * Created By: NTHIEU (17/06/2021)
     */
    async updateEmployee() {
      var url = baseUrl + "/employees/" + this.p_employee.employeeId;
      var data = this.p_employee;
      await axios
        .put(url, data)
        .then((res) => {
          if (res.data.statusCode >= 400 && res.data.statusCode < 500)
            console.log(res.data);
          if (res.data.statusCode == 200) {
            console.log(res.data);
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },

    /**
     * Event focus on EmployeeCode's input
     * Created By: NTHIEU (16/06/2021)
     */
    autoFocusEmployeeCode() {
      // debugger; // eslint-disable-line no-debugger
      this.$refs.employeeCode.focus();
    },

    checkNullEmployeeCode() {
      var isValid = true;
      if (
        this.p_employee.employeeCode == null ||
        this.p_employee.employeeCode == ""
      ) {
        isValid = false;
        this.required.employeeCode.check = false;
        this.required.employeeCode.message =
          "Thông tin mã nhân viên không được để trống";
        this.p_employee.status = "Thông tin mã nhân viên không được để trống";
      }
      return isValid;
    },

    checkNullFullName() {
      var isValid = true;
      if (this.p_employee.fullName == null || this.p_employee.fullName == "") {
        isValid = false;
        this.required.fullName.check = false;
        this.required.fullName.message =
          "Thông tin Tên nhân viên không được để trống";
        this.p_employee.status = "Thông tin Tên nhân viên không được để trống";
      }
      return isValid;
    },
    checkNullDepartmentId() {
      var isValid = true;
      if (
        this.p_employee.departmentId == null ||
        this.p_employee.departmentId == ""
      ) {
        isValid = false;
        this.required.departmentId.check = false;
        this.required.departmentId.message =
          "Thông tin Đơn vị không được để trống";
        this.p_employee.status = "Thông tin Đơn vị không được để trống";
      }
      return isValid;
    },
    /**
     * Check Null or Empty Required
     * Created By: NTHIEU (17/06/2021)
     */
    checkNullOrEmpty() {
      var isValid = true;
      //1. Kiểm tra để trống thông tin Mã
      var check_null_employeeCode = this.checkNullEmployeeCode();
      //2. Kiểm tra để trống thông tin Họ và tên
      var check_null_fullName = this.checkNullFullName();
      //3. Kiểm tra để trông thông tin đơn vị
      var check_null_departmentId = this.checkNullDepartmentId();

      //4. Kiểm tra xem tát cả các trường có thỏa mãn điều kiện không
      if (
        check_null_employeeCode == true &&
        check_null_fullName == true &&
        check_null_departmentId == true
      )
        isValid = true;
      else isValid = false;

      //5. Giá trị trả về
      return isValid;
    },

    /**
     * Check Availble (Check tồn tại)
     * Created By: NTHIEU (17/06/2021)
     */
    checkAvailble() {
      var isValid = true;
      //1. Kiểm tra tồn tại Đơn vị
      if (this.p_employee.departmentId == "not_availble") {
        isValid = false;
        this.required.departmentId.check = false;
        this.required.departmentId.message =
          "Thông tin đơn vị không tồn tại trong hệ thống";
        this.p_employee.status =
          "Thông tin đơn vị không tồn tại trong hệ thống";
      }
      return isValid;
    },

    /**
     * Validate dữ liệu
     * Created By: NTHIEU (17/06/2021)
     */
    validate() {
      var isValid = false;
      var isCheckNull = this.checkNullOrEmpty();
      var isCheckAvailble = this.checkAvailble();
      if (isCheckNull == true && isCheckAvailble == true) {
        isValid = true;
      } else isValid = false;
      return isValid;
    },
    //#endregion II
  },
  created() {},
  mounted() {
    this.autoFocusEmployeeCode();
  },
};
</script>

<style lang="scss" scoped>
.required {
  position: absolute;
  width: auto;
  padding-left: 4px;
  padding-right: 4px;
  height: 26px;
  border: 1px solid black;
  background: #f7f7f7;
  border-radius: 2px;
  color: #000;
  display: flex;
  align-items: center;
}

//#region 1. Declare
$dialog_width: 900px;
$dialog_height: auto;
@mixin flex-content-center {
  display: flex;
  align-items: center;
  justify-content: center;
}
//#endregion 1

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
.validate{
  border-color: red !important;
}
</style>