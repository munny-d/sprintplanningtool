<template>
    <form action="" method="post" ref="form" @submit="handleRegister">
        <ul>
            <li class="form-control-inline">
                <label for="name">Username: </label>
                <input
                    type="text"
                    id="name"
                    name="user_name"
                    v-model="registerUsername"
                    placeholder="Enter your username"
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
            <p class="error">{{ errorMsg }}</p>
            <li>
                <button class="btn form-btn" type="submit">
                    Create Account
                </button>
                <button class="btn form-btn" type="button" @click="onClose">
                    Cancel
                </button>
            </li>
        </ul>
    </form>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { userService } from '../services/user-service';

interface Data {
    isRegisterFormOpen: false;
    isLoading: boolean;
    submitted: boolean;
    registerUsername: string;
    registerEmail: string;
    registerPwd: string;
    errorMsg: string;
}

interface User {
    username: string;
    email: string;
    password: string;
}

let user: User = {
    username: '',
    email: '',
    password: '',
};

export default defineComponent({
    data(): Data {
        return {
            isRegisterFormOpen: false,
            isLoading: false,
            submitted: false,
            registerUsername: '',
            registerEmail: '',
            registerPwd: '',
            errorMsg: '',
        };
    },
    methods: {
        onClose(event: MouseEvent) {
            console.log('---register form btn closed: ', event);
            this.$emit('isFormOpen', false);
        },
        async handleRegister(e: any): Promise<void> {
            e.preventDefault();
            this.submitted = true;
            this.errorMsg = '';

            // TODO: Display success message and redirect to create report page
            // TODO: ADD VALIDATION

            try {
                user.username = this.registerUsername;
                user.email = this.registerEmail;
                user.password = this.registerPwd;
                console.log(user);

                userService
                    .register(user)
                    .then((res) => console.log('res', res))
                    .catch((error) => (this.errorMsg = error));
            } catch (error) {
                console.log(`ERROR in catch block: ${error}`);
            }
        },
    },
});
</script>

<style>
.error {
    color: red;
}
</style>
