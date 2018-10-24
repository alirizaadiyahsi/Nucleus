import AppComponentBase from '@/models/shared/app-component-base';
import { Component } from 'vue-property-decorator';
import UserAppService from '@/services/users/user-app-service';
import swal from 'sweetalert2';

@Component
export default class UserListComponent extends AppComponentBase {
    public pagedListOfUserListDto: IPagedList<IUserListInput> = {
        totalCount: 0,
        items: [],
    };
    public userAppService = new UserAppService();

    public mounted() {
        this.getUsers();
    }

    public getUsers() {
        const userListInput: IUserListInput = {
            filter: '',
        };

        this.userAppService.getUsers(userListInput).then((response) => {
            this.pagedListOfUserListDto = response.content as IPagedList<IUserListInput>;
        });
    }

    public remove(id: string) {
        this.swalConfirm('Are you sure want to delete?')
            .then((result) => {
                if (result.value) {
                    this.userAppService.deleteUser(id).then((response) => {
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
}
