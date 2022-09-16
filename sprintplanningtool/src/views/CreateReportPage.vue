<template>
    <div>
        <a @click="onLogout">Logout</a>
        <h1>Create Report Page</h1>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import router from '../router';
import { userService } from '../services/user-service';

interface Data {}

export default defineComponent({
    data(): Data {
        return {};
    },
    methods: {
        async onLogout(): Promise<void> {
            userService.logout();
            router.push({ path: '/' });
        },
    },
});
</script>

<style>
a {
    cursor: pointer;
}
</style>
