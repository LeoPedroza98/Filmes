import Vue from 'vue'
import VueRouter, { NavigationGuardNext, RouteConfig } from 'vue-router'

Vue.use(VueRouter)

const verificaPerfil = (next: NavigationGuardNext<Vue>, perfilPermitido: string[]) =>{

  var sessionApp: any;

  if(localStorage.sessionApp){    

    sessionApp = JSON.parse(localStorage.sessionApp);

    if(perfilPermitido.find(a => a == sessionApp.dados.role) == undefined || perfilPermitido.find(a => a == sessionApp.dados.role) == null){
      next({ name: 'Home' });
    }
    else{
      next();
    }
  }
  else
  {
    next({ name: 'Login' });
  }  
}

const routes: Array<RouteConfig> = [
  { path: '*', redirect: '/home' },
  { path: '/login', name: "Login", component: () => import("../views//shared/Login.vue") },
  { path: '/home', name: "Home", component: () => import("../views/Home.vue") },

  { path: '/all/diretor', name: "Diretor", component: () => import("../views/all/diretor/Lista.vue")},
  { path: '/all/filme', name: "Filme", component: () => import("../views/all/filme/Lista.vue")},
  { path: '/all/filmeDiretor', name: "FilmeDiretor", component: () => import("../views/all/filmeDiretor/Lista.vue")},
  { path: '/all/usuario', name: "Usuario", component: () => import("../views/all/usuario/Lista.vue") }, 
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
