import AppComponentBase from '@/infrastructure/core/app-component-base';
import { Component, Watch } from 'vue-property-decorator';

@Component
export default class UserListComponent extends AppComponentBase {
    public loading = true;
    public pagination = {};
    public search = '';
    public dialog = false;
    public formTitle = '';
    public errors: INameValueDto[] = [];
    public allRoles: IRoleDto[] = [];

    public headers = [
        { text: 'User Name', value: 'userName' },
        { text: 'E-Mail', value: 'email' },
        { text: 'Actions', value: '', sortable: false }
    ];

    public pagedListOfUserListDto: IPagedList<IUserListInput> = {
        totalCount: 0,
        items: []
    };

    @Watch('pagination')
    public onPaginationChanged() {
        this.getUsers();
    }

    public mounted() {
        this.getUsers();
    }

    public editUser(id: string) {
        this.dialog = true;
        this.formTitle = id ? 'Edit User' : 'Create User';
        this.errors = [];
        this.appService.get<IGetUserForCreateOrUpdateOutput>('/api/user/GetUserForCreateOrUpdate?id=' + id)
            .then((response) => {
                const result = response.content as IGetUserForCreateOrUpdateOutput;
                this.allRoles = result.allPermissions;
                this.createOrUpdateUserInput = {
                    grantedRoleIds: result.grantedRoleIds,
                    user: result.user
                };
            });
    }

    public deleteUser(id: string) {
        this.swalConfirm('Are you sure want to delete?')
            .then((result) => {
                if (result.value) {
                    const query = '?id=' + id;

                    this.appService.delete('/api/user/deleteUser' + query)
                        .then((response) => {
                            if (!response.isError) {
                                this.swalToast(2000, 'success', 'Successfully deleted!');
                                this.getUsers();
                            } else {
                                this.swalAlert('error', response.errors.join('<br>'));
                            }
                        });
                }
            });
    }

    public save() {
        this.errors = [];
        this.appService.post<void>('/api/user/createOrUpdateUser',
                this.createOrUpdateUserInput as ICreateOrUpdateUserInput)
            .then((response) => {
                if (!response.isError) {
                    this.swalToast(2000, 'success', 'Successful!');
                    this.dialog = false;
                    this.getUsers();
                } else {
                    this.errors = response.errors;
                }
            });
    }

    public getUsers() {
        this.loading = true;
        const { sortBy, descending, page, rowsPerPage }: any = this.pagination;
        const userListInput: IUserListInput = {
            filter: this.search,
            pageIndex: page - 1,
            pageSize: rowsPerPage
        };

        if (sortBy) {
            userListInput.sortBy = sortBy + (descending ? ' desc' : '');
        }

        const query = '?' + this.queryString.stringify(userListInput);
        this.appService.get<IPagedList<IUserListInput>>('/api/user/getUsers' + query).then((response) => {
            this.pagedListOfUserListDto = response.content as IPagedList<IUserListInput>;
            this.loading = false;
        });
    }

    public isAdminUser(userName: string) {
        return userName === this.appConsts.userManagement.adminUserName;
    }
}