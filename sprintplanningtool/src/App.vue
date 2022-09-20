<template>
    <nav-bar></nav-bar>
    <img alt="Currys logo" src="./assets/logo.png" />
    <hr />
    <router-view />
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import NavBar from './components/NavBar.vue';

export default defineComponent({
    components: { NavBar },
    name: 'App',
});
</script>

<style>
/* All shared styles are created here */
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
    margin-top: 1rem;
}

.form-style {
    box-shadow: rgba(0, 0, 0, 0.25) 0px 54px 55px,
        rgba(0, 0, 0, 0.12) 0px -12px 30px, rgba(0, 0, 0, 0.12) 0px 4px 6px,
        rgba(0, 0, 0, 0.17) 0px 12px 13px, rgba(0, 0, 0, 0.09) 0px -3px 5px;
    width: 50%;
    padding: 50px;
    justify-content: center;
    margin-left: auto;
    margin-right: auto;
    margin-top: 1rem;
}

.form-btn {
    margin: 0.5rem;
}

.error,
.invalid-field {
    color: red;
    font-size: small;
    margin: 0;
    display: block;
}

.success {
    color: green;
    font-size: small;
}

ul {
    list-style: none;
}
</style>
