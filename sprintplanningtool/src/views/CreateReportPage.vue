<template>
    <div id="report-pg">
        <header>
            <a @click="onLogout">Logout</a>
            <h1>Create a Sprint Planning Report</h1>
        </header>

        <section>
            <Form
                method="post"
                ref="sprintForm"
                @submit="onSubmit"
                :validation-schema="schema"
                id="sprint-report-form"
            >
                <ul>
                    <li>
                        <label for="sprint-date">Start date:</label>
                        <datepicker
                            name="startDate"
                            id="start-date-field"
                            v-model="sprintStartDate"
                            inputFormat="dd-MM-yyyy"
                            rules="required"
                        />
                    </li>

                    <li>
                        <label for="end-date">End date:</label>
                        <p id="end-date-field">
                            {{
                                formatDate(
                                    (sprintEndDate = addWeeks(
                                        2,
                                        sprintStartDate
                                    ))
                                )
                            }}
                        </p>
                    </li>

                    <li>
                        <ErrorMessage name="teamSize" class="invalid-field" />
                        <label for="team-size">Select team member(s):</label>
                        <Field
                            name="teamMembers"
                            as="select"
                            multiple
                            id="team-size-select"
                            v-model="selectedMember"
                            @change="onSelectChange"
                            rules="required"
                        >
                            <option
                                v-for="member in members"
                                :key="member.id"
                                :value="member.username"
                            >
                                {{ member.username }}
                            </option>
                        </Field>

                        <b-button
                            variant="success"
                            @click="addMember"
                            id="add-btn"
                            type="button"
                            title="Click to add selected member"
                        >
                            Add member
                        </b-button>
                    </li>

                    <table id="team-table">
                        <thead>
                            <tr>
                                <th>Team member(s)</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr
                                v-for="addedMember in teamMembers"
                                :key="addedMember.id"
                            >
                                <td>
                                    {{ addedMember.username }}
                                    <b-button
                                        variant="outline-danger"
                                        size="sm"
                                        id="remove-btn"
                                        title="Remove member"
                                        @click="removeMember(addedMember)"
                                    >
                                        x
                                    </b-button>
                                </td>
                            </tr>
                        </tbody>
                    </table>

                    <li>
                        <label for="">Team Size: </label>
                        <Field
                            id="team-size"
                            class="number-input"
                            name="teamSize"
                            type="number"
                            disabled
                            v-model="teamSize"
                        >
                        </Field>
                    </li>

                    <li>
                        <label for="absent-days" class="label-inline"
                            >Planned number of holiday(s)</label
                        >
                        <Field
                            name="absentDays"
                            id="absent-days"
                            type="number"
                            min="0"
                            :max="workDays"
                            v-model="absentDays"
                            @change="calculateCapacity"
                            class="number-input"
                        />
                        <label for="work-days" class="label-inline"
                            >Total number of work day(s):
                        </label>
                        <Field
                            name="workDays"
                            id="work-days"
                            type="number"
                            min="0"
                            max="10"
                            disabled
                            v-model="workDays"
                            class="number-input"
                        >
                        </Field>
                        <div id="capacity-area">
                            <label class="label-inline"
                                >Team Capacity (%):
                            </label>
                            <h4>{{ Math.round(capacity) }}%</h4>
                        </div>
                    </li>

                    <li>
                        <h3 id="velocity-txt">Planned Velocity</h3>
                        <ul class="ul-inline">
                            <li>
                                Points from new stories:
                                <Field
                                    name="newSP"
                                    type="number"
                                    v-model="newSP"
                                    @change="calculateTotalSP"
                                    min="0"
                                    class="number-input"
                                />
                            </li>
                            <li>
                                Points carried over:
                                <Field
                                    name="carriedSP"
                                    type="number"
                                    v-model="carriedSP"
                                    @change="calculateTotalSP"
                                    min="0"
                                    class="number-input"
                                />
                            </li>
                        </ul>
                    </li>

                    <li class="field">
                        <h4 id="total-sp-header">
                            Total SP:
                            <span id="total-sp-number"> {{ totalSP }} </span>
                        </h4>
                    </li>

                    <li class="sg-area">
                        <ErrorMessage name="sprintGoal" class="invalid-field" />
                        <label for="sprint-goal" class="sg-area"
                            ><h5>Sprint Goal</h5></label
                        >
                        <Field
                            name="sprintGoal"
                            v-model="sprintGoal"
                            rules="max:50"
                            id="sg-input"
                        />
                    </li>

                    <div class="container">
                        <b-button
                            variant="primary"
                            class="form-btn"
                            type="submit"
                            title="Click to create a report"
                        >
                            Submit report
                        </b-button>
                        <b-button
                            class="form-btn"
                            variant="warning"
                            title="Reset form"
                            @click="resetForm"
                            >Clear</b-button
                        >
                    </div>

                    <li>
                        <p v-if="isLoading">Creating report...</p>
                    </li>
                </ul>
            </Form>
        </section>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import router from '../router';
