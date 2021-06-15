<template>
  <div class="employee-list">
    <div class="loading" :class="{ displayNone: EmployeeList.isDone }">
      <div class="modal"></div>
      <div class="lds-dual-ring"></div>
    </div>
    <div class="title-distance">
      <div class="title">Nhân viên</div>
      <div class="btn btn-add">Thêm mới nhân viên</div>
    </div>
    <!-- #region I. Grid -->
    <div class="grid">
      <!-- #region 1. Grid-header -->
      <div class="grid-header">
        <div class="mi icon-load" @click="loadGridContent"></div>
        <div class="input-search">
          <input
            type="text"
            placeholder="tìm kiếm theo mã, tên nhân viên"
            v-model="EmployeeList.filter"
            @input="onChangeInputFilter"
          />
          <div class="mi icon-search pointer"></div>
        </div>
      </div>
      <!-- #endregion -->

      <!-- #region 2. Grid-Content -->
      <div class="grid-content">
        <table>
          <!--  Header table -->
          <thead>
            <tr>
              <!-- Check box -->
              <th class="checkbox">
                <input type="checkbox" id="all" />
                <label for="all">
                  <div class="mi custom-checkbox"></div>
                </label>
                <div class="border-right"></div>
              </th>
              <!-- Mã nhân viên -->
              <th class="EmployeeCode">
                Mã nhân viên
                <div class="border-right"></div>
              </th>
              <!-- Tên nhân viên -->
              <th class="FullName">
                Tên nhân viên
                <div class="border-right"></div>
              </th>
              <!-- Giới tính -->
              <th class="Gender">
                Giới tính
                <div class="border-right"></div>
              </th>
              <!-- Ngày sinh -->
              <th class="DateOfBirth">
                Ngày sinh
                <div class="border-right"></div>
              </th>
              <!-- Số CMT, CMND -->
              <th class="IdentityNo">
                Số CMND
                <div class="border-right"></div>
              </th>
              <!-- Chức danh -->
              <th class="Position">
                Chức danh
                <div class="border-right"></div>
              </th>
              <!-- Đơn vị -->
              <th class="DepartmentName">
                Tên đơn vị
                <div class="border-right"></div>
              </th>
              <!-- Số tài khoản -->
              <th class="AccountNumber">
                Số tài khoản
                <div class="border-right"></div>
              </th>
              <!-- Tên Ngân hàng -->
              <th class="BankName">
                Tên ngân hàng
                <div class="border-right"></div>
              </th>
              <!-- Chi nhánh ngân hàng -->
              <th class="BankBranch">Chi nhánh tk ngân hàng</th>
              <!-- Cột Chức năng -->
              <th class="Function">
                Chức năng
                <div class="border-left"></div>
              </th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="(employee, index) in EmployeeList.employees"
              :key="employee.EmployeeId"
              :class="{
                selected: EmployeeList.tr_selected_id == employee.employeeId,
              }"
              @click="trOnClick(employee.employeeId)"
            >
              <!-- Check Box -->
              <td class="checkbox">
                <input type="checkbox" :id="employee.EmployeeId" />
                <label for="employee.EmployeeId">
                  <div class="mi custom-checkbox"></div>
                </label>
                <div class="border-right"></div>
              </td>
              <!-- Mã nhân viên -->
              <td class="EmployeeCode">{{ employee.employeeCode }}</td>
              <!-- Họ và tên -->
              <td class="FullName">{{ employee.fullName }}</td>
              <!-- Giới tính -->
              <td class="Gender">{{ employee.genderName }}</td>
              <!-- Ngày sinh -->
              <td class="DateOfBirth">
                {{ employee.dateOfBirth }}
              </td>
              <!-- Số CMT, CMND -->
              <td class="IdentityNo">{{ employee.identityNo }}</td>
              <!-- Chức Danh -->
              <td class="Position">{{ employee.positionName }}</td>
              <!-- Đơn vị -->
              <td class="DepartmentName">{{ employee.departmentName }}</td>
              <!-- Số tk ngân hàng -->
              <td class="AccountNumber">{{ employee.accountNumber }}</td>
              <!-- Tên Ngân hàng -->
              <td class="BankName">{{ employee.bankName }}</td>
              <!-- Chi nhánh ngân hàng -->
              <td class="BankBranch">{{ employee.bankBranch }}</td>
              <!-- Cột Chức năng -->
              <td class="Function">
                <div class="function-content">
                  <div class="btn-edit">Sửa</div>
                  <div
                    class="btn btn-toggle-select"
                    @click.stop.prevent="
                      onClickToggleButton(
                        index,
                        FunctionDropdown.index_selecting
                      )
                    "
                    :id="`row${index}`"
                    v-click-outside="functionToggleClickOutSide"
                  >
                    <div class="mi icon-toggle-select"></div>
                  </div>
                  <div class="border-left"></div>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <!-- #endregion -->

      <!-- #region 3. Grid-Bottom -->
      <div class="grid-bottom pagination-bar">
        <div class="left-pagination">
          <div class="total-record">
            Tổng số: <b>{{ EmployeeList.total_employees }}</b> bản ghi
          </div>
        </div>
        <div class="right-pagination">
          <div class="record-in-page">
            <select
              class="record-in-page-value"
              v-model="EmployeeList.pageSize"
              @change="function(){ loadDataPagingFilter(); EmployeeList.pageInt = 1;}"
            >
              <option value="10">10 bản ghi trên 1 trang</option>
              <option value="20">20 bản ghi trên 1 trang</option>
              <option value="30">30 bản ghi trên 1 trang</option>
              <option value="50">50 bản ghi trên 1 trang</option>
            </select>
          </div>
          <paginate
            :page-count="EmployeeList.pageCount"
            :click-handler="pageNumberOnClick"
            :force-page="EmployeeList.pageInt"
            :prev-text="'Trước'"
            :next-text="'Sau'"
            :container-class="'select-page'"
            :prev-link-class="'page-prev'"
            :next-link-class="'page-next'"
            :page-link-class="'page-link'"
            :page-class="'page-number'"
          >
          </paginate>
        </div>
      </div>
      <!-- #endregion -->
    </div>
    <!-- #endregion -->

    <FunctionDropdown
      v-if="FunctionDropdown.isOpen"
      :left="FunctionDropdown.left"
      :top="FunctionDropdown.top"
      @onClickOutside="onClickOutside"
      :is_click_out_side="FunctionDropdown.isClickOutSide"
    />
    <EmployeeDetail v-if="true" />
  </div>
