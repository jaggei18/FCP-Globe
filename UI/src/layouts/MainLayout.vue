<template>
  <q-layout view="hHh lpR fFf">
    <q-header elevated class="bg-primary text-white">
      <q-toolbar>
        <q-toolbar-title>
          <q-avatar class="logo-img">
            <img src="../assets/globe-life-logo.svg">
          </q-avatar>
        </q-toolbar-title>
        <div class="menu-links">
          <a @click="$router.replace('/help')" href="javascript:void(0);">HELP</a>
        </div>
        <div class="menu-links">
          <a @click="$router.replace('/')" href="javascript:void(0);">INQUIRY</a>
        </div>
        <div class="menu-links">
          <a @click="$router.replace('/')" href="javascript:void(0);">LOG OFF</a>
        </div>
        <q-avatar class="firstc-logo-img">
          <img src="../assets/firstcommand.png">
        </q-avatar>
      </q-toolbar>
    </q-header>
    <q-page-container>
      <router-view />
    </q-page-container>
    <q-footer elevated>
      <q-toolbar class="flex flex-center text-white" style="background-color: #0F4B8F;"
        :style="'border-top: 2px solid ' + theme_color">
        <div class="col-12">
          <ul class="navbar-nav">
            <li class="nav-item">
              <a class="nav-link" @click="$router.replace('/help')" href="javascript:void(0);">HELP</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" @click="$router.replace('/privacy')" href="javascript:void(0);">PRIVACY POLICY</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" @click="$router.replace('/tac')" href="javascript:void(0);">TERMS AND CONDITIONS</a>
            </li>
          </ul>
        </div>
      </q-toolbar>
    </q-footer>
  </q-layout>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

export default defineComponent({
  name: 'MainLayout',

  components: {
    // EssentialLink
  },

  setup () {
    const leftDrawerOpen = ref(false)

    return {
      // essentialLinks: linksList,
      leftDrawerOpen,
      toggleLeftDrawer () {
        leftDrawerOpen.value = !leftDrawerOpen.value
      }
    }
  }
})
</script>
<style>
/* @import url('../css/bootstrap/bootstrap-utilities.css'); */
@import url('https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap');
body{
  font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
}
.header-image {
  height: 100%;
  z-index: -1;
  opacity: 0.2;
  filter: grayscale(100%);
}

.logo-img {
  font-size: 200px !important;
  height: 70px;
  border-radius: 0;
}

.firstc-logo-img {
  width: 10rem;
  padding: 1px;
  border-radius: 0;
}

.menu-links {
  margin-right: 30px;
  height: 50px;
  display: flex;
  align-items: end;
}

.menu-links a {
  text-decoration: none;
}

.menu-links a:hover{
  text-decoration: underline;
}

.menu-links.active>a {
  /* margin:20px; */
  text-decoration: underline;
  /* color: red; */
  font-weight: 500;
}

.q-layout__shadow:after {
  box-shadow: inset 0 0 0px 0px rgb(0 85 140), 0 0px 0px 4px rgb(0 85 140);
}

.q-tabs.items-center, .q-tabs__content{
  align-items: end !important;
  color: rgb(11 72 141);
}
.q-tab--active .q-tab__indicator{
  opacity: 0;
}
footer > .q-toolbar > div > .menu-links{
  width: 40%;
}
</style>
