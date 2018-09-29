import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import UserAppService from '../../../shared/services/user-app-service';

@Component
export default class UserListComponent extends Vue {
    pagedListOfUserListDto: IPagedList<IUserListDto> = {
        totalCount: 0,
        items: []
    };
    userAppService = new UserAppService();

    mounted() {
        let userListInput: IUserListInput = {

        };

        this.userAppService.getAll(userListInput).then((response) => {
            this.pagedListOfUserListDto = response.content as IPagedList<IUserListDto>;
        });
    }
}
