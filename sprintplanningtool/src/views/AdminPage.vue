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
                        <td>
                            {{ user.username }}

                            <button id="edit-btn" @click="displayModal(user)">
                                edit
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div>
            <user-modal
                :user="user"
                :showModal="show"
                v-if="show"
                @isModalOpen="toggleModal"
            ></user-modal>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { userService } from '../services/user-service';
import UserModal from '../components/UserModal.vue';

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
        UserModal,
    },
    methods: {
        async populateTable() {
            const users = await userService
                .getAll()
                .catch((error) => console.log(error));

            users.forEach((user: any) => {
                this.users.push(user);
            });
        },
        displayModal(user: object) {
            this.user = user;
            this.show = true;

            console.log(this.show);
        },
        toggleModal(isModalOpen) {
            console.log('TOGGLE MODAL:', isModalOpen);
            isModalOpen = this.show = !this.show;
            return isModalOpen;
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
    text-align: center;
}

#edit-btn {
    margin-left: 10rem;
}
</style>
