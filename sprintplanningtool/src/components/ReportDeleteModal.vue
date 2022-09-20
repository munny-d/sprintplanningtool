<template>
    <div>
        <!-- Main modal -->
        <vue-final-modal
            v-model="showModal"
            classes="modal-container"
            content-class="modal-content"
        >
            <span class="modal__title">Delete the following report</span>
            <div class="modal__content">
                <p>
                    report: <b> {{ report }}</b>
                </p>

                <div class="modal__action">
                    <b-button
                        variant="outline-danger"
                        highlight
                        @click="showConfirmModal = true"
                        >confirm</b-button
                    >
                    <b-button variant="light" @click="onClose">cancel</b-button>
                </div>
            </div>
        </vue-final-modal>

        <!-- Confirm modal -->
        <vue-final-modal
            v-model="showConfirmModal"
            classes="modal-container"
            content-class="modal-content"
        >
            <span class="modal__title">FINAL WARNING</span>
            <div class="modal__content">
                Please click confirm to delete the report. If you are unsure,
                click cancel to review and ensure this is the report you wish to
                delete.
                <br />
                <b> Warning:</b> this action cannot be undone.
            </div>
            <div class="modal__action">
                <b-button variant="outline-danger">confirm</b-button>
                <b-button variant="primary" @click="showConfirmModal = false"
                    >cancel</b-button
                >
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
    margin: 0.5rem;
}
</style>
