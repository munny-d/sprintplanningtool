<template>
    <b-nav pills>
        <b-nav-item :active="$route.name == 'Home'" href="/">Home</b-nav-item>
        <b-nav-item :active="$route.name == 'Dashboard'" href="/create-report"
            >Create Report</b-nav-item
        >
        <b-nav-item :active="$route.name == 'Admin'" href="/admin"
            >Admin</b-nav-item
        >
        <b-nav-item @click="onLogout">Logout</b-nav-item>
    </b-nav>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { userService } from '../services/user-service';
import router from '../router';

export default defineComponent({
    name: 'NavBar',
    methods: {
        onLogout() {
            userService.logout();
            router.push({ path: '/' });
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
