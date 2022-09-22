<template>
    <div class="main">
        <h1 id="home-title">Sprint Planning Tool</h1>

        <b-button @click="toggleRegisterForm" :disabled="isLoginFormOpen">
            Register
        </b-button>

        <b-button @click="toggleLoginForm" :disabled="isRegisterFormOpen">
            Login
        </b-button>

        <!-- Register Form -->
        <user-register-form
            v-if="isRegisterFormOpen"
            @isFormOpen="toggleRegisterForm"
        />

        <!-- Login Form -->
        <user-login-form v-if="isLoginFormOpen" @isFormOpen="toggleLoginForm" />
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import UserRegisterForm from '../components/UserRegisterForm.vue';
import UserLoginForm from '../components/UserLoginForm.vue';

interface Data {
    isRegisterFormOpen: boolean;
    isLoginFormOpen: boolean;
}

export default defineComponent({
    data(): Data {
        return {
            isRegisterFormOpen: false,
            isLoginFormOpen: false,
        };
    },
    components: { UserRegisterForm, UserLoginForm },
    methods: {
        toggleRegisterForm(isFormOpen: any): boolean {
            isFormOpen = this.isRegisterFormOpen = !this.isRegisterFormOpen;
            return isFormOpen;
        },
        toggleLoginForm(isFormOpen: any): boolean {
            isFormOpen = this.isLoginFormOpen = !this.isLoginFormOpen;
            return isFormOpen;
        },
    },
});
</script>

<style scoped>
#home-title {
    margin: 3rem;
    text-align: center;
}

.btn {
    font: inherit;
    padding: 0.5rem 1.5rem;
    background-color: rgb(140, 0, 255);
    border: 1px solid rgb(140, 0, 255);
    color: white;
    border-radius: 4px;
    cursor: pointer;
    margin-top: 0.5rem;
    margin: 0.5rem;
}

.btn:disabled,
.btn:disabled:hover {
    background-color: grey;
    cursor: not-allowed;
}

.btn:hover {
    background-color: rgb(79, 10, 190);
    border-color: rgb(79, 10, 190);
}

.form-btn {
    padding: 0.5rem;
    font-size: small;
}

form {
    background-color: #e4dceb;
    padding: 1rem;
    font: inherit;
    margin: auto;
    margin-top: 25px;
    width: 30rem;
}

label {
    width: 140px;
    display: inline-block;
    text-align: left;
}

input,
textarea,
select {
    width: 100%;
    display: block;
    margin-bottom: 1rem;
    box-sizing: border-box;
    font: inherit;
    border: 1px solid rgb(184, 184, 184);
    padding: 0.25rem;
    color: rgb(61, 58, 58);
}

input:focus,
textarea:focus {
    background-color: rgb(219, 190, 253);
    color: rgb(32, 5, 63);
}

.form-control-inline {
    display: flex;
    margin: 0.6rem 0;
}

.form-control-inline input {
    width: auto;
    margin-right: 0.5rem;
}
</style>
