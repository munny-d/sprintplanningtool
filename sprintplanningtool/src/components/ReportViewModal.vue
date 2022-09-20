<template>
    <div>
        <!-- Main modal -->
        <vue-final-modal
            v-model="showModal"
            classes="modal-container"
            content-class="modal-content"
        >
            <span class="modal__title">View Report Information</span>
            <div class="modal__content">
                <li><b>Start date: </b>{{ report.sprintStartDate }}</li>
                <li><b>End date: </b> {{ report.sprintEndDate }}</li>
                <table>
                    <thead>
                        <tr>
                            <th>Team member(s)</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr
                            v-for="member in report.teamMembers"
                            :key="member.id"
                        >
                            <td>
                                {{ member.username }}
                            </td>
                        </tr>
                    </tbody>
                </table>
                <li><b>Team size: </b> {{ report.teamSize }}</li>
                <li><b>Work Days: </b>{{ report.workDays }}</li>
                <li><b>Capacity: </b> {{ report.capacity }}%</li>
                <li><b>New SP: </b> {{ report.newSP }}</li>
                <li><b>Carried SP: </b> {{ report.carriedSP }}</li>
                <li><b>Total SP: </b> {{ report.totalSP }}</li>

                <li><b> Sprint Goal: </b> {{ report.sprintGoal }}</li>
                <li><b> Created Date: </b>{{ report.createdDate }}</li>

                <div class="modal__action">
                    <b-button
                        variant="outline-primary"
                        @click="onClose"
                        title="Click here to finish viewing"
                        >Done</b-button
                    >
                </div>
            </div>
        </vue-final-modal>
    </div>
</template>

<script lang="ts">
import { defineComponent } from '@vue/runtime-core';

interface Data {
    showModal: boolean;
    showConfirmModal: boolean;
}

export default defineComponent({
    data(): Data {
        return {
            showModal: true,
            showConfirmModal: false,
        };
    },
    props: {
        report: {},
    },
    methods: {
        onClose() {
            // @ts-ignore
            this.$emit('isModalOpen', false);
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
    list-style: none;
    display: flex;
    margin: 1rem;
    padding-left: 10rem;
    padding-right: 10rem;
    justify-content: space-between;
}

table {
    width: 100%;
    border-collapse: collapse;
    border-width: 2px;
    border-style: solid;
    margin-top: 1rem;
    margin-bottom: 1rem;
    background-color: rgb(239, 239, 239);
    text-align: center;
}

b {
    margin-right: 10px;
}
</style>
