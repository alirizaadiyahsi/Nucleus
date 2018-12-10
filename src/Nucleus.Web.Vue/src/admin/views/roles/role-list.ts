import NucleusComponentBase from '@/shared/application/nucleus-component-base';
import { Component, Watch } from 'vue-property-decorator';

@Component
export default class RoleListComponent extends NucleusComponentBase {
    public refs = this.$refs as any;
    public allPermissions: IPermissionDto[] = [];
    public errors: INameValueDto[] = [];
    public loading = true;
    public dialog = false;
    public formTitle = '';
    public pagination = {};
    public search = '';
    public selectAll = false;

    get headers() {
        return [
            { text: this.$t('RoleName'), value: 'name' },
            { text: this.$t('Actions'), value: '', sortable: false }
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

    @Watch('pagination')
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
        this.nucleusService.get<IGetRoleForCreateOrUpdateOutput>('/api/role/GetRoleForCreateOrUpdate?id=' + id)
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
                    const query = '?id=' + id;
                    this.nucleusService.delete('/api/role/deleteRole' + query)
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
            this.nucleusService.post<void>('/api/role/createOrUpdateRole',
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

    public getRoles() {
        this.loading = true;
        const { sortBy, descending, page, rowsPerPage }: any = this.pagination;
        const roleListInput: IPagedListInput = {
            filter: this.search,
            pageIndex: page - 1,
            pageSize: rowsPerPage
        };

        if (sortBy) {
            roleListInput.sortBy = sortBy + (descending ? ' desc' : '');
        }

        const query = '?' + this.queryString.stringify(roleListInput);
        this.nucleusService.get<IPagedList<IRoleListOutput>>('/api/role/getRoles' + query).then((response) => {
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