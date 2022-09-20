<template>
    <div>
        <!-- Create navbar and import here -->
        <h1>Sprint Report</h1>
        <ul>
            <li>{{ report.sprintStartDate }}</li>
            <li>{{ report.sprintEndDate }}</li>
            <table>
                <thead>
                    <tr>
                        <th>Team member(s)</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="member in report.teamMembers" :key="member.id">
                        <td>
                            {{ member.username }}
                        </td>
                    </tr>
                </tbody>
            </table>
            <li>Team Size: {{ report.teamSize }}</li>
            <li>Work Days {{ report.workDays }}</li>
            <li>Capacity {{ report.capacity }}</li>
            <li>New SP {{ report.newSP }}</li>
            <li>Carried SP {{ report.carriedSP }}</li>
            <li>Total SP {{ report.totalSP }}</li>
            <li>Sprint Goal: {{ report.sprintGoal }}</li>
            <li>Created Date: {{ report.createdDate }}</li>
        </ul>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { reportService } from '../services/report-service';

interface Member {
    id: number;
    username: string;
}

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
}

interface Data {
    report: Report;
}

export default defineComponent({
    data() {
        return {
            report: {
                sprintStartDate: '',
                sprintEndDate: '',
                teamMembers: [],
                teamSize: 0,
                absentDays: 0,
                workDays: 0,
                capacity: 0,
                newSP: 0,
                carriedSP: 0,
                totalSP: 0,
                sprintGoal: '',
            },
        };
    },
    created() {
        this.generateReport();
    },
    methods: {
        async generateReport() {
            const report = await reportService.getLatest();
            this.report = report;

            const teamMembers = await reportService.getTeamMembers(report.id);
            this.report.teamMembers = teamMembers;

            return this.report;
        },
    },
});
</script>

<style></style>
