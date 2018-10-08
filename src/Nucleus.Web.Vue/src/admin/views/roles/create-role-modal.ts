import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import RoleAppService from '../../../services/role-app-service';

@Component
export default class CreateRoleModalComponent extends Vue {

    public name = '';
    public errors: IErrorResponse[] = [];

    public onSubmit() {
        const roleAppService = new RoleAppService();
        // todo: select permission when creating role
        const createOrEditRoleInput: ICreateOrEditRoleInput = { name: this.name, permissions: [] };

        roleAppService.addRole(createOrEditRoleInput).then((response) => {
            if (!response.isError) {
                this.$refs.modalCreateRole.hide();
                this.$parent.getRoles();
            } else {
                this.errors = response.errors;
            }
        });
    }
}