</template>

<script>
import Paginate from "vuejs-paginate";
import axios from "axios";
import FunctionDropdown from "./FunctionDropdown.vue";
import vClickOutside from "v-click-outside";
import EmployeeDetail from "./EmployeeDetail.vue";
export default {
  name: "EmployeeList",
  directives: {
    clickOutside: vClickOutside.directive,
  },
  components: {
    Paginate,
    FunctionDropdown,
    EmployeeDetail,
  },
  data() {
    return {
      time_out: null,
      //1. Đối tượng EmployeeList, đối tượng nhân viên cho component
      EmployeeList: {
        employees: [], // danh sách các nhân viên
        isDone: true, // Kiểm tra load dữ liệu xong chưa
        pageCount: 1, // Tổng số trang
        pageInt: 1, // Số thứ tự của trang
        pageSize: 30, // Số bản ghi trên trang
        filter: "", // Giá trị cần tìm kiếm (filter)
        tr_selected_id: null, // Dòng nào đang được chọn (lấy id) or nhân viên có id đang được chọn
        total_employees: 0, // đếm tổng số bản ghi
      },
      //2. Đối tượng Component Function Dropdown
      FunctionDropdown: {
        isOpen: false, // Kiểm tra mở đóng
        left: 0, // vị trí căn lề bên trái
        top: 0, // Vị trí căn lề bên phải
        index_selecting: null, // giá trị trước khi click vào 1 đối tượng click toggle, (giá trị này sẽ được thay đổi sau khi click)
        isClickOutSide: false,
      },
    };
  },
  methods: {
    //#region Các sự kiện

    //#region 1. Xử lý Sự kiện nút Toggle ở mục chức năng
    //******************************************************************************************** */
    /**
     * Sự kiện khi click vào nút Toggle
     *  param:{
     *  - index : giá trị index truyền vào sau khi click
     *  - index_selecting : giá trị hiện đang lựa chọn trước khi click
     * }
     * Created By: NTHIEU (15/06/2021)
     */
    onClickToggleButton(index, index_selecting) {
      //0. Báo cho dropdown tự custom đây không phải là click out side
      this.FunctionDropdown.isClickOutSide = false;
      //1. Nếu giá trị trước khi truyền vào bằng giá trị sau khi truyền vào (Tức là click vào cùng 1 đối tượng liên tục)
      if (index == index_selecting) {
        //1.1 Thực hiện việc toggle
        this.FunctionDropdown.isOpen = !this.FunctionDropdown.isOpen;
      }
      //2. Nếu giá trị trước khi truyền vào khác giá trị sau khi truyền vào (Tức là click vào đối tượng toogle khác)
      else {
        //2.1 Thực hiện việc mở dropdown
        this.openFuntionDropdown();
        //2.2 Gán giá trị sau khi click
        this.FunctionDropdown.index_selecting = index;
      }
      //#region 3. Thực hiện việc lấy và gán vị trí của phần tử dropdown
      // Gán id
      var element = `#row${index}`;
      //3.2 Gán đối tượng cần lấy vị trí
      let position = document.querySelector(element);
      //3.3 Lấy vị trí top và left
      var left = position.getBoundingClientRect().left - 180 - 90;
      var top = position.getBoundingClientRect().top - 48 + 20;
      this.FunctionDropdown.left = left;
      this.FunctionDropdown.top = top;
      //3.4 Kiểm tra nếu vị trí ở bên dưới quá thì đổi vị trí cho thằng dropdown lên trên
      if (top > 600) {
        this.FunctionDropdown.top -= 115;
      }
      //#endregion
    },

    /**
     * Mở Dropdown
     * Created By: NTHIEU (15/06/2021)
     */
    openFuntionDropdown() {
      this.FunctionDropdown.isOpen = true;
    },

    /**
     * Đóng Dropdown: NTHIEU (15/06/2021)
     * Created BY: NTHIEU (15/06/2021)
     */
    closeFunctionDropdown() {
      this.FunctionDropdown.isOpen = false;
    },

    /**
     * Click ra ngoài cả 2 phần tử toggle và dropdown
     * Created By: NTHIEU (15/06/2021)
     */
    onClickOutside() {
      this.closeFunctionDropdown();
    },

    /**
     * Click ra ngoài mỗi phần tử Toggle thôi
     * Created By: NTHIEU (15/06/2021)
     */
    functionToggleClickOutSide() {
      // Báo cho Thằng Dropdown là tao đã bị click ra ngoài rồi để dropdown kiểm tra xem có click ra ngoài nữa không
      this.FunctionDropdown.isClickOutSide = true;
    },
    //#endregion Sự kiện nút Toggle ở mục chức năng

    //#region 2. Xử lý Sự kiện input tìm kiếm
    onChangeInputFilter() {
      this.EmployeeList.pageInt = 1;
      clearTimeout(this.time_out);
      this.time_out = setTimeout(this.loadDataPagingFilter, 500);
    },
    //#endregion 2

    //#region 3 Sự kiện click vào từng dòng trong table
    trOnClick(id) {
      this.EmployeeList.tr_selected_id = id;
    },
    //#endregion 3

    //#endregion Các sự kiện

    //#region Method
    /**
     * <summary> Load Grid Content</summary>
     * CreatedBy: Nguyễn Trung Hiếu (09/5/2021)
     ********************************************************/
    loadGridContent() {
      this.loadDataPagingFilter(); //dữ liệu Phân trang
    },

    /**
     * <summary> Load dữ liệu phân trang (Pagination)</summary>
     * CreatedBy: Nguyễn Trung Hiếu (09/5/2021)
     ********************************************************/
    loadDataPagingFilter() {
      this.EmployeeList.isDone = false;
      var p_pageInt = this.EmployeeList.pageInt.toString();
      var p_pageSize = this.EmployeeList.pageSize.toString();
      var p_filter = this.EmployeeList.filter.toString();
      var url =
        "http://localhost:60651/api/Employees/filter?pageint=" +
        p_pageInt +
        "&pagesize=" +
        p_pageSize +
        "&datafilter=" +
        p_filter;
      axios
        .get(url)
        .then((res) => {
          if (res.data.data != null) {
            this.EmployeeList.employees = res.data.data.employees;
            this.EmployeeList.total_employees = res.data.data.total;
          } else {
            this.EmployeeList.employees = null;
            this.EmployeeList.total_employees = 0;
          }

          this.EmployeeList.isDone = true;

          this.EmployeeList.pageCount = Math.ceil(
            parseInt(this.EmployeeList.total_employees) /
              parseInt(this.EmployeeList.pageSize)
          ); // Tính số trang = tổng số bản ghi / pageSize
        })
        .catch((res) => {
          console.log(res);
        });
    },

    //Click vào pageNumber (Phân trang)
    pageNumberOnClick(page) {
      this.EmployeeList.pageInt = page;
      this.loadGridContent();
    },
    //#endregion
  },
  created() {
    this.loadGridContent();
  },
};
</script>

