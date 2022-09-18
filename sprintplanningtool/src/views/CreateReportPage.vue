<template>
    <div>
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
                        <label for="sprint-date">Start date: </label>
                        <datepicker
                            name="startDate"
                            id="sprint-date"
                            v-model="sprintStartDate"
                            inputFormat="dd-MM-yyyy"
                            rules="required"
                        />
                    </li>

                    <li>
                        <label for="end-date" class="label-style">
                            <h4>End date:</h4></label
                        >
                        <p class="display-info">
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
                        <table id="team-table">
                            <thead>
                                <tr>
                                    <th>Team member(s)</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr
                                    v-for="addedMember in addedMembers"
                                    :key="addedMember.id"
                                >
                                    <td>
                                        {{ addedMember.username }}
                                        <button
                                            class="remove-btn"
                                            @click="removeMember(addedMember)"
                                        >
                                            remove
                                        </button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                        <label for="team-size">Add team member:</label>
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

                        <button @click="addMember" class="btn" id="add-btn">
                            Add member
                        </button>
                    </li>

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
                        <label class="label-inline">Team Capacity (%): </label>
                        <h4>{{ Math.round(capacity) }}%</h4>
                    </li>

                    <li>
                        <h3>Planned Velocity</h3>
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

                    <li>
                        <ErrorMessage name="sprintGoal" class="invalid-field" />
                        <label for="sprint-goal"><h3>Sprint Goal</h3></label>
                        <Field
                            name="sprintGoal"
                            v-model="sprintGoal"
                            rules="max:50"
                        />
                    </li>

                    <div class="container">
                        <button class="btn centre-btn" type="submit">
                            Create a report
                        </button>
                        <button class="btn centre-btn">Clear</button>
                    </div>
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

interface Member {
    id: number;
    username: string;
}

interface Data {
    schema: object;
    sprintStartDate: Date;
    sprintEndDate: Date | null;
    members: Member[];
    selectedMember: Member | any;
    addedMembers: Member[];
    teamSize: number;
    absentDays: string;
    workDays: number;
    capacity: number;
    newSP: string;
    carriedSP: string;
    totalSP: number;
    sprintGoal: string;
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
            sprintStartDate: new Date(),
            sprintEndDate: null,
            members: [],
            selectedMember: { id: 0, username: '' },
            addedMembers: [],
            teamSize: 0,
            absentDays: '0',
            workDays: 0,
            capacity: 0,
            newSP: '0',
            carriedSP: '0',
            totalSP: 0,
            sprintGoal: '',
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
        addWeeks(numOfWeeks: number, date = new Date()) {
            const dateCopy = new Date(date.getTime());

            dateCopy.setDate(dateCopy.getDate() + numOfWeeks * 7);

            return dateCopy;
        },
        async getUsers() {
            const users = await userService
                .getAll()
                .catch((error) => console.log(error));

            users.forEach((user: any) => {
                this.members.push(user);
            });
        },
        onSubmit() {
            console.log('Submit clicked');
        },
        onSelectChange() {
            // Assign property of the selected value. Each username in the list is unique.
            this.members.forEach((member) => {
                if (member.username === this.selectedMember[0]) {
                    this.selectedMember = member;
                }
            });
        },
        addMember() {
            if (this.selectedMember.username === '') {
                alert('Please select a member to add.');
                return;
            }

            // Validation check to prevent duplicated entries in the table.
            let duplicateMember = this.addedMembers.find(
                (member) => this.selectedMember.id === member.id
            );

            if (duplicateMember) {
                alert('This member is already added to the team.');
                return;
            }

            this.addedMembers.push(this.selectedMember);
            this.teamSize = this.addedMembers.length;
            this.calculateCapacity();
        },
        removeMember(member: Member) {
            this.addedMembers = this.addedMembers.filter(
                (currentMember) => currentMember.id != member.id
            );
            this.teamSize = this.addedMembers.length;
            this.calculateCapacity();
        },
        calculateCapacity() {
            this.workDays = this.addedMembers.length * 10;

            if (this.workDays === 0) {
                return (this.capacity = 0);
            }

            let absentPercentage =
                (parseInt(this.absentDays) / this.workDays) * 100;

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
    },
});
</script>

<style>
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

#sprint-report-form {
    padding: 2rem;
    display: flex;
    font-size: unset;
    font-weight: bold;
    width: 500px;
    display: flex;
    justify-content: center;
}

#sprint-report-form input {
    margin-top: 0.5rem;
    display: flex;
}

#sprint-date {
    width: initial;
    display: flex;
    margin-right: auto;
    margin-top: 0.7rem;
    text-align: center;
    cursor: pointer;
}

#sprint-report-form {
    font-size: small;
    font-weight: bold;
}

#calendar {
    position: relative;
}

li {
    text-align: -webkit-auto;
}

.number-input {
    width: 20%;
}

#team-size-select {
    width: 50%;
    margin-top: 1rem;
    margin-bottom: 1rem;
}

#team-table {
    width: 50%;
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

.remove-btn {
    background-color: #f74545;
    color: white;
    border: #8b2447 solid 2px;
    border-radius: 4px;
    cursor: pointer;
    margin-left: 5rem;
    padding: 0.2rem 0.3rem;
}

.label-style {
    display: inline;
}

.ul-inline {
    display: -webkit-box;
}

#total-sp-header {
    text-align: center;
    border: black solid 2px;
    padding: 10px;
    width: fit-content;
    margin-left: auto;
    margin-right: auto;
}

#total-sp {
    font-weight: bold;
}

.report-btn {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100%;
}

.display-info {
    border: black solid 1px;
    padding: 5px 55px;
    background-color: white;
    width: fit-content;
    margin-bottom: 2rem;
}

#add-btn {
    margin-bottom: 2rem;
}

.centre-btn {
    display: table-cell;
    vertical-align: bottom;
}

.container {
    display: table;
    text-align: center;
    width: 100%;
    margin-top: 1.5rem;
}
</style>
