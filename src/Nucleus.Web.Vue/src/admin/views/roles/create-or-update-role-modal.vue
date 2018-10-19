<template>
    <form v-on:submit.prevent="onSubmit">
        <b-modal id="modalCreateRole" ref="modalCreateRole" title="Create Role" @shown="createRoleModalShown">
            <div v-for="error in errors">
                <div class="alert alert-danger" role="alert">
                    {{error.value}}
                </div>
            </div>

            <b-tabs>
                <b-tab title="Role" active class="pt-3">
                    <div class="form-group">
                        <input type="text" v-model="createOrUpdateRoleModel.name" class="form-control" placeholder="Role name" required>
                    </div>
                </b-tab>
                <b-tab title="Permissions" class="pt-3">
                    <b-form-group>
                        <b-form-checkbox-group stacked
                                               name="permissions"
                                               v-model="createOrUpdateRoleModel.permissionIds"
                                               :options="allPermissions"
                                               value-field="id"
                                               text-field="displayName">
                        </b-form-checkbox-group>
                    </b-form-group>
                </b-tab>
            </b-tabs>

            <div slot="modal-footer" class="w-100">
                <button type="submit" class="btn btn-primary float-right">
                    Create
                </button>
            </div>
        </b-modal>
    </form>
</template>

<script src="./create-or-update-role-modal.ts"></script>
