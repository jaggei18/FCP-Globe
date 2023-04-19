import { RouteRecordRaw } from 'vue-router'

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      { path: '', component: () => import('pages/IndexPage.vue') },
      { path: 'help', component: () => import('pages/helpPage.vue') },
      { path: 'privacy', component: () => import('pages/ThePrivacy.vue') },
      { path: 'tac', component: () => import('pages/TheTAC.vue') }]
  },
  // {
  //   path: '',
  //   component: () => import('pages/helpPage.vue')
  // },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue')
  }
]

export default routes
