<template>
  <div class="employee-list">
    <div class="loading" :class="{ displayNone: EmployeeList.isDone }">
      <div class="modal"></div>
      <div class="lds-dual-ring"></div>
    </div>
    <div class="title-distance">
      <div class="title">Nhân viên</div>
      <div class="btn btn-add" @click="btnAddOnClick">Thêm mới nhân viên</div>
    </div>
    <!-- #region I. Grid -->
    <div class="grid">
      <!-- #region 1. Grid-header -->
      <div class="grid-header">
        <div class="mi icon-load" @click="loadGridContent" content="Lấy lại dữ liệu"
            v-tippy="{ placement: 'bottom' }"></div>
        <div class="mi icon-excel pointer" @click="getExportData" content="Xuất file Excel"
            v-tippy="{ placement: 'left' }"></div>
        <div class="input-search">
          <input
            type="text"
            placeholder="Tìm kiếm theo mã, tên nhân viên"
            v-model="EmployeeList.filter"
            @input="onChangeInputFilter"
          />
          <div class="mi icon-search pointer" @click="loadGridContent"></div>
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
                <input type="checkbox" id="all" v-model="checkbox_all" />
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
                selected:
                  EmployeeList.list_id_selected[index] == employee.employeeId,
              }"
              @click.exact="trOnClick(employee.employeeId, index)"
              @dblclick.exact="btnEditOnClick(employee.employeeId)"
              @click.ctrl.exact="multiTrOnClick(employee.employeeId, index)"
              @click.right.exact.prevent="
                trOnRightClick(
                  index,
                  FunctionDropdown.index_selecting,
                  employee.employeeId,
                  employee.employeeCode,
                  $event
                )
              "
            >
              <!-- Check Box -->
              <td class="checkbox">
                <input
                  type="checkbox"
                  :id="employee.employeeId"
                  v-model="EmployeeList.checkbox_list[index]"
                />
                <label :for="employee.employeeId">
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
                {{ frontEndDateFormat(employee.dateOfBirth) }}
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
                  <div
                    class="btn-edit pointer"
                    @click="btnEditOnClick(employee.employeeId)"
                  >
                    Sửa
                  </div>
                  <div
                    class="btn btn-toggle-select pointer"
                    @click.stop.prevent="
                      onClickToggleButton(
                        index,
                        FunctionDropdown.index_selecting,
                        employee.employeeId,
                        employee.employeeCode
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
        <div class="no-data" v-if="EmployeeList.employees == null">
          <div class="img"></div>
          <span class="text-warn">không có dữ liệu</span>
        </div>
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
            <Combobox
              :options="[
                {
                  value: 10,
                  text: `10 bản ghi trên 1 trang`,
                },
                {
                  value: 20,
                  text: `20 bản ghi trên 1 trang`,
                },
                {
                  value: 30,
                  text: `30 bản ghi trên 1 trang`,
                },
                {
                  value: 40,
                  text: `40 bản ghi trên 1 trang`,
                },
                {
                  value: 50,
                  text: `50 bản ghi trên 1 trang`,
                },
              ]"
              :value_key="'value'"
              :label_key="'text'"
              :model_value.sync="EmployeeList.pageSize"
              @eventHandle="pageSizeOnClick"
            />
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
      :right="FunctionDropdown.right"
      :top="FunctionDropdown.top"
      @onClickOutside="onClickOutside"
      @duplicateOnClick="btnDuplicateOnClick"
      :is_click_out_side="FunctionDropdown.isClickOutSide"
      :id="EmployeeList.tr_selected_id"
      @openPopup="
        openPopup(
          'delete',
          `Bạn có muốn xóa nhân viên có mã: ${EmployeeList.tr_selected_code} không ?`
        )
      "
      :count_tr_selected="EmployeeList.count_tr_selected"
    />
    <EmployeeDetail
      :is_open.sync="EmployeeDetail.isOpen"
      :p_employee="EmployeeList.selecting_employee"
      :p_employee_before="EmployeeList.employeeCopy"
      v-if="EmployeeDetail.isOpen"
      :departments="EmployeeDetail.departments"
      :p_form_mode.sync="EmployeeDetail.form_mode"
      @openPopup="openPopup"
      @loadComponent="loadGridContent"
      @btnAddOnClick="btnAddOnClick"
      @openToast="openToast"
    />
    <Toast
      :isShow="toast.isShow"
      :isHide="toast.isHide"
      :toastMode="toast.mode"
      :message="toast.message"
    />

    <Popup
      :p_isOpen="popup.isOpen"
      :p_message="popup.message"
      :p_mode="popup.mode"
      @p_deleteEmployee="deleteEmployee(EmployeeList.tr_selected_id)"
      @p_closePopup="closePopup"
    />
  </div>
