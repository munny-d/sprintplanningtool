<template>
    <div id="nav">
        <img alt="Currys logo" src="./assets/logo.png" />
        <hr />
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

#nav li {
    display: inline;
    text-align: left;
}

hr {
    border: 0;
    height: 1px;
    background-image: linear-gradient(
        to right,
        rgba(0, 0, 0, 0),
        rgba(85, 73, 91, 0.75),
        rgba(0, 0, 0, 0)
    );
}

img {
    display: block;
    margin-left: auto;
    margin-right: auto;
    width: 200px;
    margin-bottom: 20px;
}
</style>
