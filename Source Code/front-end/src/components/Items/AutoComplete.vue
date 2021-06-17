<template>
  <div
    class="autocomplete"
    v-click-outside="OnClickOutSide"
    v-on:keyup.down="KeyDownOnPress"
    v-on:keyup.enter="EnterOnPress"
    v-on:keyup.up="KeyUpOnPress"
    v-on:keydown.tab="HideOptions"
  >
    <div class="select-label">
      <input
        class="label-value"
        v-model="label_value"
        @click="FilterData"
        @input="FilterData"
        ref="focusInput"
        :class="validate_class"
      />
      <div class="icon-item" @click="ToggleOnClick">
        <div class="icon-toggle"></div>
      </div>
    </div>
    <div class="select-options" v-if="isShow">
      <div
        ref="option"
        v-for="(item, index) in data_filter"
        :key="index"
        class="option"
        @click="OptionOnClick(item)"
        :class="{
          'is-selecting': index == index_Selecting,
          selected: option_selected == item[value_key],
        }"
        :id="index"
      >
        {{ item[label_key] }}
      </div>
      <div class="empty" v-if="is_empty">Không có dữ liệu hiển thị</div>
    </div>
  </div>
</template>

<script>
import vClickOutside from "v-click-outside";
export default {
  directives: {
    clickOutside: vClickOutside.directive,
  },
  name: "autocomplete",
  props: {
    /**
     * Value
     */
    model_value: {
      default: null,
    },
    // Key (propertype) của Value muốn lấy
    value_key: {
      type: String,
      default: null,
    },

    // Key (propertype) của Text muốn hiển thị
    label_key: {
      type: String,
      default: null,
    },

    // Mảng các options
    options: {
      type: Array,
      default: null,
    },
    validate_class:{
      default:"",
    }
  },
  watch: {
    // options(){
    //   this.GetDefaultValue();
    // },
    // model_value(){
    //   this.GetDefaultValue();
    // }
  },
  data() {
    return {
      option_selected: null, // Option nào đang được chọn
      isShow: false, // Ẩn hiện Options
      index_Selecting: -1, // Chỉ số (STT) của thằng đang được chọn
      label_value: null, // Label Text value (Giá trị hiển thị của thằng đang được chọn)
      data_filter: [], // Mảng tìm kiếm
      is_empty: false,
    };
  },
  // watch:{
  //   model_value(){
  //     // debugger // eslint-disable-line no-debugger
  //     console.log(this.model_value);
  //     this.GetDefaultValue();
  //   }
  // },
  methods: {
    //#region 1. Xử lý các sự kiện
    /**
     * Sự kiện click vào chọn Option
     * Created By: NTHIEU (07/06/2021)
     */
    OptionOnClick(val) {
      this.SetValue(val);
      this.HideOptions();
    },
    /**
     * Sự kiện Press vào phím mũi tên đi xuống chọn Option
     * Created By: NTHIEU (07/06/2021)
     */
    KeyDownOnPress() {
      this.index_Selecting++;
      if (this.index_Selecting > this.data_filter.length - 1) {
        this.index_Selecting = 0;
      }
      this.isShow = true;
    },
    /**
     * Sự kiện Click ra ngoài
     * Created By: NTHIEU (07/06/2021)
     */
    OnClickOutSide() {
      // debugger // eslint-disable-line no-debugger
      this.HideOptions();
    },

    /**
     * Sự kiện Click vào nút mũi tên trong autocomplete
     * Created By: NTHIEU (07/06/2021)
     */
    ToggleOnClick() {
      this.data_filter = this.options;
      this.isShow = !this.isShow;
      this.$refs.focusInput.focus();
      this.is_empty = false;
    },

    /**
     * Sự kiện Press vào Enter
     * Created By: NTHIEU (07/06/2021)
     */
    EnterOnPress() {
      // debugger // eslint-disable-line no-debugger
      if (this.index_Selecting == -1) {
        this.GetDefaultValue();
      } else {
        this.SetValue(this.data_filter[this.index_Selecting]);
      }

      this.HideOptions();
    },

    /**
     * Sự kiện Press mũi tên đi lên chọn Option
     * Created By: NTHIEU (07/06/2021)
     */
    KeyUpOnPress() {
      this.index_Selecting--;
      if (this.index_Selecting < 0) {
        this.index_Selecting = this.data_filter.length - 1;
      }

      this.isShow = true;
    },
    //#endregion

    //#region 2. Các method, Function
    /**
     * Xử lý thay đổi giá trị value
     * Created By: NTHIEU (07/06/2021)
     */
    SetValue(val) {
      if (val != null) {
        this.$emit("update:model_value", this.GetValue(val));
        this.label_value = val[this.label_key];
        this.option_selected = val[this.value_key];
      }
    },

    /**
     * Khởi tạo lấy giá trị mặc định được truyền vào
     * Created By: NTHIEU (07/06/2021)
     */
    GetDefaultValue() {
      // debugger // eslint-disable-line no-debugger

      //1. Gán giá trị mặc định của mảng
      this.data_filter = this.options;

      //2. Lấy giá trị mặc định
      if (this.data_filter != null)
        this.data_filter.forEach((option) => {
          if (option[this.value_key] == this.model_value) {
            this.label_value = option[this.label_key];
            this.option_selected = option[this.value_key];
          }
        });
      // debugger // eslint-disable-line no-debugger
    },

    /**
     * Function lấy Text
     * Created By: NTHIEU (10/06/2021)
     */
    GetText() {
      return this.label_value;
    },

    /**
     * Function lấy Value
     * Created By: NTHIEU (10/06/2021)
     */
    GetValue(val) {
      return val[this.value_key];
    },

    /**
     * Hiển thị Options
     * Created By: NTHIEU (10/06/2021)
     */
    ShowOptions() {
      this.isShow = true;
    },

    /**
     * Ẩn các Options
     * Created By: NTHIEU (10/06/2021)
     */
    HideOptions() {
      // debugger // eslint-disable-line no-debugger
      var is_availble = false;
      this.options.forEach((option) => {
        if (option[this.label_key] == this.label_value) {
          is_availble = true;
        }
      });
      if (this.label_value !=null && this.label_value != "" && is_availble == false) {
        this.$emit("update:model_value", "not_availble")// eslint-disable-line no-debugger
      }
      else if (this.label_value == null || this.label_value == ""){
        this.$emit("update:model_value", null)
      }
      this.isShow = false;
      this.index_Selecting = -1;
    },

    /**
     * Hàm tìm kiếm
     * Created By: NTHIEU (10/06/2021)
     */
    FilterData() {
      //1. Phần tử được chọn đầu tiên
      this.index_Selecting = 0;
      //2. Hiển thị Options
      this.ShowOptions();
      //3. Nếu Trống Thì hiển thị tất cả
      if (this.label_value != null) {
        if (this.label_value.length == 0) {
          this.data_filter = this.options;
        } else {
          this.data_filter = this.options.filter(
            (e) =>
              e[this.label_key]
                .toLowerCase()
                .indexOf(this.label_value.toLowerCase()) != -1
          );
        }
      } else {
        this.data_filter = this.options;
      }
      if (this.data_filter.length == 0) {
        this.is_empty = true;
      } else this.is_empty = false;
    },
    //#endregion

    Test() {
      alert(1);
    },
  },
  created() {
    this.GetDefaultValue();
  },
  mounted() {
    // this.GetDefaultValue();
    // console.log(this.model_value);
  },
};
</script>