<style lang="scss" scope>
/* --------------------Layout------------------------- */
//#region Grid Layout
.title-distance {
  position: absolute;
  top: 0;
  left: 20px;
  right: 20px;
  height: 60px;
  display: flex;
  align-items: center;
}
.title {
  font-family: notosans-semibold;
  font-size: 24px;
}
.grid {
  position: absolute;
  top: 60px;
  left: 20px;
  right: 20px;
  bottom: 10px;
  background: #fff;
}
.grid-header {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  height: 72px;
  display: flex;
  align-items: center;
  box-sizing: border-box;
}
.grid-content {
  position: absolute;
  top: 72px;
  left: 0;
  right: 0;
  bottom: 46px;
  box-sizing: border-box;
  /* border: 1px solid black; */
  overflow: auto;
}
.grid-bottom {
  position: absolute;
  height: 46px;
  left: 0;
  right: 0;
  bottom: 0;
  box-sizing: border-box;
  /* border: 1px solid green; */
}
//#endregion

/*---------------------- Các phần tử con của trang ---------------------------- */
.pagination-bar {
  display: flex;
  align-items: center;
}
.left-pagination {
  position: absolute;
  left: 0;
  height: 100%;
  display: flex;
  align-items: center;
}
.right-pagination {
  position: absolute;
  right: 0;
  height: 100%;
  display: flex;
  align-items: center;
}
.record-in-page {
  height: 32px;
  width: 190px;
}
.select-page {
  display: flex;
  align-items: center;
  height: 32px;
  margin-left: 16px;
  margin-right: 16px;
  padding-left: 0;
}