import { userService } from '../services/user-service';
import { Form, Field, ErrorMessage } from 'vee-validate';
import * as yup from 'yup';
import Datepicker from 'vue3-datepicker';
import { reportService } from '../services/report-service';

interface Member {
    id: number;
    username: string;
}

interface ReportReq {
    sprintStartDate: Date;
    sprintEndDate: Date;
    teamMembers: Member[];
    teamSize: number;
    absentDays: number;
    workDays: number;
    capacity: number;
    newSP: number | string;
    carriedSP: number | string;
    totalSP: number;
    sprintGoal: string;
    createdByUser: any;
}

interface Data {
    schema: object;
    displayEndDate: string | Date;
    sprintStartDate: Date;
    sprintEndDate: Date | null;
    members: Member[];
    selectedMember: Member | any;
    teamMembers: Member[];
    teamSize: number;
    absentDays: number;
    workDays: number;
    capacity: number;
    newSP: string; // this has to be a string since it's an input.
    carriedSP: string; // this has to be a string since it's an input.
    totalSP: number;
    sprintGoal: string;
    submitted: boolean;
    isLoading: boolean;
}

// A schema that uses Yup (MIT licensed JS package) for Form validation.
const schema = yup.object().shape({
    teamSize: yup.number().min(1, 'There needs to be at least 1 member added.'),
    absentDays: yup.number(),
    sprintGoal: yup
        .string()
        .matches(
            /^[\w.]/,
            'Please fill in the sprint goal. Special characters such as "!@<" are not allowed.'
        )
        .min(5, 'Sprint goal must be at least 5 characters.'),
});

