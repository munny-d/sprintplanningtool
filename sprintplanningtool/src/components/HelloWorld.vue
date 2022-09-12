<template>
    <div class="">
        <h1>Sprint Planning Tool</h1>
        <button>Register</button>
        <button>Login</button>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

type Forecasts = {
    date: string;
}[];

interface Data {
    loading: boolean;
    post: null | Forecasts;
}

export default defineComponent({
    data(): Data {
        return {
            loading: false,
            post: null,
        };
    },
    created() {
        // fetch the data when the view is created and the data is
        // already being observed
        this.fetchData();
    },
    watch: {
        // call again the method if the route changes
        $route: 'fetchData',
    },
    methods: {
        fetchData(): void {
            this.post = null;
            this.loading = true;

            fetch('weatherforecast')
                .then((r) => r.json())
                .then((json) => {
                    this.post = json as Forecasts;
                    this.loading = false;
                    return;
                });
        },
    },
});
</script>
