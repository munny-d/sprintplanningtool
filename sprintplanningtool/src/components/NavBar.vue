<template>
    <b-nav pills>
        <b-nav-item
            v-if="displayBtn"
            :active="$route.name == 'Create-Report'"
            href="/create-report"
            >Create Report</b-nav-item
        >
        <b-nav-item
            :active="$route.name == 'Dashboard'"
            href="/dashboard"
            v-if="displayBtn"
            >View latest report</b-nav-item
        >
        <b-nav-item
            :active="$route.name == 'Admin'"
            href="/admin"
            v-if="isAdmin"
            >Admin</b-nav-item
        >
        <b-nav-item v-if="displayBtn" @click="onLogout">Logout</b-nav-item>
    </b-nav>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { userService } from '../services/user-service';

export default defineComponent({
    name: 'NavBar',
    data() {
        return {
            isAdmin: false,
            displayBtn: false,
        };
    },
    watch: {
        $route() {
            console.log('WATCH');

            // @ts-ignore
            const user = JSON.parse(localStorage.getItem('user'));

            if (user) {
                user ? (this.displayBtn = true) : (this.displayBtn = false);
                user.isAdmin ? (this.isAdmin = true) : (this.isAdmin = false);
            }
        },
    },
    methods: {
        onLogout() {
            userService.logout();
        },
    },
});
</script>
<style>
.nav {
    display: flex;
    justify-content: end;
    width: 100%;
    transform: translateY(-60px);
}

.nav-link {
    color: rgb(109, 105, 105);
}

.nav-pills .nav-link:hover {
    border: solid #5700d3 1px;
}

.nav-pills .nav-link.active {
    background: #5700d3;
}

.nav-pills {
    background: rgb(115, 36, 163);
    background: linear-gradient(
        32deg,
        rgba(115, 36, 163, 1) 0%,
        rgba(254, 253, 255, 1) 100%,
        rgba(255, 255, 255, 1) 100%
    );
    padding: 1rem;
}

a {
    margin-left: 1rem;
}

.nav-link:hover {
    color: rgb(0, 0, 0);
}
</style>