/* ---------------Table-------------------------------------------------------------- */
//#region Table CSS
table {
  width: 2180px;
  min-width: 100%;
  box-sizing: border-box;
  border-collapse: collapse;
}

tr {
  border-bottom: 1px solid #c7c7c7;
  /* box-sizing: border-box; */
}

thead tr {
  position: sticky;
  top: 0;
  z-index: 1;

  th {
    position: relative;
  }
  .border-right {
    border-right: 1px solid #c7c7c7;
  }
  .border-left {
    border-left: 1px solid #c7c7c7;
  }
  .checkbox,
  .Function {
    top: 0;
    z-index: 2;
  }
}
tbody tr:hover td {
  background: #f3f8f8;
}
tbody .selected td {
  background: #f9f9f9;
}
th:not(:last-child),
td:not(:last-child) {
  border-right: 1px dotted #c7c7c7;
}

td {
  padding: 8px;
  height: 48px;
  box-sizing: border-box;
}
th {
  background: #eceef1 !important;
  height: 34px;
  box-sizing: border-box;
}

.checkbox {
  padding: 0;
  width: 40px;
  position: sticky;
  left: 0;
  background: #fff;
}
.EmployeeCode {
  width: 150px;
}
.FullName {
  width: 250px;
}
.Gender {
  width: 120px;
}
.DateOfBirth {
  width: 150px;
}
.IdentityNo {
  width: 200px;
}
.Position {
  width: 250px;
}
.DepartmentName {
  width: 250px;
}
.AccountNumber {
  width: 150px;
}
.BankName {
  width: 250px;
}
.BankBranch {
  width: 250px;
}
.Function {
  padding: 0;
  width: 120px;
  position: sticky;
  right: 0;
  background: #fff;
}
.border-left {
  position: absolute;
  left: -1px;
  top: 0;
  bottom: 0;
  border-left: 1px dotted #c7c7c7;
  box-sizing: border-box;
}
.border-right {
  position: absolute;
  right: -1px;
  top: 0;
  bottom: 0;
  border-right: 1px dotted #c7c7c7;
  box-sizing: border-box;
}
.function-content {
  height: 100%;
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}
//#endregion

