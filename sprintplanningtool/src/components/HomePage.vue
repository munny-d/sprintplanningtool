<template>
    <div class="main">
        <h1>Sprint Planning Tool</h1>
        <button class="btn" @click="toggleRegisterForm">Register</button>
        <button class="btn" @click="toggleLoginForm">Login</button>

        <!-- Register Form -->
        <div v-if="isRegisterFormOpen">
            <form action="" method="post">
                <ul>
                    <li class="form-control-inline">
                        <label for="name">Username: </label>
                        <input
                            type="text"
                            id="name"
                            name="user_name"
                            v-model="registerUsername"
                            placeholder="Enter your username"
                            required
                        />
                    </li>
                    <li class="form-control-inline">
                        <label for="mail">E-mail: </label>
                        <input
                            type="email"
                            id="mail"
                            name="user_email"
                            v-model="registerEmail"
                            placeholder="Enter your email"
                            required
                        />
                    </li>
                    <li class="form-control-inline">
                        <label for="register-pwd">Password: </label>
                        <input
                            type="password"
                            id="register-pwd"
                            name="user_pwd"
                            v-model="registerPwd"
                            placeholder="Enter a password"
                        />
                    </li>
                    <li class="form-control-inline">
                        <label for="pwd-repeat">Repeat Password: </label>
                        <input
                            type="password"
                            id="pwd-repeat"
                            name="pwd_repeat"
                            placeholder="Reenter password"
                        />
                    </li>
                    <li>
                        <button class="btn form-btn" type="submit">
                            Create Account
                        </button>
                        <button
                            class="btn form-btn"
                            type="button"
                            @click="isRegisterFormOpen = false"
                        >
                            Cancel
                        </button>
                    </li>
                </ul>
            </form>
        </div>

        <!-- Login Form -->
        <div v-if="isLoginFormOpen">
            <form action="" method="post">
                <ul>
                    <li class="form-control-inline">
                        <label for="login-name">Username:</label>
                        <input
                            type="text"
                            id="login-name"
                            v-model="username"
                            name="login_name"
                            required
                        />
                    </li>
                    <li class="form-control-inline">
                        <label for="login_pwd">Password:</label>
                        <input
                            type="password"
                            v-model="password"
                            id="login_pwd"
                            name="login_pwd"
                            required
                        />
                    </li>
                    <button
                        class="btn form-btn"
                        type="submit"
                        @click="handleLogin"
                    >
                        Login
                    </button>
                    <button
                        class="btn form-btn"
                        type="button"
                        @click="isLoginFormOpen = false"
                    >
                        Cancel
                    </button>
                </ul>
            </form>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { userService } from '../services/user-service';

interface Data {
    loading: boolean;
    isRegisterFormOpen: boolean;
    isLoginFormOpen: boolean;
    username: string;
    password: string;
    submitted: boolean;
    registerUsername: string;
    registerEmail: string;
    registerPwd: string;
}

export default defineComponent({
    data(): Data {
        return {
            loading: false,
            isRegisterFormOpen: false,
            isLoginFormOpen: false,
            username: '',
            password: '',
            submitted: false,
            registerUsername: '',
            registerEmail: '',
            registerPwd: '',
        };
    },
    methods: {
        toggleRegisterForm(): boolean {
            return (this.isRegisterFormOpen = !this.isRegisterFormOpen);
        },
        toggleLoginForm(): boolean {
            return (this.isLoginFormOpen = !this.isLoginFormOpen);
        },
        async handleLogin(e: any): Promise<void> {
            e.preventDefault();

            this.submitted = true;
            const { username, password } = this;

            console.log(`--USERNAME: ${username}, PASSWORD: ${password}`);

            if (username && password) {
                await userService.login(username, password);
            }
        },
    },
});
</script>

<style scoped>
ul {
    list-style-type: none;
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

.btn:hover {
    background-color: rgb(79, 10, 190);
    border-color: rgb(79, 10, 190);
}

.form-btn {
    padding: 0.5rem;
    font-size: small;
}

form {
    /* color: rgb(255, 255, 255); */
    padding: 1rem;
    border: solid 2px black;
    border-radius: 6px;
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
}

.form-control-inline input {
    width: auto;
    margin-right: 0.5rem;
}
</style>
