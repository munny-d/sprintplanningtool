<template>
    <div>
        <h1>Admin Console</h1>
        <p class="error">{{ errorMsg }}</p>
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
            <b-accordion-item title="Reports">
                <div class="container">
                    <table id="team-table">
                        <tbody>
                            <tr v-for="report in reports" :key="report.id">
                                <td id="td-text">
                                    <ul>
                                        <li>Report ID: {{ report.id }}</li>
                                        <li>
                                            Start:
                                            {{ report.sprintStartDate }}
                                        </li>
                                        <li>
                                            End:
                                            {{ report.sprintEndDate }}
                                        </li>
                                        <li>
                                            Created by:
                                            {{ report.createdByUser }}
                                        </li>
                                    </ul>
                                    <div class="btn-position">
                                        <b-button
                                            variant="primary"
                                            size="sm"
                                            id="delete-btn"
                                            @click="displayRepViewModal(report)"
                                        >
                                            view
                                        </b-button>
                                        <b-button
                                            variant="danger"
                                            size="sm"
                                            id="delete-btn"
                                            @click="
                                                displayRepDeleteModal(report)
                                            "
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
                @updatedUser="triggerEditUser"
            ></user-edit-modal>
        </div>
        <div>
            <report-delete-modal
                :report="report"
                :showModal="showRepDeleteModal"
                v-if="showRepDeleteModal"
                @isModalOpen="toggleRepDeleteModal"
                @repId="triggerDeleteRep"
            ></report-delete-modal>
        </div>
        <div>
            <report-view-modal
                :report="report"
                :showModal="showRepViewModal"
                v-if="showRepViewModal"
                @isModalOpen="toggleRepViewModal"
            ></report-view-modal>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { userService } from '../services/user-service';
import { reportService } from '../services/report-service';
import UserDeleteModal from '../components/UserDeleteModal.vue';
import UserEditModal from '../components/UserEditModal.vue';
import ReportDeleteModal from '../components/ReportDeleteModal.vue';
import ReportViewModal from '../components/ReportViewModal.vue';
import router from '../router';

interface Data {
    users: [];
    user: object;
    reports: [];
    report: object;
    showDeleteModal: boolean;
    showEditModal: boolean;
    showRepDeleteModal: boolean;
    showRepViewModal: boolean;
    errorMsg: string;
}

export default defineComponent({
    data(): Data {
        return {
            users: [],
            user: {},
            reports: [],
            report: {},
            showDeleteModal: false,
            showEditModal: false,
            showRepDeleteModal: false,
            showRepViewModal: false,
            errorMsg: '',
        };
    },
    created() {
        this.fetchUsers();
        this.fetchReports();
    },
    components: {
        UserDeleteModal,
        UserEditModal,
        ReportDeleteModal,
        ReportViewModal,
    },
    methods: {
        async fetchUsers() {
            const users = await userService
                .getAll()
                .catch((error) => console.log(error));

            users.forEach((user: any) => {
                // @ts-ignore
                this.users.push(user);
            });
        },
        async fetchReports() {
            const reports = await reportService.getAll().catch((e) => {
                console.log('Error fetching reports: ', e);
            });

            reports.forEach((report: any) => {
                this.reports.push(report);
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
        displayRepDeleteModal(report: object) {
            this.report = report;
            this.showRepDeleteModal = true;
        },
        displayRepViewModal(report: object) {
            this.report = report;
            this.showRepViewModal = true;
        },
        toggleDeleteModal(isModalOpen: boolean) {
            isModalOpen = this.showDeleteModal = !this.showDeleteModal;
            return isModalOpen;
        },
        toggleEditModal(isModalOpen: boolean) {
            isModalOpen = this.showEditModal = !this.showEditModal;
            return isModalOpen;
        },
        toggleRepDeleteModal(isModalOpen: boolean) {
            isModalOpen = this.showRepDeleteModal = !this.showRepDeleteModal;
            return isModalOpen;
        },
        toggleRepViewModal(isModalOpen: boolean) {
            isModalOpen = this.showRepViewModal = !this.showRepViewModal;
            return isModalOpen;
        },
        triggerDeleteRep() {
            console.log('delete rep triggered');
        },
        triggerEditUser(user: any) {
            // @ts-ignore
            const loggedInUser = JSON.parse(localStorage.getItem('user'));

            this.errorMsg = '';

            // If user is editing their own account, logout and navigate back to homepage
            if (user.id === loggedInUser.id) {
                alert(
                    'Logged in user information is being updated... you may be logged and required to sign in again.'
                );
                userService
                    .update(user)
                    .then(() => userService.logout())
                    .then(() => router.push('/'))
                    .catch((e) => {
                        console.log('---Error while updating self', e);
                        this.errorMsg = `Error: ${e}`;
                    });
            } else {
                userService
                    .update(user)
                    .then(() => {
                        location.reload();
                    })
                    .catch((e) => {
                        console.log('Error during user update: ', e);
                        this.errorMsg = `Error: ${e}`;
                    });
            }
        },
        triggerDeleteUser(userId: number) {
            const loggedInUser = JSON.parse(localStorage.getItem('user'));
            this.errorMsg = '';

            // If user is deleting their own account, logout and navigate back to
            if (userId === loggedInUser.id) {
                userService
                    .delete(userId)
                    .then(() => userService.logout())
                    .then(() => router.push('/'))
                    .catch((e) => {
                        console.log('---Error while deleting self', e);
                        this.errorMsg = `Error: ${e}`;
                    });
            } else {
                userService
                    .delete(userId)
                    .then(() => {
                        location.reload();
                    })
                    .catch((e) => {
                        console.log('Error during deletion: ', e);
                        this.errorMsg = `Error: ${e}`;
                    });
            }
        },
    },
});
</script>

<style>
ul {
    list-style: none;
}

li {
    margin-bottom: 2px;
}

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
