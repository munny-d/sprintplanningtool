<template>
    <img alt="Currys logo" src="./assets/logo.png" />
    <HomePage />
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import HomePage from './components/HomePage.vue';

export default defineComponent({
    name: 'App',
    components: {
        HomePage,
    },
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
