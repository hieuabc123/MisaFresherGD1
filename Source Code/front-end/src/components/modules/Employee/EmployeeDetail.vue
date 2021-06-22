<template>
  <div class="dialog employee-detail" @keydown.esc="btnXOnClick">
    <!-- phần mờ phía sau dialog -->
    <div class="modal"></div>
    <!-- #region I Nội dung phần dialog -->
    <div class="dialog-detail zoomIn">
      <!-- #region 1. Dialog Header -->
      <div class="dialog-header">
        <div class="dialog-header-title">
          <div
            class="mi mi-24 icon-close X"
            @click="btnXOnClick"
            content="Đóng (ESC)"
            v-tippy="{ placement: 'bottom' }"
          ></div>
          <div
            class="mi mi-24 icon-question pointer"
            content="Giúp (F1)"
            v-tippy="{ placement: 'bottom' }"
          ></div>
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
      <!-- #endregion 1 -->

      <!-- #region 2. Dialog content -->
      <div class="dialog-content">
        <div class="form form-1">
          <!-- -->
          <!-- -->
          <!------- Grid-LEFT  ----------->
          <!-- -->
          <!-- -->
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
                  @mouseleave="mouseLeave"
                  :class="{ validate: !required.employeeCode.check }"
                  @focusout="checkEmployeeCode"
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
                  @mouseleave="mouseLeave"
                  :class="{ validate: !required.fullName.check }"
                  @focusout="checkFullName"
                />
              </div>
            </div>
            <div class="grid-item column-2" id="departmentName">
              <div class="input-title">
                Đơn vị
                <div style="color: red; padding-left: 4px">&nbsp; *</div>
              </div>
              <div class="input" @mouseover="hoverInDepartment">
                <AutoComplete
                  :options="departments"
                  :value_key="'departmentId'"
                  :label_key="'departmentName'"
                  :model_value.sync="p_employee.departmentId"
                  ref="autocomplete"
                  :validate_class="{ validate: !required.departmentId.check }"
                  @focusOut="validateDepartment"
                  @p_click="required.event.is_hover = false"
                  @mouseLeave="mouseLeave"
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
                <input
                  type="date"
                  placeholder="DD/MM/YYYY"
                  pattern="[0-9]{2}/[0-9]{2}/[0-9]{4}"
                  v-model="p_employee.dateOfBirth"
                  :class="{
                    visible:
                      p_employee.dateOfBirth != null &&
                      p_employee.dateOfBirth != '',
                  }"
                  max="9999-01-01"
                />
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
                <input
                  type="text"
                  v-model="p_employee.identityNo"
                  @input="checkIdentityNo(p_employee.identityNo)"
                  @mouseover="hoverInIdentityCode"
                  @mouseleave="mouseLeave"
                  :class="{ validate: !required.identityNo.check }"
                />
              </div>
            </div>
            <div class="grid-item" id="identityDate">
              <div class="input-title">Ngày cấp</div>
              <div class="input">
                <input
                  type="date"
                  v-model="p_employee.identityDate"
                  :class="{
                    visible:
                      p_employee.identityDate != null &&
                      p_employee.identityDate != '',
                  }"
                  max="9999-01-01"
                />
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
      <!-- #endregion 2 -->

      <!-- #region 3. Dialog Bottom -->
      <div class="dialog-bottom">
        <div class="btn btn-save" @click="btnSaveOnClick">Cất</div>
        <div class="btn btn-save-and-add" @click="btnSaveAndAddOnClick">
          Cất và thêm
        </div>
        <div class="btn btn-cancel" @click="closeDialogEmployeeDetail">Hủy</div>
      </div>
      <!-- #endregion 3 -->
    </div>
    <!-- #endregion I -->

    <!-- Nội dung phần hover required -->
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

    <!-- Thẻ Popup -->
    <Popup
      :p_isOpen="popup.isOpen"
      :p_message="popup.message"
      :p_mode="popup.mode"
      @p_closePopup="closePopup"
      @p_btnNotAgreeSave="closeDialogEmployeeDetail"
      @p_btnAgreeSave="btnSaveOnClick"
    />
  </div>
</template>

