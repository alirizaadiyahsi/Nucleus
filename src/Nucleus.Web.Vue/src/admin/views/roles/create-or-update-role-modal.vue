<template>
    <form v-on:submit.prevent="onSubmit()" novalidate>
        <b-modal id="modalCreateOrUpdateRole" ref="modalCreateOrUpdateRole" v-bind:title="isUpdate ? 'Update Role' : 'Create Role'" @shown="createOrUpdateRoleModalShown">
            <div v-for="error in errors" :key="error.name">
                <div class="alert alert-danger" role="alert">
                    {{error.value}}
                </div>
            </div>
            <b-tabs>
                <b-tab title="Role" active class="pt-3">
                    <div class="form-group">
                        <input type="text" v-model="createOrUpdateRoleInput.role.name" class="form-control" placeholder="Role name" required>
                    </div>
                </b-tab>
                <b-tab title="Permissions" class="pt-3">
                    <b-form-group>
                        <b-form-checkbox-group stacked
                                               name="permissions"
                                               v-model="createOrUpdateRoleInput.grantedPermissionIds"
                                               :options="getRoleForCreateOrUpdateOutput.allPermissions"
                                               value-field="id"
                                               text-field="displayName">
                        </b-form-checkbox-group>
                    </b-form-group>
                </b-tab>
            </b-tabs>

            <div slot="modal-footer" class="w-100">
                <button type="submit" class="btn btn-primary float-right">
                    <template v-if="isUpdate">
                        Update
                    </template>
                    <template v-else>
                        Create
                    </template>
                </button>
            </div>
        </b-modal>
    </form>
</template>

<script src="./create-or-update-role-modal.ts"></script>
