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
                            placeholder="Enter your email"
                            required
                        />
                    </li>
                    <li class="form-control-inline">
                        <label for="pwd">Password: </label>
                        <input
                            type="password"
                            id="password"
                            name="user_password"
                            placeholder="Enter password"
                        />
                    </li>
                    <li class="form-control-inline">
                        <label for="pwd-repeat">Repeat Password: </label>
                        <input
                            type="password"
                            id="password"
                            name="user_password"
                            placeholder="Enter password"
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
                            name="login_name"
                            required
                        />
                    </li>
                    <li class="form-control-inline">
                        <label for="login_mail">E-mail:</label>
                        <input
                            type="email"
                            id="login_mail"
                            name="user_email"
                            required
                        />
                    </li>
                    <button class="btn form-btn" type="submit">Login</button>
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

interface Data {
    loading: boolean;
    isRegisterFormOpen: boolean;
    isLoginFormOpen: boolean;
}

export default defineComponent({
    data(): Data {
        return {
            loading: false,
            isRegisterFormOpen: false,
            isLoginFormOpen: false,
        };
    },
    created() {
        // fetch the data when the view is created and the data is
        // already being observed
        this.fetchData();
    },
    watch: {
        // call again the method if the route changes
        $route: 'fetchData',
    },
    methods: {
        fetchData(): void {
            this.loading = true;

            // fetch('weatherforecast')
            //     .then((r) => r.json())
            //     .then((json) => {
            //         this.post = json as Forecasts;
            //         this.loading = false;
            //         return;
            //     });
        },
        toggleRegisterForm(): boolean {
            return (this.isRegisterFormOpen = !this.isRegisterFormOpen);
        },
        toggleLoginForm(): boolean {
            return (this.isLoginFormOpen = !this.isLoginFormOpen);
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