<script>
import axios from "axios";
import AutoComplete from "../../Items/AutoComplete.vue";
import { baseUrl } from "../../../config/dev.env";
import Popup from "../../Items/Popup.vue";
export default {
  name: "EmployeeDetail",
  components: { AutoComplete, Popup },

  //#region Declare
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
     * Đối tượng employee Truyền vào sẽ được thay đổi
     */
    p_employee: {
      type: Object,
      default: null,
    },

    /**
     * Đối tượng employee truyền vào mặc định
     */
    p_employee_before: {
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
        identityNo: {
          check: true,
          message: null,
        },
        event: {
          is_hover: false,
          message: null,
        },
      },
      position_required: {
        top: 0,
        left: 0,
        time_out: null,
      },
      process_isdone: false,
      //4. Đối tượng popup
      popup: {
        isOpen: false,
        message: "Warning",
        mode: "question",
      },
    };
  },
  //#endregion

  methods: {
    //#region I. Xử lý các sự kiện
    //#region 1. Sự kiện hover
    /**
     * Hover vào input Mã nhân viên
     * Created By: NTHIEU (17/06/2021)
     */
    hoverInEmployeeCode(event) {
      if (this.required.employeeCode.check == false) {
        this.required.event.message = this.required.employeeCode.message;
        this.getPositionOfMouse(event);
      } else {
        clearTimeout(this.time_out);
        this.required.event.is_hover = false;
      }
    },

    /**
     * Hover vào input Tên nhân viên
     * Created By: NTHIEU (17/06/2021)
     */
    hoverInFullName(event) {
      if (this.required.fullName.check == false) {
        this.required.event.message = this.required.fullName.message;
        this.getPositionOfMouse(event);
      } else {
        clearTimeout(this.time_out);
        this.required.event.is_hover = false;
      }
    },

    /**
     * Hover vào Đơn vị, phòng ban
     * Created By: NTHIEU (17/06/2021)
     */
    hoverInDepartment(event) {
      if (this.required.departmentId.check == false) {
        this.required.event.message = this.required.departmentId.message;
        this.getPositionOfMouse(event);
      } else {
        clearTimeout(this.time_out);
        this.required.event.is_hover = false;
      }
    },

    hoverInIdentityCode(event) {
      if (this.required.identityNo.check == false) {
        this.required.event.message = this.required.identityNo.message;
        this.getPositionOfMouse(event);
      } else {
        clearTimeout(this.time_out);
        this.required.event.is_hover = false;
      }
    },

    /**
     * Sự kiện rời chuột ra ngoài phần tử
     */
    mouseLeave() {
      clearTimeout(this.time_out);
      this.required.event.is_hover = false;
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

    //#region 2 Xử lý sự kiện Popup
    /**
     * Mở Popup
     * Created By: NTHIEU (17/06/2021)
     */
    openPopup(p_mode, p_message) {
      // debugger // eslint-disable-line no-debugger
      this.popup.mode = p_mode;
      this.popup.message = p_message;
      this.popup.isOpen = true;
    },

    /**
     * Đóng Popup
     * Created By: NTHIEU (17/06/2021)
     */
    closePopup() {
      this.popup.isOpen = false;
    },
    //#endregion 2

    //#region 3 Xử lý sự kiện click vào nút X tắt dialog
    /**
     * Sự kiện click vào nút X tắt dialog
     * Created By: NTHIEU (16/06/2021)
     */
    btnXOnClick() {
      if (this.shallowEqual(this.p_employee, this.p_employee_before) == true)
        this.closeDialogEmployeeDetail();
      else
        this.openPopup(
          "question",
          "Dữ liệu đã bị thay đổi, bạn có muốn cất không ?"
        );
    },
    //#endregion 3

    /**
     * Sự kiện click vào nút Cất
     * Created By: NTHIEU (17/06/2021)
     */
    async btnSaveOnClick() {
      this.process_isdone = false;
      if (this.validate()) {
        this.popup.isOpen = false;
        switch (this.p_form_mode) {
          case "add":
            await this.addNewEmployee();
            break;
          case "edit":
            await this.updateEmployee();
            break;
          case "duplicate":
            await this.addNewEmployee();
            break;
        }
        if (this.process_isdone == true) this.closeDialogEmployeeDetail();
      } else {
        this.openPopup("warning", this.p_employee.status);
      }
    },
    /**
     * Sự kiện click vào nút Cất và Thêm
     * Created By: NTHIEU (17/06/2021)
     */
    async btnSaveAndAddOnClick() {
      this.process_isdone = false;
      if (this.validate()) {
        switch (this.p_form_mode) {
          case "add":
            await this.addNewEmployee();
            break;
          case "edit":
            await this.updateEmployee();
            break;
          case "duplicate":
            await this.addNewEmployee();
            break;
        }
        if (this.process_isdone == true) {
          this.$emit("btnAddOnClick");
          this.$emit("update:p_form_mode", "add");
        }
      } else {
        this.openPopup("warning", this.p_employee.status);
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
     * Insert New Employee to Server (Thêm mới nhân viên)
     * Created By: NTHIEU (17/06/2021)
     */
    async addNewEmployee() {
      var url = baseUrl + "/employees";
      var data = this.p_employee;
      await axios
        .post(url, data)
        .then((res) => {
          if (res.data.statusCode >= 400 && res.data.statusCode < 500)
            if (res.data.data == "EmployeeCode") {
              this.openPopup("warning", res.data.userMsg);
              this.required.employeeCode.check = false;
              this.required.employeeCode.message = res.data.userMsg;
            }

          if (res.data.statusCode == 200) {
            this.$emit("loadComponent");
            this.process_isdone = true;
            this.$emit("openToast", "success", "Thêm mới thành công");
          }
        })
        .catch((error) => {
          this.openPopup(
            "warning",
            "Có lỗi với hệ thống vui lòng liên hệ Misa để được trợ giúp:" +
              error
          );
        });
    },

    /**
     * Update Employee to Server (Cập nhật thông tin nhân viên)
     * Created By: NTHIEU (17/06/2021)
     */
    async updateEmployee() {
      var url = baseUrl + "/employees/" + this.p_employee.employeeId;
      var data = this.p_employee;
      await axios
        .put(url, data)
        .then((res) => {
          if (res.data.statusCode >= 400 && res.data.statusCode < 500) {
            this.openPopup("warning", res.data.userMsg);
            this.required.employeeCode.check = false;
            this.required.employeeCode.message = res.data.userMsg;
          }

          if (res.data.statusCode == 200) {
            // debugger; // eslint-disable-line no-debugger
            this.$emit("loadComponent");
            this.process_isdone = true;
            this.$emit("openToast", "success", "sửa thành công");
          }
        })
        .catch((error) => {
          this.openPopup(
            "warning",
            "Có lỗi với hệ thống vui lòng liên hệ Misa để được trợ giúp:" +
              error
          );
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

    ///#region Validate
    /**
     * Check Mã nhân viên
     */
    checkEmployeeCode() {
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
      } else {
        this.required.employeeCode.check = true;
        isValid = true;
      }
      return isValid;
    },

    /**
     * check Họ tên
     */
    checkFullName() {
      var isValid = true;
      if (this.p_employee.fullName == null || this.p_employee.fullName == "") {
        isValid = false;
        this.required.fullName.check = false;
        this.required.fullName.message =
          "Thông tin Tên nhân viên không được để trống";
        this.p_employee.status = "Thông tin Tên nhân viên không được để trống";
      } else {
        this.required.fullName.check = true;
        isValid = true;
      }
      return isValid;
    },

    /**
     * Check phòng ban, đơn vị
     */
    checkDepartmentId() {
      var isValid = true;
      // 1. Check không được để trống
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
      // 2. Check tồn tại giá trị
      else if (this.checkAvailble() == true) {
        this.required.departmentId.check = true;
        isValid = true;
      } else isValid = false;
      return isValid;
    },

    /**
     * Check lại phòng ban đơn vị, khi nhập liệu
     */
    validateDepartment() {
      if (this.required.departmentId.check == false) {
        this.checkDepartmentId();
      }
    },
    /**
     * Check Null or Empty Required
     * Created By: NTHIEU (17/06/2021)
     */
    validate() {
      var isValid = true;
      //1. Kiểm tra để trống thông tin Mã
      var check_employeeCode = this.checkEmployeeCode();
      //2. Kiểm tra để trống thông tin Họ và tên
      var check_fullName = this.checkFullName();
      //3. Kiểm tra để trông thông tin đơn vị
      var check_departmentId = this.checkDepartmentId();
      //4. Kiểm tra định dạng số CMND
      var check_identityNo = this.checkIdentityNo();

      //4. Kiểm tra xem tát cả các trường có thỏa mãn điều kiện không
      if (
        check_employeeCode == true &&
        check_fullName == true &&
        check_departmentId == true &&
        check_identityNo == true
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
      } else {
        // debugger // eslint-disable-line no-debugger
        this.required.departmentId.check = true;
        isValid = true;
      }
      return isValid;
    },

    /**
     * Check định dạng của CMT, CMND
     * Created By: NTHIEU (17/06/2021)
     */
    checkIdentityNo() {
      if (
        this.p_employee.identityNo == "" ||
        this.p_employee.identityNo == null
      ) {
        this.required.identityNo.check = true;
        return true;
      }
      var number = /^\d+$/;
      let isnum = number.test(this.p_employee.identityNo);
      if (isnum == true) {
        this.required.identityNo.check = true;
        return true;
      } else {
        this.required.identityNo.check = false;
        this.required.identityNo.message =
          "Thông tin số CMND không đúng định dạng";
        this.p_employee.status = "Thông tin số CMND không đúng định dạng";
        return false;
      }
    },
    //#endregion

    /**
     * Hàm so sánh nông ( so sánh từng giá trị của từng phần tử)
     * Created By: NTHIEU (18/06/2021)
     */
    shallowEqual(object1, object2) {
      //1. Lấy chuỗi keys của từng object (giống lấy properties trong C#)
      const keys1 = Object.keys(object1);
      const keys2 = Object.keys(object2);
      //2. Kiểm tra xem độ dài của 2 properties khác nhau không
      if (keys1.length !== keys2.length) {
        return false;
      }
      //3. Kiểm tra từng giá trị value tương ứng với từng key
      for (let key of keys1) {
        if (key != "status")
          if (object1[key] !== object2[key]) {
            if (
              (object1[key] == null && object2[key] == "") ||
              (object1[key] == "" && object2[key] == null)
            ) {
              continue;
            } else return false;
          }
      }
      //4. kết quả trả về default
      return true;
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
    width: calc(100% - 74px - 32px);
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
.validate {
  border-color: red !important;
}
</style>