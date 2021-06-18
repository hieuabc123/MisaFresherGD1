<template>
  <div
    class="combobox"
    v-click-outside="OnClickOutSide"
    v-on:keyup.down="KeyDownOnPress"
    v-on:keyup.enter="EnterOnPress"
    v-on:keyup.up="KeyUpOnPress"
    v-on:keydown.tab="HideOptions"
    tabindex="0"
  >
    <div class="select-label" @click="ToggleOnClick">
      <input
        class="label-value"
        v-model="label_value"
        disabled
      />

      <div class="icon-item">
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
    </div>
  </div>
</template>

<script>
import vClickOutside from "v-click-outside";
export default {
  directives: {
    clickOutside: vClickOutside.directive,
  },
  name: "combobox",
  props: {
    model_value:{
      default: null
    },
    value_key: {
      type: String,
      default: null,
    },
    label_key: {
      type: String,
      default: null,
    },

    options: {
      type: Array,
      default: null,
    },
  },
  watch:{
    model_value(){
      this.eventHandle();
    }
  },
  data() {
    return {
      is_active: false,
      option_selected: null,
      isShow: false, // Ẩn hiện Options
      index_Selecting: -1, // Chỉ số (STT) của thằng đang được chọn
      label_value: null, // Label Text value (Giá trị hiển thị của thằng đang được chọn)
      data_filter: [],
    };
  },
  methods: {
    Test() {
      alert(1);
    },
    OnClickOutSide() {
      // debugger // eslint-disable-line no-debugger
      this.HideOptions();
    },
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
    ToggleOnClick() {
      this.data_filter = this.options;
      this.isShow = !this.isShow;
    },
    /**
     * Sự kiện Press vào Enter
     * Created By: NTHIEU (07/06/2021)
     */
    EnterOnPress() {
      this.SetValue(this.data_filter[this.index_Selecting]);
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
      this.$emit("update:model_value", this.GetValue(val));
      this.label_value = val[this.label_key];
      this.option_selected = val[this.value_key];
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
      if(this.options!=null)
      this.data_filter.forEach((option) => {
        if (option[this.value_key] == this.model_value) {
          this.label_value = option[this.label_key];
          this.option_selected = option[this.value_key];
        }
      });
    },

    GetText() {
      return this.label_value;
    },
    GetValue(val) {
      return val[this.value_key];
    },

    /**
     * Hiển thị options
     * Created By: NTHIEU (07/06/2021)
     */
    ShowOptions() {
      this.isShow = true;
    },

    /**
     * Ẩn options
     * Created By: NTHIEU (07/06/2021)
     */
    HideOptions() {
      // debugger // eslint-disable-line no-debugger
      this.isShow = false;
      this.index_Selecting = -1;
    },

    /**
     * Hàm tìm kiếm
     * Created By: NTHIEU (07/06/2021)
     */
    FilterData() {
      this.ShowOptions();
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
    },
    eventHandle(){
      this.$emit("eventHandle");
    }
    //#endregion
  },
  created() {
    this.GetDefaultValue();
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
$background-hover: #ccc;
// Màu chữ default:
$color-default: #000000;
// Màu chữ hover:
$color-hover: #000;
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
* combobox CSS
***************************/
.combobox {
  @include common-size;
  position: relative;
  display: flex;
  align-items: center;
  box-sizing: border-box;
  border-radius: 4px;
  border: $border;
  &:focus {
    outline: none;
    border: $border-focus;
  }
  

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
      border: none;
      
    }
    .icon-item {
      position: absolute;
      right: 0;
      @include Flex-Center;
      @include Size(30px, 100%);
      .icon-toggle {
        @include Icon;
      }
    }
  }

  /* -------- Select_options---------- */
  .select-options {
    position: absolute;
    @include Size(100%, auto);
    bottom: $height;
    left: -1px;
    background: $background-default;
    border-radius: 4px;
    overflow: hidden;
    border: $border;
    max-height: 500px;
    overflow-y: auto;
    // box-sizing: border-box;
    /* -------- option ---------- */
    .option {
      @include Size(100%, $height);
      @include Flex-Center;
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
  }
}

.selected {
  background-color: green !important;
  color: #fff !important;
}
</style>