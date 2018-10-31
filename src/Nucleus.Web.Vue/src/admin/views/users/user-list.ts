import AppComponentBase from '@/infrastructure/core/app-component-base';
import { Component } from 'vue-property-decorator';
import AppConsts from '@/infrastructure/core/app-consts';

@Component
export default class UserListComponent extends AppComponentBase {
    public currentPage = 1;

    public pagedListOfUserListDto: IPagedList<IUserListInput> = {
        totalCount: 0,
        items: []
    };

    public mounted() {
        this.getUsers();
    }

    public changePage() {
        this.getUsers();
    }

    public getUsers() {
        const userListInput: IUserListInput = {
            filter: '',
            pageIndex: this.currentPage - 1,
            pageSize: 10
        };

        const query = '?' + this.queryString.stringify(userListInput);
        this.appService.get<IPagedList<IUserListInput>>('/api/user/getUsers' + query).then((response) => {
            this.pagedListOfUserListDto = response.content as IPagedList<IUserListInput>;
        });
    }

    public remove(id: string) {
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
        return userName === AppConsts.userManagement.adminUserName;
    }
}