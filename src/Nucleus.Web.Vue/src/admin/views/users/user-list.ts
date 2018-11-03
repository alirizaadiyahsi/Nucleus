import AppComponentBase from '@/infrastructure/core/app-component-base';
import { Component, Watch } from 'vue-property-decorator';

@Component
export default class UserListComponent extends AppComponentBase {
    public loading = true;
    public pagination = {};

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

    public getUsers() {
        this.loading = true;
        const { sortBy, descending, page, rowsPerPage }: any = this.pagination;
        const userListInput: IUserListInput = {
            filter: '',
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

    public deleteRole(id: string) {
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

    public isAdminUser(userName: string) {
        return userName === this.appConsts.userManagement.adminUserName;
    }
}