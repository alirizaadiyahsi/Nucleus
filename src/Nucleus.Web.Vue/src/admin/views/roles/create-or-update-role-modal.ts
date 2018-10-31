import AppComponentBase from '@/infrastructure/core/app-component-base';
import { Component } from 'vue-property-decorator';

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
            isSystemDefault: false
        } as IRoleDto
    } as ICreateOrUpdateRoleInput;
    public errors: INameValueDto[] = [];

    public createOrUpdateRoleModalShown() {
        this.appService.get<IGetRoleForCreateOrUpdateOutput>('/api/role/GetRoleForCreateOrUpdate?id=' +
            this.parent.selectedRoleId)
            .then((response) => {
                const result = response.content as IGetRoleForCreateOrUpdateOutput;
                this.isUpdate = result.role.name != null;
                this.getRoleForCreateOrUpdateOutput = result;
                this.createOrUpdateRoleInput = {
                    grantedPermissionIds: result.grantedPermissionIds,
                    role: result.role
                };
            });
    }

    public onSubmit() {
        this.errors = [];
        if (this.createOrUpdateRoleInput.role.name) {
            this.appService.post<void>('/api/role/createOrUpdateRole',
                this.createOrUpdateRoleInput as ICreateOrUpdateRoleInput)
                .then((response) => {
                    if (!response.isError) {
                        this.swalToast(2000, 'success', 'Successfully ' + (this.isUpdate ? 'updated!' : 'created!'));
                        this.refs.modalCreateOrUpdateRole.hide();
                        this.parent.getRoles();
                    } else {
                        this.errors = response.errors;
                    }
                });
        } else {
            this.errors.push({ name: 'Required', value: 'RoleName is required' });
        }
    }
}