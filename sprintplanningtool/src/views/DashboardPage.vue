<template>
    <div>
        <!-- Create navbar and import here -->
        <h1>Sprint Report</h1>
        <div class="report-style">
            <ul>
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
            </ul>
        </div>
        <b-button variant="outline-dark" @click="sendEmail"
            >Click to email</b-button
        >
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { reportService } from '../services/report-service';
import { dateHelper } from '../helpers/date-helper';

export default defineComponent({
    data() {
        return {
            report: {},
        };
    },
    created() {
        this.generateReport();
    },
    methods: {
        async generateReport() {
            // Fetch the latest report to display
            const report = await reportService.getLatest();

            // Format ISO date to display as dd-mm-yyyy.
            report.sprintStartDate = dateHelper.formatDate(
                report.sprintStartDate
            );
            report.sprintEndDate = dateHelper.formatDate(report.sprintEndDate);
            report.createdDate = dateHelper.formatDate(report.createdDate);

            this.report = report;
            return this.report;
        },
        sendEmail() {
            console.log('SENDING EMAIL...');
            console.log('EMAIL SENT...');
        },
    },
});
</script>

<style scoped>
.report-style {
    background-color: whitesmoke;
    box-shadow: rgba(0, 0, 0, 0.25) 0px 54px 55px,
        rgba(0, 0, 0, 0.12) 0px -12px 30px, rgba(0, 0, 0, 0.12) 0px 4px 6px,
        rgba(0, 0, 0, 0.17) 0px 12px 13px, rgba(0, 0, 0, 0.09) 0px -3px 5px;
    width: fit-content;
    margin-left: auto;
    margin-right: auto;
    margin-top: 1rem;
    padding: 0.5rem;
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
    width: 50%;
    margin-left: auto;
    margin-right: auto;
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
