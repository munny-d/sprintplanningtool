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

                            <button id="edit-btn" @click="displayInfo(user)">
                                edit
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div>
            test
            <user-modal></user-modal>
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
            show: true,
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
        displayInfo(user: object) {
            console.log('click');
            this.user = user;
            console.log(user);
        },
        openModalExample() {},
        confirm() {
            this.show = false;
        },
        cancel(close) {
            close();
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
