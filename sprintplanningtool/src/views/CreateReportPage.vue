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
                    <ErrorMessage name="" class="" />
                    <li>
                        <label for="sprint-date">Start date: </label>
                        <datepicker
                            name="startDate"
                            id="sprint-date"
                            v-model="sprintStartDate"
                            inputFormat="dd-MM-yyyy"
                        />
                    </li>

                    <li>
                        <label for="end-sprint-date"
                            >End date:
                            {{
                                (sprintEndDate = addWeeks(
                                    2,
                                    sprintStartDate
                                )).toLocaleDateString()
                            }}
                        </label>
                    </li>

                    <li class="field">
                        <table id="team-table">
                            <thead>
                                <tr>
                                    <th>Team</th>
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
                            name="team"
                            as="select"
                            multiple
                            id="team-size-select"
                            v-model="selectedMember"
                            @change="onSelectChange"
                        >
                            <option
                                v-for="member in members"
                                :key="member.id"
                                :value="member.username"
                            >
                                {{ member.username }}
                            </option>
                        </Field>

                        <button @click="addMember">Add member</button>
                    </li>

                    <li class="field">
                        <span id="teamSize">Team Size: {{ teamSize }}</span>
                    </li>

                    <li class="field">
                        <h3>Team Capacity</h3>
                        <label for="absent-days" class="label-inline"
                            >Planned number of holiday(s)</label
                        >
                        <Field
                            name="absentDays"
                            id="absent-days"
                            type="number"
                            min="0"
                            v-model="absentDays"
                            @change="calculateCapacity"
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
                        >
                        </Field>
                        <label class="label-inline">Team Capacity (%): </label>
                        <h4>{{ capacity }}%</h4>
                    </li>

                    <li class="field">
                        <h3>Planned Velocity</h3>
                        <ul>
                            <li class="field">
                                Points from new stories:
                                <Field
                                    name="newSP"
                                    type="number"
                                    v-model="newSP"
                                    @change="calculateTotalSP"
                                    min="0"
                                />
                            </li>
                            <li class="field">
                                Points carried over:
                                <Field
                                    name="carriedSP"
                                    type="number"
                                    v-model="carriedSP"
                                    @change="calculateTotalSP"
                                    min="0"
                                />
                            </li>
                            <li class="field">Total SP: {{ totalSP }}</li>
                        </ul>
                    </li>

                    <li class="field">
                        <label for="sprint-goal"><h3>Sprint Goal</h3></label>
                        <Field name="sprintGoal" v-model="sprintGoal" />
                    </li>

                    <li>
                        <button>Create a report</button>
                        <button>Clear</button>
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
    username: yup.date().required('Date is required'),
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
            Math.round(absentPercentage);

            this.capacity = 100 - absentPercentage;
        },
        calculateTotalSP() {
            this.totalSP = parseInt(this.newSP) + parseInt(this.carriedSP);
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
}

#calendar {
    position: relative;
}

li {
    text-align: -webkit-auto;
}

.field {
    margin: 2rem;
}

#team-size-select {
    width: 100%;
    margin-top: 1rem;
    margin-bottom: 1rem;
    text-align: center;
}

#team-table {
    width: 100%;
    border-collapse: collapse;
    border-width: 2px;
    border-style: solid;
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
    margin-left: 10rem;
}

.label-inline {
    display: inline;
}
</style>
