<template>
  <div class="popup" :class="{ 'popup-open': p_isOpen }">
    <div class="modal"></div>
    <div class="popup-detail" :class="p_mode">
      <!-- <div class="mi icon-close X" @click="closePopup"></div> -->
      <div class="popup-content">
        <div class="mi icon icon-message"></div>
        <div class="message-content">
          <span>{{ p_message }}</span>
        </div>
      </div>
      <div class="popup-bottom">
        <div class="default btn-cancel" @click="closePopup">Không</div>
        <div class="btn default btn-delete" @click="btnAgreeDelete">
          <p>Có</p>
        </div>
        <div class="btn default btn-close" @click="closePopup">
          <p>Đồng ý</p>
        </div>
        <div class="btn default btn-close-warning" @click="closePopup">Đóng</div>
        <div class="default btn-cancel-close-dialog" @click="closePopup">Hủy</div>
        <div class="default btn-not-agree" @click="btnNotAgreeSave">Không</div>
        <div class="btn default btn-agree" @click="btnAgreeSave">Có</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "PopUp",
  props: {
    p_message: {
      type: String,
      default: "",
    },
    p_isOpen: {
      type: Boolean,
      default: false,
    },
    p_mode: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      toast: {
        message: "Xóa thành công",
        mode: "alert",
      },
    };
  },
  methods: {
    /**
     * Click btn agree to Delete
     * Created By: NTHIEU (17/06/2021)
     */
    btnAgreeDelete() {
      this.$emit("p_deleteEmployee");
      this.closePopup();
    },

    /**
     * Click btn don't agree save
     * Created By: NTHIEU (17/06/2021)
     */
    btnNotAgreeSave(){
      this.$emit("p_btnNotAgreeSave");
      this.closePopup();
    },

    /**
     * Click btn Agree Save
     * Created By: NTHIEU (17/06/2021)
     */
    btnAgreeSave(){
      this.$emit("p_btnAgreeSave");
      // this.closePopup();
    },
    /**
     * Close Pop up
     * Created By: NTHIEU (17/06/2021)
     */
    closePopup() {
      this.$emit("p_closePopup");
    },
  },
};
</script>

<style lang="scss" scoped>
.popup-open {
  display: flex !important;
}
.popup {
  position: fixed;
  top: 0;
  left: 0;
  z-index: 100;
  width: 100%;
  height: 100%;
  display: none;
  align-items: center;
  justify-content: center;
}
.modal {
  position: absolute;
  background: #000000;
  opacity: 0.5;
  width: 100%;
  height: 100%;
}

.popup-detail {
  position: absolute;
  background-color: #ffffff;
  border-radius: 4px;
  overflow: hidden;
  max-width: 400px;
  min-width: 300px;
  padding: 32px;
}
.popup-content {
  position: relative;
  display: flex;
  background: #ffffff;
  border-bottom: 1px solid #c7c7c7;
  padding-bottom: 32px;
}

.popup-bottom {
  position: relative;
  bottom: -10px;
  height: 60px;
  width: 100%;
  display: flex;
  align-items: center;
}

//#region Css Trạng thái Warning
.warning {
  .icon-message {
    background-position: -752px -462px;
    width: 36px;
    height: 36px;
  }
  .popup-bottom {
    justify-content: center;
    .btn {
      position: relative;
      background-color: #2ca01c;
      color: white;
      &:hover {
        background-color: #35bf22;
      }
    }
    .btn-close-warning {
      display: flex;
    }
    .icon-message {
      background-position: -752px -462px;
      width: 36px;
      height: 36px;
    }
  }
}
//#endregion

//#region CSS Trạng thái Alert
.alert {
  .btn {
    background-color: #2ca01c;
    color: white;
    &:hover {
      background-color: #35bf22;
    }
  }
  .btn-close {
    display: flex;
  }
  .icon-message {
    background-position: -598px -463px;
    width: 36px;
    height: 37px;
  }
  .btn-cancel {
    display: flex;
  }
}
//#endregion

//#region CSS trạng thái Delete
.delete {
  .btn {
    background-color: #2ca01c;
    color: white;
    &:hover {
      background-color: #35bf22;
    }
  }
  .btn-delete {
    display: flex;
    padding-left: 12px;
    padding-right: 12px;
  }
  .icon-message {
    background-position: -598px -463px;
    width: 36px;
    height: 37px;
  }
  .btn-cancel {
    display: flex;
  }
}
//#endregion

//#region CSS trạng thái Question
.question {
  width: 600px;
  .icon-message {
    background-position: -832px -462px;
    width: 36px;
    height: 36px;
  }
  .btn {
    background-color: #2ca01c;
    color: white;
    &:hover {
      background-color: #35bf22;
    }
  }
  .btn-cancel-close-dialog {
    color: #000000;
    height: 36px;
    padding-left: 20px;
    padding-right: 20px;
    display: flex;
    align-items: center;
    text-align: center;
    cursor: pointer;
    position: absolute;
    left: 0;
    box-sizing: border-box;
    border: 1px solid #8d9096;
    border-radius: 4px;
    &:hover {
      background: #d2d3d6;
    }
  }
  .btn-not-agree {
    color: #000000;
    height: 36px;
    padding-left: 20px;
    padding-right: 20px;
    display: flex;
    align-items: center;
    text-align: center;
    cursor: pointer;
    position: absolute;
    right: 70px;
    box-sizing: border-box;
    border: 1px solid #8d9096;
    border-radius: 4px;
    &:hover {
      background: #d2d3d6;
    }
  }
  .btn-agree {
    padding-right: 12px;
    padding-left: 12px;
    display: flex;
  }
}
//#endregion
.btn {
  position: absolute;
  min-width: 40px;
  right: 0;

  display: flex;
  justify-content: center;
}

.btn-cancel {
  color: #000000;
  height: 36px;
  padding-left: 20px;
  padding-right: 20px;
  display: flex;
  align-items: center;
  text-align: center;
  cursor: pointer;
  position: absolute;
  left: 0;
  box-sizing: border-box;
  border: 1px solid #8d9096;
  border-radius: 4px;
  &:hover {
    background: #d2d3d6;
  }
}
span {
  width: calc(100% - 40px);
}

.icon-close {
  background-position: -147px -147px;
  width: 18px;
  height: 18px;
  position: absolute;
  top: 8px;
  right: 8px;
  cursor: pointer;
}
.message-content {
  position: relative;
  display: flex;
  align-items: center;
  max-width: 350px;
  left: 10px;
  margin-left: 10px;
}
.default {
  display: none;
}
.icon {
  position: relative;
}

</style>