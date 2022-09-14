<template>
    <img alt="Currys logo" src="./assets/logo.png" />
    <div id="nav">
        <router-link to="/">Home</router-link>
        <router-link to="/create-report">Create Report</router-link>
        <router-link to="/admin">Admin</router-link>
    </div>
    <router-view />
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
    name: 'App',
});
</script>

<style>
#app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    margin-top: 60px;
}

body {
    /* background-color: #f9d2e5; */
    background-color: #e8e3e6;
}
</style>
