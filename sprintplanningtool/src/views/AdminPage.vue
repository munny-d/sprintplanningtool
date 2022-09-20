<template>
    <div>
        <h1>Admin Console</h1>
        <b-accordion>
            <b-accordion-item title="Users">
                <div class="container">
                    <table id="team-table">
                        <tbody>
                            <tr v-for="user in users" :key="user.id">
                                <td id="td-text">
                                    {{ user.username }}
                                    <div class="btn-position">
                                        <b-button
                                            variant="primary"
                                            size="sm"
                                            id="edit-btn"
                                            @click="displayEditModal(user)"
                                        >
                                            edit
                                        </b-button>
                                        <b-button
                                            variant="danger"
                                            size="sm"
                                            id="delete-btn"
                                            @click="displayDeleteModal(user)"
                                        >
                                            delete
                                        </b-button>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </b-accordion-item>
            <b-accordion-item title="Reports"> </b-accordion-item>
        </b-accordion>
        <div>
            <user-delete-modal
                :user="user"
                :showModal="showDeleteModal"
                v-if="showDeleteModal"
                @isModalOpen="toggleDeleteModal"
                @userId="triggerDeleteUser"
            ></user-delete-modal>
        </div>
        <div>
            <user-edit-modal
                :user="user"
                :showModal="showEditModal"
                v-if="showEditModal"
                @isModalOpen="toggleEditModal"
                @userId="triggerEditUser"
            ></user-edit-modal>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { userService } from '../services/user-service';
import UserDeleteModal from '../components/UserDeleteModal.vue';
import UserEditModal from '../components/UserEditModal.vue';
import router from '../router';

interface Data {
    users: [];
    user: {};
    showDeleteModal: boolean;
    showEditModal: boolean;
}

export default defineComponent({
    data(): Data {
        return {
            users: [],
            user: {},
            showDeleteModal: false,
            showEditModal: false
        };
    },
    created() {
        this.populateTable();
    },
    components: {
        UserDeleteModal,
        UserEditModal,
    },
    methods: {
        async populateTable() {
            const users = await userService
                .getAll()
                .catch((error) => console.log(error));

            users.forEach((user: any) => {
                // @ts-ignore
                this.users.push(user);
            });
        },
        displayDeleteModal(user: object) {
            this.user = user;
            this.showDeleteModal = true;
        },
        displayEditModal(user: object) {
            this.user = user;
            this.showEditModal = true;

        },
        toggleDeleteModal(isModalOpen: boolean) {
            console.log('TOGGLE MODAL FOR DELETE:', isModalOpen);
            isModalOpen = this.showDeleteModal = !this.showDeleteModal;
            return isModalOpen;
        },
        toggleEditModal(isModalOpen: boolean) {
            console.log('TOGGLE MODAL FOR EDIT:', isModalOpen);
            isModalOpen = this.showEditModal = !this.showEditModal;
            return isModalOpen;
        },
        triggerEditUser(userId: number) {
            console.log("USER ID IN EDIT: " userId);

        },
        triggerDeleteUser(userId: number) {
            console.log('user id in parent', userId);
            const loggedInUser = JSON.parse(localStorage.getItem('user'));

            console.log('ID OF LOGGED IN', loggedInUser.id);

            // If user is deleting their own account, logout and navigate back to
            if (userId === loggedInUser.id) {
                console.log('IM ABOUT TO DELETE MAHSELF');

                userService
                    .delete(userId)
                    .then(() => userService.logout())
                    .then(() => router.push('/'))
                    .catch((e) => {
                        console.log('---Error while deleting self', e);
                    });
            } else {
                userService
                    .delete(userId)
                    .then(() => {
                        console.log('user is deleted...');
                        console.log('repopulating users...');
                        console.log('current set of users: ', this.users);
                        location.reload();
                    })
                    .catch((e) => {
                        console.log('Error during deletion: ', e);
                    });
            }
        },
    },
});
</script>

<style>
.container {
    display: flex;
    justify-content: center;
}

#t-head {
    background: darkgrey;
}

#td-text {
    margin: 2px;
    margin-bottom: 1rem;
    font-weight: bold;
    font-size: 20px;
}

#delete-btn {
    margin-left: 1rem;
}

.btn-position {
    margin: 2px;
}

.accordion {
    width: 50%;
    display: inline-block;
    margin-top: 2rem;
}

.accordion-button:not(.collapsed) {
    background: blueviolet;
    color: white;
}
</style>
