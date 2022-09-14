<template>
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
                <button class="btn form-btn" type="button" @click="onClose">
                    Cancel
                </button>
            </li>
        </ul>
    </form>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

interface Data {
    isRegisterFormOpen: false;
    isLoading: boolean;
    submitted: boolean;
    registerUsername: string;
    registerEmail: string;
    registerPwd: string;
}

export default defineComponent({
    data(): Data {
        return {
            isRegisterFormOpen: false,
            isLoading: false,
            submitted: false,
            registerUsername: '',
            registerEmail: '',
            registerPwd: '',
        };
    },
    methods: {
        onClose(event: PointerEvent) {
            console.log('---register form btn closed: ', event);
            this.$emit('isFormOpen', false);
        },
        async handleRegister(e: any): Promise<void> {
            e.preventDefault();
            // TODO
        },
    },
});
</script>
