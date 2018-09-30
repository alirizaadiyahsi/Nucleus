import * as tslib_1 from "tslib";
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import UserAppService from '../../../services/user-app-service';
let UserListComponent = class UserListComponent extends Vue {
    constructor() {
        super(...arguments);
        this.pagedListOfUserListDto = {
            totalCount: 0,
            items: []
        };
        this.userAppService = new UserAppService();
    }
    mounted() {
        let userListInput = {};
        this.userAppService.getAll(userListInput).then((response) => {
            this.pagedListOfUserListDto = response.content;
        });
    }
};
UserListComponent = tslib_1.__decorate([
    Component
], UserListComponent);
export default UserListComponent;
//# sourceMappingURL=user-list.js.map