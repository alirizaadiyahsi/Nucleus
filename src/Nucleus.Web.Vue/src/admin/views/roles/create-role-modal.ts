import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import RoleAppService from '../../../services/role-app-service';
import PermissionAppService from '../../../services/permission-app-service';

@Component
export default class CreateRoleModalComponent extends Vue {

    public createOrEditRoleModel: ICreateOrEditRoleInput = {
        permissionIds: [],
        name: ''
    };
    public errors: IErrorResponse[] = [];
    public allPermissions: IPermissionDto[] = [];
    public roleAppService: RoleAppService = new RoleAppService;
    public permissionAppService: PermissionAppService = new PermissionAppService;

    public mounted() {
        this.permissionAppService.getAllPermissions().then((response) => {
            this.allPermissions = response.content as IPermissionDto[];
        });
    }

    public onSubmit() {
        this.roleAppService.addRole(this.createOrEditRoleModel).then((response) => {
            if (!response.isError) {
                this.$refs.modalCreateRole.hide();
                this.$parent.getRoles();
            } else {
                this.errors = response.errors;
            }
        });
    }
}
