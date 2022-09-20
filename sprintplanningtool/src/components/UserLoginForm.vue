<template>
    <Form
        class="form-style"
        method="post"
        @submit="onSubmit"
        :validation-schema="schema"
    >
        <ul>
            <ErrorMessage name="loginUsername" class="invalid-field" />
            <li class="form-control-inline">
                <label for="login-name">Username:</label>
                <Field
                    type="text"
                    id="login-name"
                    v-model="username"
                    name="loginUsername"
                    required
                />
            </li>

            <ErrorMessage name="loginPwd" class="invalid-field" />
            <li class="form-control-inline">
                <label for="login-pwd">Password:</label>
                <Field
                    type="password"
                    v-model="password"
                    id="login-pwd"
                    name="loginPwd"
                    required
                />
            </li>
            <p v-if="isLoading">Authenticating credentials...</p>
            <p class="error" v-if="hasFailedValidation">
                Please submit valid credentials. If you have forgotten your
                account details, please contact an admin.
            </p>
            <p class="error">{{ errorMsg }}</p>
            <b-button variant="primary" class="form-btn" type="submit"
                >Login</b-button
            >
            <b-button
                variant="danger"
                class="form-btn"
                type="button"
                @click="onClose"
            >
                Cancel
            </b-button>
        </ul>
    </Form>
</template>

<script lang="ts">
import router from '../router';
import { defineComponent } from 'vue';
import { userService } from '../services/user-service';
import { Form, Field, ErrorMessage } from 'vee-validate';
import * as yup from 'yup';

interface Data {
    isLoading: boolean;
    isLoginFormOpen: boolean;
    username: string;
    password: string;
    submitted: boolean;
    hasFailedValidation: boolean;
    errorMsg: string;
    schema: object;
}

// A schema that uses Yup (MIT licensed JS package) for Form validation.
const schema: any = yup.object().shape({
    loginUsername: yup.string().required('Username is required'),
    loginPwd: yup.string().required('Password is required'),
});

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
            schema,
        };
    },
    components: {
        Form,
        Field,
        ErrorMessage,
    },
    methods: {
        onClose() {
            this.$emit('isFormOpen', false);
        },
        async onSubmit(): Promise<void> {
            this.submitted = true;
            this.isLoading = true;
            this.errorMsg = '';

            const { username, password } = this;

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
.error,
.invalid-field {
    color: red;
    font-size: small;
    margin: 0;
    display: block;
}

.form-btn {
    margin: 0.5rem;
}

label {
    margin: 1rem;
}
</style>
