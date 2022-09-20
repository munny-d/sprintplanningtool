<template>
    <Form
        method="post"
        ref="form"
        @submit="onSubmit"
        :validation-schema="schema"
        class="form-style"
    >
        <ul>
            <ErrorMessage name="username" class="invalid-field" />
            <li class="form-control-inline">
                <label for="register-username">Username: </label>
                <Field
                    name="username"
                    id="register-username"
                    v-model="registerUsername"
                    placeholder="Enter your username"
                />
            </li>

            <ErrorMessage name="email" class="invalid-field" />
            <li class="form-control-inline">
                <label for="register-email">E-mail: </label>
                <Field
                    name="email"
                    type="email"
                    id="register-email"
                    v-model="registerEmail"
                    placeholder="Enter your email"
                />
            </li>

            <ErrorMessage name="password" class="invalid-field" />
            <li class="form-control-inline">
                <label for="register-pwd">Password: </label>
                <Field
                    name="password"
                    type="password"
                    id="register-pwd"
                    v-model="registerPwd"
                    placeholder="Enter a password"
                />
            </li>

            <ErrorMessage name="pwdRepeat" class="invalid-field" />
            <li class="form-control-inline">
                <label for="register-pwd-repeat">Repeat Password: </label>
                <Field
                    name="pwdRepeat"
                    type="password"
                    id="register-pwd-repeat"
                    placeholder="Reenter password"
                />
            </li>
            <p class="error">{{ errorMsg }}</p>
            <p v-if="isLoading">Registering user...</p>
            <p v-if="submitted" class="success">
                User has successfully been created, please close this form to
                login.
            </p>
            <li>
                <b-button variant="primary" class="form-btn" type="submit">
                    Create Account
                </b-button>
                <b-button
                    variant="danger"
                    class="form-btn"
                    type="button"
                    @click="onClose"
                >
                    Close
                </b-button>
            </li>
        </ul>
    </Form>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { userService } from '../services/user-service';
import { Form, Field, ErrorMessage } from 'vee-validate';
import * as yup from 'yup';

interface Data {
    isRegisterFormOpen: false;
    isLoading: boolean;
    submitted: boolean;
    registerUsername: string;
    registerEmail: string;
    registerPwd: string;
    errorMsg: string;
    schema: object;
}

interface User {
    username: string;
    email: string;
    password: string;
}

// A schema that uses Yup (MIT licensed JS package) for Form validation.
const schema = yup.object().shape({
    username: yup
        .string()
        .required('Username is required')
        .matches(
            /^[a-zA-Z0-9@]+$/,
            'This field cannot be empty or contain special characters'
        ),
    email: yup.string().required('Email is required').email(),
    password: yup
        .string()
        .required('Password is required')
        .min(6, 'Password must be at least 6 characters'),
    pwdRepeat: yup
        .string()
        .required('Please confirm your password.')
        .oneOf([yup.ref('password')], 'Passwords do not match'),
});

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
        // @ts-ignore
        async onSubmit(newUser: User | any, { resetForm }): Promise<void> {
            this.isLoading = true;
            this.submitted = false;
            this.errorMsg = '';

            try {
                userService
                    .register(newUser)
                    .then(() => {
                        this.submitted = true;
                        this.isLoading = false;
                        resetForm();
                    })
                    .catch((error) => {
                        this.isLoading = false;
                        this.errorMsg = error;
                    });
            } catch (error) {
                this.submitted = false;
                console.log(`ERROR in catch block: ${error}`);
            }
        },
    },
});
</script>

<style scoped>
label {
    margin: 0.5rem;
    text-align: start;
}
</style>
