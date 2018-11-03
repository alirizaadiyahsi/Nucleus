import AppComponentBase from '@/infrastructure/core/app-component-base';
import { Component, Watch } from 'vue-property-decorator';

@Component
export default class RoleListComponent extends AppComponentBase {
    public allPermissions: IPermissionDto[] = [];
    public errors: INameValueDto[] = [];
    public loading = true;
    public dialog = false;
    public formTitle = '';
    public pagination = {};
    public headers = [
        { text: 'Role Name', value: 'name' },
        { text: 'Actions', value: '', sortable: false }
    ];

    public createOrUpdateRoleInput = {
        grantedPermissionIds: [],
        role: {
            id: '',
            name: '',
            isSystemDefault: false
        } as IRoleDto
    } as ICreateOrUpdateRoleInput;

    public pagedListOfRoleListDto: IPagedList<IRoleListOutput> = {
        totalCount: 0,
        items: []
    };

    @Watch('pagination')
    public onPaginationChanged() {
        this.getRoles();
    }

    public mounted() {
        this.getRoles();
    }

    public editRole(id: string) {
        this.dialog = true;
        this.formTitle = id ? 'Edit Role' : 'Create Role';
        this.errors = [];
        this.appService.get<IGetRoleForCreateOrUpdateOutput>('/api/role/GetRoleForCreateOrUpdate?id=' + id)
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
        this.swalConfirm('Are you sure you want to delete this item?')
            .then((result) => {
                if (result.value) {
                    const query = '?id=' + id;
                    this.appService.delete('/api/role/deleteRole' + query)
                        .then((response) => {
                            if (!response.isError) {
                                this.swalToast(2000, 'success', 'Successful!');
                                this.getRoles();
                            } else {
                                this.swalAlert('error', response.errors.join('<br>'));
                            }
                        });
                }
            });
    }

    public save() {
        this.errors = [];
        this.appService.post<void>('/api/role/createOrUpdateRole',
            this.createOrUpdateRoleInput as ICreateOrUpdateRoleInput)
            .then((response) => {
                if (!response.isError) {
                    this.swalToast(2000, 'success', 'Successful!');
                    this.dialog = false;
                    this.getRoles();
                } else {
                    this.errors = response.errors;
                }
            });
    }

    public getRoles() {
        this.loading = true;
        const { sortBy, descending, page, rowsPerPage }: any = this.pagination;
        const roleListInput: IPagedListInput = {
            filter: '', // todo: add search to grid
            pageIndex: page - 1,
            pageSize: rowsPerPage
        };

        if (sortBy) {
            roleListInput.sortBy = sortBy + (descending ? ' desc' : '');
        }
        const query = '?' + this.queryString.stringify(roleListInput);
        this.appService.get<IPagedList<IRoleListOutput>>('/api/role/getRoles' + query).then((response) => {
            this.pagedListOfRoleListDto = response.content as IPagedList<IRoleListOutput>;
            this.loading = false;
        });
    }
}