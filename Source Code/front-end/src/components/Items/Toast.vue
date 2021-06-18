<template>
  <div
    class="toast hide"
    :class="{
      'show-toast': isShow,
      'hide-toast': isHide,
      alert: toastMode == 'alert',
      success: toastMode == 'success',
    }"
  >
    <div class="toast-content">
      <div class="mi icon icon-item"></div>
      <div class="message">{{ message }}</div>
    </div>
    <div class="mi toast-close X" @click="closeToast"></div>
  </div>
</template>

<script>
export default {
  name: "Toast",
  props: {
    message: {
      type: String,
      default: "ándakjdshlashfj",
    },
    isShow: {
      type: Boolean,
      default: false,
    },
    isHide: {
      type: Boolean,
      default: false,
    },
    toastMode: {
      type: String,
      default: "success",
    },
  },
  methods: {
    closeToast() {
      this.$emit("p_closeToast");
    },
  },
};
</script>

<style scoped>
.toast {
  position: absolute;
  top: 10px;
  width: 250px;
  min-height: 48px;
  display: flex;
  align-items: center;
  box-shadow: 0px 3px 6px;
  background-color: #ffffff;
  border-radius: 4px;
}

.icon-item {
  width: 24px;
  height: 24px;
  margin-left: 16px;
  margin-right: 0;
}
.alert .icon {
  background-position: -593px -145px;
	width: 22px;
	height: 23px;
}

.success .icon {
  background-position: -1602px -258px;
	width: 20px;
	height: 20px;
}

.toast-close {
  position: absolute;
  right: 16px;
  background-position: -84px -316px;
	width: 8px;
	height: 8px;
  cursor: pointer;
}


.toast-content {
  display: flex;
  align-items: center;
}

.toast-content .message {
  position: absolute;
  left: 50px;
  right: 40px;
}
.hide {
  display: none;
}
.show-toast {
  animation: showToast 0.5s ease-out 1;
  right: 16px;
  display: flex;
}

.hide-toast {
  animation: hideToast 1s ease-out 1;
  right: -255px;
  display: flex;
}

@keyframes showToast {
  from {
    right: -255px;
  }
  to {
    right: 16px;
  }
}
@keyframes hideToast {
  from {
    right: 16px;
  }
  to {
    right: -255px;
  }
}
</style>