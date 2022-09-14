// import Vue from 'vue';
// import VueRouter from 'vue-router';

// import HomePage from './components/HomePage.vue';
// import CreateReportPage from './components/CreateReportPage.vue';
// import ResultsPage from './components/ResultsPage.vue';
// import AdminPage from './components/AdminPage.vue';

// export const router = new VueRouter({
//     mode: 'history',
//     routes: [
//         { path: '/', name: 'home', component: HomePage },
//         {
//             path: '/create-report',
//             name: 'create-report',
//             component: CreateReportPage,
//         },
//         { path: '/results', name: 'results', component: ResultsPage },
//         { path: '/admin', name: 'admin', component: AdminPage },

//         // otherwise redirect to home
//         { path: '*', redirect: '/' },
//     ],
// });

// router.beforeEach((to: any, from: any, next: any) => {
//     // redirect to login page if not logged in and trying to access a restricted page
//     const publicPages = ['/'];
//     const authRequired = !publicPages.includes(to.path);
//     const loggedIn = localStorage.getItem('user');

//     if (authRequired && !loggedIn) {
//         return next('/');
//     }

//     next();
// });
