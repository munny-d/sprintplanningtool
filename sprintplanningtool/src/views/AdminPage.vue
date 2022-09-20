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
            <b-accordion-item class="acc-header" title="Reports">
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
                                            variant="outline-primary"
                                            size="sm"
                                            id="delete-btn"
                                            @click="displayReportModal(report)"
                                            title="Click to open more details"
                                        >
                                            View
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
            <report-modal
                :report="report"
                :showModal="showReportModal"
                v-if="showReportModal"
                @isModalOpen="toggleReportModal"
                @repId="triggerDeleteReport"
            ></report-modal>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { userService } from '../services/user-service';
import { reportService } from '../services/report-service';
import UserDeleteModal from '../components/UserDeleteModal.vue';
import UserEditModal from '../components/UserEditModal.vue';
import ReportModal from '../components/ReportModal.vue';
import router from '../router';
import { dateHelper } from '../helpers/date-helper';

interface Data {
    users: [];
    user: object;
    reports: [];
    report: object;
    showDeleteModal: boolean;
    showEditModal: boolean;
    showReportModal: boolean;
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
            showReportModal: false,
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
        ReportModal,
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

            console.log('REPORT: ', reports);

            reports.forEach((report: any) => {
                // Format ISO date to display as dd-mm-yyyy
                report.sprintStartDate = dateHelper.formatDate(
                    report.sprintStartDate
                );
                report.sprintEndDate = dateHelper.formatDate(
                    report.sprintEndDate
                );
                report.createdDate = dateHelper.formatDate(report.createdDate);

                // @ts-ignore
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
        displayReportModal(report: object) {
            this.report = report;
            this.showReportModal = true;
        },
        toggleDeleteModal(isModalOpen: boolean) {
            isModalOpen = this.showDeleteModal = !this.showDeleteModal;
            return isModalOpen;
        },
        toggleEditModal(isModalOpen: boolean) {
            isModalOpen = this.showEditModal = !this.showEditModal;
            return isModalOpen;
        },
        toggleReportModal(isModalOpen: boolean) {
            isModalOpen = this.showReportModal = !this.showReportModal;
            return isModalOpen;
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
            // @ts-ignore
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
                        alert('User has been deleted');
                        location.reload();
                    })
                    .catch((e) => {
                        console.log('Error during deletion: ', e);
                        this.errorMsg = `Error: ${e}`;
                    });
            }
        },
        triggerDeleteReport(repId: number) {
            this.errorMsg = '';

            console.log('TRIGGERING DELETE...');
            console.log('REPORT ID: ', repId);

            reportService
                .delete(repId)
                .then(() => {
                    alert('Report has been deleted');
                    location.reload();
                })
                .catch((e) => {
                    console.log('---Error while deleting report: ', e);
                });
        },
    },
});
</script>

<style scoped>
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

table {
    width: 70%;
    border-collapse: collapse;
    border-width: 2px;
    border-style: solid;
    margin-top: 1rem;
    margin-bottom: 1rem;
    background-color: rgb(239, 239, 239);
    text-align: center;
}

table td,
table th {
    border-width: 2px;
    border-color: rgb(70, 59, 76);
    border-style: solid;
    padding: 3px;
}
</style>
