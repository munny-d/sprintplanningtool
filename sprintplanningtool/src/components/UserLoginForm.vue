<template>
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
            <p v-if="isLoading">Authenticating credentials...</p>
            <p class="error" v-if="hasFailedValidation">
                Please submit valid credentials. If you have forgotten your
                account details, please contact an admin.
            </p>
            <p class="error">{{ errorMsg }}</p>
            <button class="btn form-btn" type="submit" @click="handleLogin">
                Login
            </button>
            <button class="btn form-btn" type="button" @click="onClose">
                Cancel
            </button>
        </ul>
    </form>
</template>

<script lang="ts">
import router from '../router';
import { defineComponent } from 'vue';
import { userService } from '../services/user-service';

interface Data {
    isLoading: boolean;
    isLoginFormOpen: boolean;
    username: string;
    password: string;
    submitted: boolean;
    hasFailedValidation: boolean;
    errorMsg: string;
}

export default defineComponent({
    data(): Data {
        return {
            isLoading: false,
            isLoginFormOpen: false,
            username: '',
            password: '',
            submitted: false,
            hasFailedValidation: false,
            errorMsg: '',
        };
    },
    methods: {
        onClose(event: MouseEvent) {
            console.log('---login form btn closed: ', event);
            this.$emit('isFormOpen', false);
        },
        async handleLogin(e: any): Promise<void> {
            e.preventDefault();

            this.submitted = true;
            this.isLoading = true;
            this.errorMsg = '';

            const { username, password } = this;

            console.log(`--USERNAME: ${username}, PASSWORD: ${password}`);

            if (username && password) {
                await userService
                    .login(username, password)
                    .then(() => {
                        router.push({ path: '/create-report' });
                    })
                    .catch((error) => {
                        this.isLoading = false;

                        // Display error message delivered from the backend instead.
                        this.hasFailedValidation = false;
                        this.errorMsg = `${error}. If you have forgotten your
                account details, please contact an admin.`;
                    });
            } else {
                this.isLoading = false;
                this.hasFailedValidation = true;
            }
        },
    },
});
</script>

<style scoped>
.error {
    color: red;
}
</style>
