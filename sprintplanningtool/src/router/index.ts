import { createWebHistory, createRouter } from 'vue-router';

import HomePage from '../views/HomePage.vue';
import CreateReportPage from '../views/CreateReportPage.vue';
import DashboardPage from '../views/DashboardPage.vue';
import AdminPage from '../views/AdminPage.vue';
import NotFoundPage from '../views/NotFoundPage.vue';

const routes = [
    { path: '/', name: 'Home', component: HomePage },
    {
        path: '/create-report',
        name: 'Create-Report',
        component: CreateReportPage,
    },
    { path: '/dashboard', name: 'Dashboard', component: DashboardPage },
    {
        path: '/admin',
        name: 'Admin',
        beforeEnter: checkAdminRights,
        component: AdminPage,
    },
    { path: '/:catchAll(.*)', name: 'NotFound', component: NotFoundPage },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

router.beforeEach((to: any, from: any, next: any) => {
    // redirect to login page if not logged in and trying to access a restricted page
    const publicPages = ['/'];
    const authRequired = !publicPages.includes(to.path);
    const loggedIn = localStorage.getItem('user');

    if (authRequired && !loggedIn) {
        return next('/');
    }

    next();
});

function checkAdminRights(to: any, from: any, next: any) {
    const user = JSON.parse(localStorage.getItem('user') as string);

    // check if the user is admin
    if (user.isAdmin) {
        next();
    } else {
        alert('Error: unauthorised access attempted.');
        next({ path: '/' });
    }
}

export default router;
