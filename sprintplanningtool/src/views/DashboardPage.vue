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
                <li><b>Capacity: </b> {{ report.capacity }}</li>
                <li><b>New SP: </b> {{ report.newSP }}</li>
                <li><b>Carried SP: </b> {{ report.carriedSP }}</li>
                <li><b>Total SP: </b> {{ report.totalSP }}</li>

                <li><b> Sprint Goal: </b> {{ report.sprintGoal }}</li>
                <li><b> Created Date: </b>{{ report.createdDate }}</li>
            </ul>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { reportService } from '../services/report-service';
import { dateHelper } from '../helpers/date-helper';

// interface Member {
//     id: number;
//     username: string;
// }

interface Report {
    sprintStartDate: Date;
    sprintEndDate: Date;
    teamMembers: Member[];
    teamSize: number;
    absentDays: number;
    workDays: number;
    capacity: number;
    newSP: number;
    carriedSP: number;
    totalSP: number;
    sprintGoal: string;
    createdByUser: any;
    createdDate: Date;
}

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
    },
});
</script>

<style>
.report-style {
    background-color: whitesmoke;
    box-shadow: rgba(0, 0, 0, 0.25) 0px 54px 55px,
        rgba(0, 0, 0, 0.12) 0px -12px 30px, rgba(0, 0, 0, 0.12) 0px 4px 6px,
        rgba(0, 0, 0, 0.17) 0px 12px 13px, rgba(0, 0, 0, 0.09) 0px -3px 5px;
    width: 50%;
    padding: 50px;
    justify-content: center;
    margin-left: auto;
    margin-right: auto;
    margin-top: 1rem;
}

a {
    cursor: pointer;
    position: absolute;
    top: 0;
    right: 0;
    margin: 2rem;
}

h1 {
    text-decoration: underline;
}

a:hover {
    text-decoration: underline;
}

li {
    text-align: -webkit-auto;
    margin-bottom: 2rem;
    box-shadow: rgba(0, 0, 0, 0.15) 1.95px 1.95px 2.6px;
}

#end-date-txt {
    font-weight: lighter;
}

input #start-date-field {
    width: initial;
    display: flex;
    text-align: center !important;
    cursor: pointer;
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
