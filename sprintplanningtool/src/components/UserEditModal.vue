<template>
    <div>
        <!-- Main modal -->
        <vue-final-modal
            v-model="showModal"
            classes="modal-container"
            content-class="modal-content"
        >
            <span class="modal__title">Edit User Information</span>
            <div class="modal__content">
                <p>
                    User id: <b> {{ user.id }} </b> Username:
                    <b> {{ user.username }} </b>
                </p>
                <Form>
                    <ul>
                        <li>
                            <Field
                                name="username"
                                v-model="updateUser.username"
                                placeholder="Change username"
                            ></Field>
                        </li>
                        <li class="form-control-inline">
                            <Field
                                name="email"
                                type="email"
                                v-model="updateUser.email"
                                placeholder="Change email"
                            />
                        </li>
                        <li>
                            <Field
                                name="password"
                                v-model="updateUser.password"
                                type="password"
                                placeholder="Change password"
                            ></Field>
                        </li>
                        <li>
                            <Field
                                name="pwdRepeat"
                                type="password"
                                placeholder="Reenter the password"
                                v-model="repeatPwd"
                            ></Field>
                        </li>
                        <li>
                            <b-form-checkbox v-model="updateUser.isAdmin"
                                >Make user admin</b-form-checkbox
                            >
                        </li>
                    </ul>

                    <!-- Confirmation modal -->
                    <div class="modal__action">
                        <b-button
                            variant="outline-success"
                            highlight
                            @click="showConfirmModal = true"
                            >confirm</b-button
                        >
                        <b-button variant="outline-secondary" @click="onClose"
                            >cancel</b-button
                        >
                    </div>
                </Form>
            </div>
        </vue-final-modal>

        <!-- Confirm modal -->
        <vue-final-modal
            v-model="showConfirmModal"
            classes="modal-container"
            content-class="modal-content"
        >
            <span class="modal__title">PLEASE CONFIRM THE CHANGES</span>
            <div class="modal__content">
                Please click confirm to update the user details.
            </div>
            <div class="modal__action">
                <b-button variant="outline-success" @click="confirmEdit"
                    >confirm</b-button
                >
                <b-button
                    variant="outline-danger"
                    @click="showConfirmModal = false"
                    >cancel</b-button
                >
            </div>
            <p class="error">
                If there is no changes required, please click cancel.
            </p>
        </vue-final-modal>
    </div>
</template>

<script lang="ts">
import { defineComponent } from '@vue/runtime-core';
import { Form, Field } from 'vee-validate';

interface Data {
    showModal: boolean;
    showConfirmModal: boolean;
    updateUser: object | any;
    repeatPwd: string;
    isValid: boolean;
    isErrorMsg: boolean;
}

export default defineComponent({
    data(): Data {
        return {
            showModal: true,
            showConfirmModal: false,
            updateUser: {
                username: '',
                password: '',
                email: '',
                id: 0,
                isAdmin: false,
            },
            repeatPwd: '',
            isValid: false,
            isErrorMsg: false,
        };
    },
    components: {
        Form,
        Field,
    },
    props: {
        user: {} as any,
    },
    methods: {
        confirmEdit() {
            const newUserInfo: any = this.updateUser;
            console.log('newUserInfo', newUserInfo);

            // Validation logic
            if (newUserInfo.username)
                this.validateUsername(newUserInfo.username);
            if (newUserInfo.email) this.validateEmail(newUserInfo.email);
            if (newUserInfo.password || this.repeatPwd)
                this.validatePassword(newUserInfo.password);

            if (!this.isValid && !this.updateUser.isAdmin) {
                return;
            }

            this.showConfirmModal = false;
            this.showModal = false;
            this.isErrorMsg = false;

            this.updateUser.id = this.user.id;

            this.$emit('updatedUser', this.updateUser);
            this.$emit('isModalOpen', false);
        },
        onClose() {
            // @ts-ignore
            this.$emit('isModalOpen', false);
        },
        validateUsername(username: string): boolean {
            username.trim();
            const regex = /^[a-zA-Z0-9@]+$/;
            this.isValid = username.length > 1 && regex.test(username);

            if (!this.isValid) {
                alert('invalid username entered.');
                this.isValid = false;
            }
            return this.isValid;
        },
        validateEmail(email: string): boolean {
            email.trim();
            const regex = /\S+@\S+\.\S+/;
            this.isValid = email.length > 1 && regex.test(email);

            if (!this.isValid) {
                alert('Invalid email entered.');
            }
            return this.isValid;
        },
        validatePassword(password: string): boolean {
            password.trim();
            this.isValid = password.length > 6 && password == this.repeatPwd;

            if (!this.isValid) {
                alert(
                    'Invalid password entered. Password must be at least 6 characters long and both passwords must match.'
                );
                this.isValid = false;
            }
            return this.isValid;
        },
    },
});
</script>

<style scoped>
::v-deep .modal-container {
    display: flex;
    justify-content: center;
    align-items: center;
}
::v-deep .modal-content {
    position: relative;
    display: flex;
    flex-direction: column;
    max-height: 90%;
    margin: 0 1rem;
    padding: 1rem;
    border: 1px solid #e2e8f0;
    border-radius: 0.25rem;
    background: #fff;
    width: 50%;
}
.modal__title {
    margin: 0 2rem 0.5rem 0;
    font-size: 1.5rem;
    font-weight: 700;
    transform: translateX(33px);
}
.modal__content {
    flex-grow: 1;
    overflow-y: auto;
}
.modal__action {
    display: flex;
    justify-content: center;
    align-items: center;
    flex-shrink: 0;
    padding: 1rem 0 0;
}
.modal__close {
    position: absolute;
    top: 0.5rem;
    right: 0.5rem;
}
</style>

<style scoped>
.dark-mode div::v-deep .modal-content {
    border-color: #2d3748;
    background-color: #1a202c;
}

.btn {
    margin: 0.5rem;
}

p {
    transform: translateX(10px);
}

ul {
    list-style: none;
    display: grid;
    justify-content: center;
}

li {
    margin: 0.5rem;
}
</style>