<style lang="scss" scoped>
/********************
* Declare
**********************/

// chiều dài của thẻ:
$width: 100%;
// Chiều cao của thẻ:
$height: 32px;
// Background Default:
$background-default: #ffffff;
// Background Hover:
$background-hover: #f4f5f6;
// Màu chữ default:
$color-default: #000000;
// Màu chữ hover:
$color-hover: #2ca01c;
// border chung
$border: 1px solid #babec5;
// border khi focus vào
$border-focus: 1px solid #2ca01c;
// Icon toggle:
$icon-toggle: url("");

/**************************************
* Mixin Method
***************************************/
// Thuộc tính icon toggle cho thẻ

@mixin Icon {
  background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -564px -365px;
  width: 8px;
  height: 5px;
}

// Size Đồng bộ chung
@mixin common-size {
  width: $width;
  height: $height;
}
// Size
@mixin Size($w, $h) {
  width: $w;
  height: $h;
}

// Flex Box (Center, Center)
@mixin Flex-Center {
  display: flex;
  align-items: center;
  justify-content: center;
}

/*************************
* autocomplete CSS
***************************/
.autocomplete {
  @include common-size;
  position: relative;
  display: flex;
  align-items: center;
  box-sizing: border-box;

  /* ------- Select_label----------- */
  .select-label {
    position: absolute;
    @include Size(100%, 100%);
    @include Flex-Center;

    .label-value {
      position: absolute;
      display: flex;
      align-items: center;
      padding-left: 8px;
      box-sizing: border-box;
      left: 0;
      color: $color-default;
      @include Size(100%, 100%);
      border-radius: 4px;
      border: $border;
      &:focus {
        outline: none;
        border: $border-focus;
      }
    }
    .icon-item {
      position: absolute;
      right: 1px;
      top: 1px;
      height: 30px !important;
      border-top-right-radius: 3px;
      border-bottom-right-radius: 3px;
      @include Flex-Center;
      @include Size(30px, 100%);
      &:hover {
        background: #e0e0e0;
      }
      .icon-toggle {
        @include Icon;
      }
    }
  }

  /* -------- Select_options---------- */
  .select-options {
    position: absolute;
    @include Size(100%, auto);
    top: $height + 2px;
    left: -1px;
    background: $background-default;
    border-radius: 4px;
    z-index: 10;
    // max-height: 250px;
    // overflow-y: auto;
    border: $border-focus;
    // box-sizing: border-box;
    /* -------- option ---------- */
    .option {
      @include Size(100%, $height);
      @include Flex-Center;
      border-radius: 3px;
      color: $color-default;
      &:hover {
        background: $background-hover;
        color: $color-hover;
      }
    }
    // Thằng nào đang được chọn
    .is-selecting {
      background: $background-hover;
      color: $color-hover;
    }
    .empty {
      @include Size(100%, $height);
      @include Flex-Center;
      background: #f7f7f7;
      border-radius: 3px;
    }
  }
}

.selected {
  background-color: #2ca01c !important;
  color: #fff !important;
}

.validate{
  border-color: red !important;
}
</style>