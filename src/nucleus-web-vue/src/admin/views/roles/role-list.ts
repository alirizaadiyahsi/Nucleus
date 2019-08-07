import NucleusComponentBase from '@/shared/application/nucleus-component-base';
import { Component, Watch } from 'vue-property-decorator';
import Guid from '@/shared/helpers/guid-helper';

@Component
export default class RoleListComponent extends NucleusComponentBase {
    public refs = this.$refs as any;
    public allPermissions: IPermissionDto[] = [];
    public errors: INameValueDto[] = [];
    public loading = true;
    public dialog = false;
    public formTitle = '';
    public options = {};
    public search = '';
    public selectAll = false;

    get headers() {
        return [
            { text: this.$t('RoleName'), value: 'name' },
            { text: this.$t('Actions'), value: 'action', sortable: false }
        ];
    }

    public createOrUpdateRoleInput = {
        grantedPermissionIds: [],
        role: {} as IRoleDto
    } as ICreateOrUpdateRoleInput;

    public pagedListOfRoleListDto: IPagedList<IRoleListOutput> = {
        totalCount: 0,
        items: []
    };

    @Watch('options')
    public onPaginationChanged() {
        this.getRoles();
    }

    @Watch('search')
    public onSearchChanged() {
        this.getRoles();
    }

    public mounted() {
        this.getRoles();
    }

    public editRole(id: string) {
        this.dialog = true;
        this.formTitle = id ? this.$t('EditRole').toString() : this.$t('NewRole').toString();
        this.errors = [];
        this.nucleusService.get<IGetRoleForCreateOrUpdateOutput>('/api/roles/' + (id ? id : Guid.empty))
            .then((response) => {
                const result = response.content as IGetRoleForCreateOrUpdateOutput;
                this.allPermissions = result.allPermissions;
                this.createOrUpdateRoleInput = {
                    grantedPermissionIds: result.grantedPermissionIds,
                    role: result.role
                };
            });
    }

    public deleteRole(id: string) {
        this.swalConfirm(this.$t('AreYouSureToDelete').toString())
            .then((result) => {
                if (result.value) {
                    this.nucleusService.delete('/api/roles?id=' + id)
                        .then((response) => {
                            if (!response.isError) {
                                this.swalToast(2000, 'success', this.$t('Successful').toString());
                                this.getRoles();
                            } else {
                                this.swalAlert('error', response.errors.join('<br>'));
                            }
                        });
                }
            });
    }

    public save() {
        if (this.refs.form.validate()) {
            this.errors = [];
            if (this.createOrUpdateRoleInput.role.id === Guid.empty) {
                this.nucleusService.post<void>('/api/roles',
                        this.createOrUpdateRoleInput as ICreateOrUpdateRoleInput)
                    .then((response) => {
                        if (!response.isError) {
                            this.swalToast(2000, 'success', this.$t('Successful').toString());
                            this.dialog = false;
                            this.getRoles();
                        } else {
                            this.errors = response.errors;
                        }
                    });
            } else {
                this.nucleusService.put<void>('/api/roles',
                        this.createOrUpdateRoleInput as ICreateOrUpdateRoleInput)
                    .then((response) => {
                        if (!response.isError) {
                            this.swalToast(2000, 'success', this.$t('Successful').toString());
                            this.dialog = false;
                            this.getRoles();
                        } else {
                            this.errors = response.errors;
                        }
                    });
            }
        }
    }

    public getRoles() {
        this.loading = true;
        const { sortBy, sortDesc, page, itemsPerPage }: any = this.options;
        const roleListInput: IPagedListInput = {
            filter: this.search,
            pageIndex: page - 1,
            pageSize: itemsPerPage
        };

        if (sortBy.length > 0 && sortBy[0]) {
            roleListInput.sortBy = sortBy + ((sortDesc.length > 0 && sortDesc[0]) ? ' desc' : '');
        }

        const query = '?' + this.queryString.stringify(roleListInput);
        this.nucleusService.get<IPagedList<IRoleListOutput>>('/api/roles' + query, false).then((response) => {
            this.pagedListOfRoleListDto = response.content as IPagedList<IRoleListOutput>;
            this.loading = false;
        });
    }

    public selectAllPermissions() {
        this.createOrUpdateRoleInput.grantedPermissionIds = [];
        if (this.selectAll) {
            this.createOrUpdateRoleInput.grantedPermissionIds =
                ((this.allPermissions.map((permissions) => permissions.id)) as string[]);
        }
    }
}