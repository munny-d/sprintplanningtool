<template>
    <div>
        <header>
            <a @click="onLogout">Logout</a>
            <h1>Create Report Page</h1>
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
                        <label for="sprint-date">Start of sprint date: </label>
                        <datepicker
                            name="startDate"
                            id="sprint-date"
                            v-model="sprintStartDate"
                            inputFormat="dd-MM-yyyy"
                            @change="onChange()"
                        />
                    </li>

                    <li>
                        <label for="end-sprint-date"
                            >End of sprint date:
                            {{
                                (sprintEndDate = addWeeks(
                                    2,
                                    sprintStartDate
                                )).toLocaleDateString()
                            }}
                        </label>
                    </li>

                    <li>
                        <label for="team-size">Add team member:</label>
                        <Field name="team" multiple> </Field>
                        <button>Add member</button>
                        <button>Remove member</button>
                    </li>

                    <li><span id="teamSize">Team Size: 0</span></li>

                    <li>
                        <label for="team-velocity">Planned Velocity </label>
                        <span>Points from new stories: 10</span>
                        <span>Points carried over: 5</span>
                        <span>Total SP: 15</span>
                    </li>

                    <li>
                        <label for="sprint-goal">Sprint Goal </label>
                        <span>Complete stories</span>
                    </li>

                    <li>
                        <label for="team-capacity">Team Capacity</label>
                        <span>Total number of planned absences (days)</span>
                        <span>Total number of work days</span>
                        <span>Team Capacity (%): 80%</span>
                    </li>

                    <li>
                        <label for="team-capacity">Team Capacity</label>
                        <span>Total number of planned absences (days)</span>
                        <span>Total number of work days</span>
                        <span>Team Capacity (%): 80%</span>
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

interface Data {
    schema: object;
    sprintStartDate: Date;
    sprintEndDate: Date;
    team: string[];
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
            team: [],
        };
    },
    components: {
        Form,
        Field,
        ErrorMessage,
        Datepicker,
    },
    methods: {
        async onLogout(): Promise<void> {
            userService.logout();
            router.push({ path: '/' });
        },
        onSubmit() {
            console.log('Submit clicked');
        },
        onChange() {
            console.log('clicked');
            console.log('VALUE: ', this.sprintStartDate);

            // this.sprintEndDate = this.sprintStartDate + 12096e5;
        },
        addWeeks(numOfWeeks: number, date = new Date()) {
            const dateCopy = new Date(date.getTime());

            dateCopy.setDate(dateCopy.getDate() + numOfWeeks * 7);

            return dateCopy;
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
</style>
