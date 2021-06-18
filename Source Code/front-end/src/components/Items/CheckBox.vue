<template>
  <!-- #region checkbox -->
  <div style="display: inline-block">
    <input type="checkbox" class="checkbox" :id="id" />
    <label :for="id"></label>
  </div>
  <!-- #endregion -->
</template>

<script>
//#region export
export default {
  //#region data
  data() {
    return {
      id: Math.random(),
    };
  },
  //#endregion
};
//#endregion
</script>
<style lang="scss" scoped>
@mixin icon-default {
    background-image: url("../../assets/img/Sprites.64af8f61.svg");
    background-repeat: no-repeat;
    display: inline-block;
}

@mixin icon-bind($position, $size: $icon-size) {
    background-position: $position;
    height: $size;
    width: $size;
}
.checkbox {
    display: none;

    & + label {
        border-radius: 3px;
        display: inline-block;
        position: relative;
        border: 1px solid #afafaf;
        height: 18px;
        width: 18px;
    }

    &:checked + label {
        animation-name: rotateIn;
        animation-duration: 0.3s;
        border: 1px solid #2ca01c;

        &::after {
            @include icon-default();
            @include icon-bind(-1224px -360px, 16px);
            content: "";
            font-size: 14px;
            position: absolute;
            top: 0;
            left: 0;
        }
    }
}
</style>