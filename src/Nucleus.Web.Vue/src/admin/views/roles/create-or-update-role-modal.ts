import AppComponentBase from '@/models/shared/app-component-base';
import { Component } from 'vue-property-decorator';
import RoleAppService from '@/services/roles/role-app-service';

@Component
export default class CreateOrUpdateRoleModalComponent extends AppComponentBase {

    public parent: any = this.$parent;
    public refs: any = this.$refs;
    public isUpdate = false;
    public getRoleForCreateOrUpdateOutput = {};
    public createOrUpdateRoleInput = {
        grantedPermissionIds: [],
        role: {
            id: '',
            name: '',
            isSystemDefault: false,
        } as IRoleDto,
    } as ICreateOrUpdateRoleInput;
    public errors: IErrorResponse[] = [];
    public roleAppService = new RoleAppService();

    public createOrUpdateRoleModalShown() {
        this.roleAppService.getRoleForCreateOrUpdate(this.parent.selectedRoleId)
            .then((response) => {
                const result = response.content as IGetRoleForCreateOrUpdateOutput;
                this.isUpdate = result.role.name != null;
                this.getRoleForCreateOrUpdateOutput = result;
                this.createOrUpdateRoleInput = {
                    grantedPermissionIds: result.grantedPermissionIds,
                    role: result.role,
                };
            });
    }

    public onSubmit() {
        this.roleAppService.createOrUpdateRole(this.createOrUpdateRoleInput as ICreateOrUpdateRoleInput)
            .then((response) => {
                if (!response.isError) {
                    this.swalToast(2000, 'success', 'Successfully ' + (this.isUpdate ? 'updated!' : 'created!'));
                    this.refs.modalCreateOrUpdateRole.hide();
                    this.parent.getRoles();
                } else {
                    this.errors = response.errors;
                }
            });
    }
}