/* -----------Các button, active, hover,input---------------------------- */
input {
  border: 1px solid #babec5;
  border-radius: 4px;
  width: 100%;
  height: 32px;
  padding-left: 16px;
  padding-right: 32px;
  box-sizing: border-box;
  &:hover {
    box-shadow: 0 0 5pt #d3d3d3;
  }
  &:focus {
    outline-color: #2ca01c;
  }
}

input::placeholder {
  color: #babec5;
  font-family: notosans-italic;
  font-size: 12px;
}

input[type="checkbox"] {
  display: none;
}
input[type="checkbox"] + label {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 16px;
  height: 16px;
  border: 1px solid #afafaf;
  border-radius: 2px;
  cursor: pointer;
  margin: auto;
  background: #fff;
  &:hover {
    box-shadow: 0 0 5pt #d3d3d3;
  }
}

input[type="checkbox"]:checked + label .custom-checkbox {
  background-position: -899px -316px;
  width: 11px;
  height: 8px;
}

//Paginate
.disabled {
  cursor: none;
  color: #9e9e9e;
}
.page-prev {
  margin-right: 16px;
}
.page-next {
  margin-left: 16px;
}
.page-index {
  display: flex;
  align-items: center;
}
.page-number {
  font-weight: 700;
  margin-left: 4px;
  margin-right: 4px;
}
.page-link {
  width: 21px;
  height: 21px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.input-search {
  position: absolute;
  display: flex;
  align-items: center;
  right: 60px;
  height: 32px;
  width: 230px;
}
.active {
  border: 1px solid #c7c7c7;
  border-radius: 2px;
}
.btn {
  padding-left: 24px;
  padding-right: 24px;
  height: 36px;
  display: flex;
  align-items: center;
  border-radius: 4px;
  cursor: pointer;
}
.btn-toggle-select {
  padding-left: 15px;
  padding-right: 15px;
  margin-left: 8px;
  height: 20px;
}
.btn-add {
  color: #fff;
  background: #2ca01c;
  margin-left: auto;
}
.btn-add:hover {
  background-color: #35bf22;
}
.btn-edit {
  margin-left: 20px;
  color: #0075c0;
  font-family: notosans-semibold;
  cursor: pointer;
}
.icon-load {
  position: absolute;
  right: 20px;
  background-position: -425px -201px;
  width: 20px;
  height: 23px;
  cursor: pointer;
  &:hover {
    background-position: -1098px -90px;
  }
}

.icon-search {
  background-position: -992px -360px;
  width: 16px;
  height: 16px;
  position: absolute;
  right: 8px;
}
.icon-toggle-select {
  background-position: -900px -365px;
  width: 8px;
  height: 5px;
}

/* loading CSS */
.loading {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: 2000;
}
.modal {
  background: #000;
  opacity: 0.2;
  width: 100%;
  height: 100%;
}
.lds-dual-ring {
  position: absolute;
  top: 40%;
  left: 50%;
  bottom: 50%;
  transform: translate(-50%, -50%);
  display: inline-block;
  width: 80px;
  height: 80px;
}
.lds-dual-ring:after {
  content: "";
  display: block;
  width: 64px;
  height: 64px;
  margin: 8px;
  border-radius: 50%;
  border: 6px solid #fff;
  border-color: #019160 transparent #019160 transparent;
  animation: lds-dual-ring 1.2s linear infinite;
}
@keyframes lds-dual-ring {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}
.displayNone {
  display: none !important;
}
</style>