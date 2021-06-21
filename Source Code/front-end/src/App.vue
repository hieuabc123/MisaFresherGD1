<template>
  <div id="app">
    <the-navbar></the-navbar>
    <div class="layout-left">
      <the-content></the-content>
      <the-header></the-header>
    </div>
  </div>
</template>

<script>
import TheContent from "./components/Layouts/TheContent.vue";
import TheHeader from "./components/Layouts/TheHeader.vue";
import TheNavbar from "./components/Layouts/TheNavbar.vue";
import './components/Common/CSS/common.scss'
import './components/Common/CSS/icons.scss'

export default {
  name: "App",
  components: {
    TheNavbar,
    TheContent,
    TheHeader,
  },
};
</script>
<style lang="scss">

#app {
  display: flex;
  flex-wrap: nowrap;
  height: 100vh;
  overflow: hidden;
}
.layout-left{
  flex-grow: 1;
  background: #ccc;
  position: relative;
  height: 100vh;
  // transform: translate3d(0,0,0);
	// transition: transform .3s;
}
</style>
