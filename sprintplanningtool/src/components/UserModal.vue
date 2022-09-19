<template>
    <div>
        <!-- First modal -->
        <vue-final-modal
            v-model="showModal"
            classes="modal-container"
            content-class="modal-content"
        >
            <button class="modal__close" @click="onClose">x</button>
            <span class="modal__title">User Information</span>
            <div class="modal__content">
                <p>
                    {{ user }}
                </p>
            </div>
            <div class="modal__action">
                <b-button highlight @click="showConfirmModal = true"
                    >confirm</b-button
                >
                <b-button @click="onClose">cancel</b-button>
            </div>
        </vue-final-modal>

        <!-- Second modal -->
        <vue-final-modal
            v-model="showConfirmModal"
            classes="modal-container"
            content-class="modal-content"
        >
            <button class="modal__close" @click="showConfirmModal = false">
                x
            </button>
            <span class="modal__title">Confirm</span>
            <div class="modal__content">Confirm to submit.</div>
            <div class="modal__action">
                <b-button variant="primary" @click="confirm">confirm</b-button>
                <b-button variant="danger" @click="showConfirmModal = false"
                    >cancel</b-button
                >
            </div>
        </vue-final-modal>
    </div>
</template>

<script lang="ts">
interface Data {
    showModal: boolean;
    showConfirmModal: boolean;
}

export default {
    data(): Data {
        return {
            showModal: true,
            showConfirmModal: false,
        };
    },
    props: {
        user: {},
    },
    methods: {
        confirm() {
            // @ts-ignore
            this.showConfirmModal = false;
            // @ts-ignore
            this.showModal = false;

            this.$emit('isModalOpen', false);
        },
        onClose() {
            this.$emit('isModalOpen', false);
        },
    },
};
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
}
.modal__title {
    margin: 0 2rem 0.5rem 0;
    font-size: 1.5rem;
    font-weight: 700;
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
</style>
