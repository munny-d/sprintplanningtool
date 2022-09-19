<template>
    <div>
        <h1>Admin Page</h1>
        <div class="container">
            <table id="team-table">
                <thead id="t-head">
                    <tr>
                        <th>Users</th>
                    </tr>
                </thead>
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
        <div>
            <user-delete-modal
                :user="user"
                :showModal="show"
                v-if="show"
                @isModalOpen="toggleModal"
                @userId="triggerDeleteUser"
            ></user-delete-modal>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { userService } from '../services/user-service';
import UserDeleteModal from '../components/UserDeleteModal.vue';
import router from '../router';

interface Data {
    users: [];
    user: {};
    show: boolean;
}

export default defineComponent({
    data(): Data {
        return {
            users: [],
            user: {},
            show: false,
        };
    },
    created() {
        this.populateTable();
    },
    components: {
        UserDeleteModal,
    },
    watch: {
        users(newUsers, oldUsers) {},
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
            this.show = true;
        },
        displayEditModal(user: object) {
            // this.user = user;
            // this.show = true;

            console.log('EDIT BTN CLICKED');
        },
        toggleModal(isModalOpen: boolean) {
            console.log('TOGGLE MODAL:', isModalOpen);
            isModalOpen = this.show = !this.show;
            return isModalOpen;
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
    font-size: 40px;
}

#delete-btn {
    margin-left: 1rem;
}

.btn-position {
    margin: 2px;
}
</style>