export default defineComponent({
    data(): Data {
        return {
            schema,
            displayEndDate: '',
            sprintStartDate: new Date(),
            sprintEndDate: null,
            members: [],
            selectedMember: { id: 0, username: '' },
            teamMembers: [],
            teamSize: 0,
            absentDays: 0,
            workDays: 0,
            capacity: 0,
            newSP: '0',
            carriedSP: '0',
            totalSP: 0,
            sprintGoal: '',
            submitted: false,
            isLoading: false,
        };
    },
    components: {
        Form,
        Field,
        ErrorMessage,
        Datepicker,
    },
    created() {
        this.getUsers();
    },
    methods: {
        async onLogout(): Promise<void> {
            userService.logout();
            router.push({ path: '/' });
        },
        async getUsers() {
            const users = await userService
                .getAll()
                .catch((error) => console.log(error));

            users.forEach((user: any) => {
                this.members.push(user);
            });
        },
        async onSubmit(): Promise<void> {
            this.isLoading = true;

            const currentUser = JSON.parse(localStorage.getItem('user') as any);
            this.resetTeamMemberId();

            // Assign values to a request body obj needed for creating a report.
            const reportReq: ReportReq = {
                sprintStartDate: this.sprintStartDate,
                sprintEndDate: this.sprintEndDate as any,
                teamMembers: this.teamMembers,
                teamSize: this.teamSize,
                absentDays: this.absentDays,
                workDays: this.workDays,
                carriedSP: this.carriedSP,
                newSP: this.newSP,
                capacity: this.capacity,
                totalSP: this.totalSP,
                sprintGoal: this.sprintGoal,
                createdByUser: currentUser.username,
            };

            console.log('TEAM NAMES: ', reportReq.teamMembers);

            reportService
                .create(reportReq)
                .then(() => router.push('/dashboard'));

            this.isLoading = false;
        },
        onSelectChange() {
            // Assign property of the selected value. Each username in the list is unique.
            this.members.forEach((member) => {
                if (member.username === this.selectedMember[0]) {
                    this.selectedMember = member;
                }
            });
        },
        addWeeks(numOfWeeks: number, date = new Date()) {
            const dateCopy = new Date(date.getTime());
            dateCopy.setDate(dateCopy.getDate() + numOfWeeks * 7);
            return dateCopy;
        },
        addMember() {
            if (this.selectedMember.username === '') {
                alert('Please select a member to add.');
                return;
            }

            // Validation check to prevent duplicated entries in the table.
            let duplicateMember = this.teamMembers.find(
                (member) => this.selectedMember.id === member.id
            );

            if (duplicateMember) {
                alert('This member is already added to the team.');
                return;
            }

            this.teamMembers.push(this.selectedMember);
            this.teamSize = this.teamMembers.length;
            this.calculateCapacity();
        },
        removeMember(member: Member) {
            // Filter to remove selected member from the array of members.
            this.teamMembers = this.teamMembers.filter(
                (currentMember) => currentMember.id != member.id
            );
            this.teamSize = this.teamMembers.length;
            this.calculateCapacity();
        },
        calculateCapacity() {
            this.workDays = this.teamMembers.length * 10;

            if (this.workDays === 0) {
                return (this.capacity = 0);
            }

            let absentPercentage = (this.absentDays / this.workDays) * 100;

            this.capacity = 100 - absentPercentage;
        },
        calculateTotalSP() {
            this.totalSP = parseInt(this.newSP) + parseInt(this.carriedSP);
        },
        padToTwoDigits(num: number) {
            return num.toString().padStart(2, '0');
        },
        formatDate(date: Date) {
            return [
                this.padToTwoDigits(date.getDate()),
                this.padToTwoDigits(date.getMonth() + 1),
                date.getFullYear(),
            ].join('-');
        },
        resetTeamMemberId() {
            this.teamMembers.forEach((x) => (x.id = 0));
        },
        resetForm() {
            location.reload();
        },
    },
});
</script>

<style scoped>
#report-pg {
    background-color: whitesmoke;
}

a {
    cursor: pointer;
    position: absolute;
    top: 0;
    right: 0;
    margin: 2rem;
}

a:hover {
    text-decoration: underline;
}

ul {
    width: 70%;
}

li {
    text-align: -webkit-auto;
    margin-bottom: 2rem;
}

form {
    display: inline-flex;
    justify-content: center;
    margin-top: 1rem;
}

.ul-inline {
    margin-top: 2rem;
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

.v3dp__input_wrapper {
    text-align: center !important;
}
#end-date-field {
    border: black solid 1px;
    padding: 3px 5px;
    background-color: rgb(225, 225, 225);
    width: 200px;
    margin-bottom: 2rem;
    margin-top: 0.5rem;
    font-weight: lighter;
    cursor: not-allowed;
}

.sg-area {
    display: block;
}

#sg-input {
    width: 100%;
    margin-top: 0.5rem;
}

input {
    width: 100%;
    text-align: center;
}

#team-size-select {
    display: block;
    width: 70%;
    margin-top: 1rem;
    margin-bottom: 1rem;
    text-align: center;
}

#team-table {
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

#capacity-area {
    margin-top: 2rem;
}

#remove-btn {
    margin-left: 2rem;
}

.form-btn {
    margin-left: 10px;
    margin-right: 10px;
}
</style>
