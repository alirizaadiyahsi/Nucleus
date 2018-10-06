<template>
    <form v-on:submit.prevent="onSubmit">
        <b-modal id="modalCreateRole" ref="modalCreateRole" title="Create Role">
            <div v-for="error in errors">
                <div class="alert alert-danger" role="alert">
                    {{error.value}}
                </div>
            </div>
            <div class="form-group">
                <input type="text" v-model="name" class="form-control" placeholder="Role name" required>
            </div>

            <div slot="modal-footer" class="w-100">
                <button type="submit" class="btn btn-primary float-right">
                    Create
                </button>
            </div>
        </b-modal>
    </form>
</template>

<script src="./create-role-modal.ts"></script>