</template>

<script>
import Paginate from "vuejs-paginate";
import moment from "moment";
import axios from "axios";
import FunctionDropdown from "./FunctionDropdown.vue";
import vClickOutside from "v-click-outside";
import EmployeeDetail from "./EmployeeDetail.vue";
import Combobox from "../../Items/Combobox.vue";
import { baseUrl } from "../../../config/dev.env";
import Popup from "../../Items/Popup.vue";
import Toast from "../../Items/Toast.vue";
export default {
  name: "EmployeeList",
  directives: {
    clickOutside: vClickOutside.directive,
  },
  components: {
    Paginate,
    FunctionDropdown,
    EmployeeDetail,
    Combobox,
    Popup,
    Toast,
  },
  data() {
    return {
      checkbox_all: false,
      time_out: null,
      //1. Đối tượng EmployeeList, đối tượng nhân viên cho component
      EmployeeList: {
        employees: [], // danh sách các nhân viên
        selecting_employee: {}, // nhân viên đang được chọn
        employeeCopy: {},
        checkbox_list: [], // list danh sách check box
        isDone: true, // Kiểm tra load dữ liệu xong chưa
        pageCount: 1, // Tổng số trang
        pageInt: 1, // Số thứ tự của trang
        pageSize: 30, // Số bản ghi trên trang
        filter: "", // Giá trị cần tìm kiếm (filter)
        tr_selected_id: null, // Dòng nào đang được chọn (lấy id) or nhân viên có id đang được chọn
        list_id_selected: [], // list số dòng đang được chọn
        total_employees: 0, // đếm tổng số bản ghi
        tr_selected_code: null, // Mã của nhân viên đang được chọn , Mã của nhân viên tương ứng với dòng đang được chọn
        count_tr_selected:0, //đếm dòng tr được chọn
      },
      //2. Đối tượng Component Function Dropdown (ở mục chức năng)
      FunctionDropdown: {
        isOpen: false, // Kiểm tra mở đóng
        right: 0, // vị trí căn lề bên trái
        top: 0, // Vị trí căn lề bên phải
        index_selecting: null, // giá trị trước khi click vào 1 đối tượng click toggle, (giá trị này sẽ được thay đổi sau khi click)
        isClickOutSide: false,
      },

      //3. Đối tượng Employee Detail (Chi tiết nhân viên)
      EmployeeDetail: {
        isOpen: false, // Kiểm tra mở đóng
        form_mode: "",
        departments: [],
      },

      //4. Đối tượng popup
      popup: {
        isOpen: false,
        message: "Warning",
        mode: "question",
      },

      //5. Đối tượng toast
      toast: {
        isShow: false,
        isHide: false,
        mode: "",
        message: "",
      },
    };
  },
  watch: {
    checkbox_all() {
      // debugger; // eslint-disable-line no-debugger
      if (this.checkbox_all == true) {
        for (let i = 0; i < this.EmployeeList.checkbox_list.length; i++) {
          this.EmployeeList.checkbox_list[i] = true;
        }
      } else {
        for (let i = 0; i < this.EmployeeList.checkbox_list.length; i++) {
          this.EmployeeList.checkbox_list[i] = false;
        }
      }
    },
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
    onClickToggleButton(index, index_selecting, id, code) {
      //0. Báo cho dropdown tự custom đây không phải là click out side
      this.FunctionDropdown.isClickOutSide = false;
      //0. Lấy Id của dòng đang chọn
      this.EmployeeList.tr_selected_id = id;
      //0. Lấy tên của employee đang được chọn
      this.EmployeeList.tr_selected_code = code;
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
      //3.3 Lấy vị trí top và right
      var right = 170;
      var top = position.getBoundingClientRect().top - 48 + 20;
      this.FunctionDropdown.right = right;
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

    //#region 2. Xử lý Sự kiện input tìm kiếm và nút tìm kiếm
    /**
     * Sự kiện Khi nhập liệu input tìm kiếm
     * Created By: NTHIEU (17/06/2021)
     */
    onChangeInputFilter() {
      this.EmployeeList.pageInt = 1;
      clearTimeout(this.time_out);
      this.time_out = setTimeout(this.loadGridContent, 500);
    },

    //#endregion 2

    //#region 3 Sự kiện click vào từng dòng trong table

    /**
     * Sự kiện click vào từng dòng
     * Created By: NTHIEU (19/06/2021)
     */
    trOnClick(id, index) {
      this.EmployeeList.count_tr_selected = 1;
      this.EmployeeList.list_id_selected = [];
      this.EmployeeList.list_id_selected[this.EmployeeList.pageSize] = null;
      this.EmployeeList.tr_selected_id = id;
      this.EmployeeList.list_id_selected[index] = id;
    },

    /**
     * Sự kiện khi click vào nhiều dòng (CTRL + CLICK)
     * Created By: NTHIEU (19/06/2021)
     */
    multiTrOnClick(id, index) {
      // this.EmployeeList.list_id_selected = [];
      this.count_tr_selected +=1 ;
      this.EmployeeList.list_id_selected.splice(index, 1, id);
      console.log(index);
    },

    /**
     * Sự kiện chuột phải vào từng dòng để hiển thị property
     * Created By: NTHIEU (19/06/2021)
     */
    trOnRightClick(index, index_selecting, id, code, event) {
      //0. Báo cho dropdown tự custom đây không phải là click out side
      this.FunctionDropdown.isClickOutSide = false;
      //0. Lấy Id của dòng đang chọn
      this.EmployeeList.tr_selected_id = id;
      //0. Lấy tên của employee đang được chọn
      this.EmployeeList.tr_selected_code = code;
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
      //3 Lấy vị trí top và right của con trỏ chuột và gán cho drop down
      var right = screen.width - event.clientX;
      console.log(right);
      // debugger; // eslint-disable-line no-debugger
      var top = event.clientY - 30;
      this.FunctionDropdown.right = right -10;
      this.FunctionDropdown.top = top;
      //3.4 Kiểm tra nếu vị trí ở bên dưới quá thì đổi vị trí cho thằng dropdown lên trên
      if (top > 600) {
        this.FunctionDropdown.top -= 115;
      }
    },
    //#endregion 3

    //#region 4 Sự kiện click vào paginate (phân trang)
    /**
     * Click vào pageNumber (Phân trang)
     * Created By: NTHIEU (15/06/2021)
     **********************************/
    pageNumberOnClick(page) {
      this.EmployeeList.pageInt = page;
      this.loadGridContent();
    },

    /**
     * Click vào page Size (chọn số bản ghi / trang) (Phân trang)
     * Created By: NTHIEU (16/06/2021)
     **********************************/
    pageSizeOnClick() {
      this.EmployeeList.pageInt = 1;
      this.loadGridContent();
    },
    //#endregion 4

    //#region 5 Xử lý sự kiện Thêm mới, sửa, nhân bản nhân viên và $emit dialog nhân viên
    /**
     * Sự kiện click vào nút thêm mới nhân viên
     * Created By :NTHIEU (16/06/2021)
     */
    async btnAddOnClick() {
      //1. Lấy dữ liệu nhân viên mới
      await this.getNewEmployee();
      //2. Mở form dialog với form mode là thêm mới (Add)
      this.openDialogEmployeeDetail("add");
    },

    /**
     * Sự kiện click vào nút SỬA
     * Created By: NTHIEU (16/06/2021)
     */
    async btnEditOnClick(id) {
      //1. Lấy dữ liệu nhân viên hiện tại theo Id
      await this.getEmployeeById(id);
      //2. Mở form dialog với form mode là Sửa
      this.openDialogEmployeeDetail("edit");
    },

    async btnDuplicateOnClick(id) {
      //1. Lấy dữ liệu nhân bản nhân viên theo Id
      await this.getDuplicateEmployee(id);
      //2. Mở form dialog với form mode là thêm mới
      this.openDialogEmployeeDetail("add");
    },
    /**
     * - Mở form dialog Nhân viên
     * Created By: NTHIEU (16/06/2021)
     */
    async openDialogEmployeeDetail(formMode) {
      await this.loadDepartments();
      this.EmployeeDetail.form_mode = formMode;
      Object.assign(
        this.EmployeeList.employeeCopy,
        this.EmployeeList.selecting_employee
      );
      this.EmployeeDetail.isOpen = true;
    },

    //#endregion 5

    //#region 6 Xử lý sự kiện Popup
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
    //#endregion 6

    //#region 7 Xủ lý sự kiện Toast
    /**
     * Toast Notification
     * Created By Nguyễn Trung Hiếu
     * Created Date 07/4/2021
     */
    openToast(mode, message) {
      this.showToast();
      setTimeout(this.hideToast, 2000);
      this.toast.message = message;
      this.toast.mode = mode;
    },
    showToast() {
      this.toast.isShow = true;
      this.toast.isHide = false;
    },
    hideToast() {
      this.toast.isShow = false;
      this.toast.isHide = true;
    },
    //#endregion 7

    //#endregion Các sự kiện

    //#region Method
    /**
     * Load Grid Content
     * CreatedBy: Nguyễn Trung Hiếu (09/5/2021)
     ********************************************************/
    async loadGridContent() {
      this.EmployeeList.checkbox_list[this.EmployeeList.pageSize] = false;
      this.EmployeeList.list_id_selected[this.EmployeeList.pageSize] = null;
      await this.loadDataPagingFilter(); //dữ liệu Phân trang
    },

    /**
     * Load dữ liệu tìm kiếm phân trang (Pagination)
     * CreatedBy: Nguyễn Trung Hiếu (09/5/2021)
     ********************************************************/
    async loadDataPagingFilter() {
      this.EmployeeList.isDone = false;
      var p_pageInt = this.EmployeeList.pageInt.toString();
      var p_pageSize = this.EmployeeList.pageSize.toString();
      var p_filter = this.EmployeeList.filter.toString();
      var url =
        `${baseUrl}/Employees/filter?pageint=` +
        p_pageInt +
        "&pagesize=" +
        p_pageSize +
        "&datafilter=" +
        p_filter;
      await axios
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
        .catch((error) => {
          console.log(error);
        });
    },

    /**
     * Lấy dữ liệu nhân viên theo Id
     * Created By: NTHIEU (16/06/2021)
     */
    async getEmployeeById(id) {
      await axios
        .get(`${baseUrl}/Employees/` + id)
        .then((res) => {
          // debugger;// eslint-disable-line no-debugger
          this.EmployeeList.selecting_employee = res.data;
          this.formatDate();
          console.log(this.EmployeeList.selecting_employee);
        })
        .catch((error) => {
          console.log(error);
        });
    },

    /**
     * Lấy dữ liệu tạo mới nhân viên
     * Created By: NTHIEU (16/06/2021)
     */
    async getNewEmployee() {
      await axios
        .get(`${baseUrl}/Employees/new`)
        .then((res) => {
          this.EmployeeList.selecting_employee = res.data.data;
          console.log(this.EmployeeList.selecting_employee);
        })
        .catch((error) => {
          console.log(error);
        });
    },

    /**
     * Lấy dữ liệu nhân bản nhân viên
     * Created By: NTHIEU (16/06/2021)
     */
    async getDuplicateEmployee(id) {
      await axios
        .get(`http://localhost:60651/api/v1/Employees/duplicate/${id}`)
        .then((res) => {
          this.EmployeeList.selecting_employee = res.data.data;
          this.formatDate();
        })
        .catch((error) => {
          console.log("getDuplicateEmployee Error:" + error);
        });
    },

    /**
     * Xóa dữ liệu nhân viên
     * Created By: NTHIEU (16/06/2021)
     */
    async deleteEmployee(id) {
      await axios
        .delete(`http://localhost:60651/api/v1/Employees/${id}`)
        .then((res) => {
          console.log(res.data);
          this.loadGridContent();
          this.openToast("alert", "Xóa thành công 1 bản ghi");
        })
        .catch((error) => {
          console.log("getDuplicateEmployee Error:" + error);
        });
    },

    /**
     * Lấy dữ liệu các phòng ban
     * Created By: NTHIEU (16/06/2021)
     */
    async loadDepartments() {
      await axios
        .get("http://localhost:60651/api/v1/Departments")
        .then((res) => {
          // debugger // eslint-disable-line no-debugger
          this.EmployeeDetail.departments = res.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },

    /**
     * Export dữ liệu ra file Excel
     * Created By: NTHIEU (18/06/2021)
     */
    async getExportData() {
      var url = baseUrl + "/Employees/export";
      window.open(url);
    },

    /****************************************************************************
     * Các fomat định dạng cho dữ liệu
     * Created By Nguyễn Trung Hiếu
     * Created Date 05/4/2021
     *****************************************************************************/
    /**
     * format định dạng ngày tháng năm hiển thị trên bảng
     */
    frontEndDateFormat(date) {
      if (date != null && date != "")
        return moment(date, "YYYY-MM-DD").format("DD/MM/YYYY");
      else return null;
    },

    /**
     * Fomat định dạng YYYY/MM/DD cho input[type = date]
     * Created By: NTHIEU (18/06/2021)
     */
    backEndDateFormat(date) {
      if (date == "" || date == null) return (date = null);
      else return moment(date, "YYYY-MM-DD").format("YYYY-MM-DD");
    },

    /**
     * hàm xử lý format date cho input employee
     * Created By: NTHIEU (18/06/2021)
     */
    formatDate() {
      this.EmployeeList.selecting_employee.dateOfBirth = this.backEndDateFormat(
        this.EmployeeList.selecting_employee.dateOfBirth
      );
      this.EmployeeList.selecting_employee.identityDate =
        this.backEndDateFormat(
          this.EmployeeList.selecting_employee.identityDate
        );
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
.employee-list {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
}
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
  left: 20px;
  right: 16px;
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
  left: 20px;
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
  z-index: 1;
  th {
    position: sticky;
    top: 0;
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

thead{
  th{
    text-align: left;
    padding-left: 8px;
  }
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
  text-align: center;
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
  text-align: center;
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
    border: 1px solid #2ca01c;
    outline: none;
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
  right: 100px;
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
  right: 60px;
  background-position: -425px -201px;
  width: 20px;
  height: 23px;
  &:hover {
    background-position: -1098px -90px;
  }
}
.icon-excel {
  position: absolute;
  right: 20px;
  background-position: -705px -202px;
  width: 23px;
  height: 20px;
  &:hover {
    background-position: -705px -258px;
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
  width: 100%;
  height: 100%;
}
.lds-dual-ring {
  display: flex;
  position: absolute;
  top: 40%;
  left: 50%;
  bottom: 50%;
  transform: translate(-50%, -50%);
  background: url("../../../assets/loading.svg") no-repeat;
  background-size: cover;
  display: inline-block;
  width: 60px;
  height: 60px;
  animation: lds-dual-ring 2s linear 0s infinite;
  -webkit-animation: lds-dual-ring 2s linear 0s infinite;
  -moz-animation: lds-dual-ring 2s linear 0s infinite;
  -o-animation: lds-dual-ring 2s linear 0s infinite;
}
@keyframes lds-dual-ring {
  from {
    -webkit-transform: rotate(0deg);
    -moz-transform: rotate(0deg);
    -o-transform: rotate(0deg);
  }
  to {
    -webkit-transform: rotate(360deg);
    -moz-transform: rotate(360deg);
    -o-transform: rotate(360deg);
  }
}
.displayNone {
  display: none !important;
}

.no-data {
  position: sticky;
  margin-top: 70px;
  width: 132px;
  left: 45%;
  display:flex;
  justify-content: center;
  flex-direction: column;
  span{
    text-align: center;
  }
  .img {
    background: url('../../../assets/img/bg_report_nodata.76e50bd8.svg') no-repeat 0 0;
    width: 132px;
    height: 76px;
    margin-bottom: 30px;
  }
}
</style>