import { __decorate } from "tslib";
import NucleusComponentBase from '@/shared/application/nucleus-component-base';
import { Component, Watch } from 'vue-property-decorator';
import Guid from '@/shared/helpers/guid-helper';
let UserListComponent = class UserListComponent extends NucleusComponentBase {
    constructor() {
        super(...arguments);
        this.refs = this.$refs;
        this.loading = true;
        this.options = {};
        this.search = '';
        this.dialog = false;
        this.formTitle = '';
        this.errors = [];
        this.allRoles = [];
        this.isEdit = false;
        this.selectAll = false;
        this.createOrUpdateUserInput = {
            grantedRoleIds: [],
            user: {}
        };
        this.pagedListOfUserListDto = {
            totalCount: 0,
            items: []
        };
    }
    get headers() {
        return [
            { text: this.$t('UserName'), value: 'userName' },
            { text: this.$t('Email'), value: 'email' },
            { text: this.$t('Actions'), value: 'action', sortable: false }
        ];
    }
    onPaginationChanged() {
        this.getUsers();
    }
    onSearchChanged() {
        this.getUsers();
    }
    mounted() {
        this.getUsers();
    }
    editUser(id) {
        this.formTitle = id ? this.$t('EditUser').toString() : this.$t('NewUser').toString();
        this.isEdit = id ? true : false;
        this.errors = [];
        this.nucleusService.get('/api/users/' + (id ? id : Guid.empty))
            .then((response) => {
            const result = response.content;
            this.allRoles = result.allRoles;
            this.createOrUpdateUserInput = {
                grantedRoleIds: result.grantedRoleIds,
                user: result.user
            };
            this.dialog = true;
        });
    }
    deleteUser(id) {
        this.swalConfirm(this.$t('AreYouSureToDelete').toString())
            .then((result) => {
            if (result.value) {
                this.nucleusService.delete('/api/users?id=' + id)
                    .then((response) => {
                    debugger;
                    if (!response.isError) {
                        this.swalToast(2000, 'success', this.$t('Successful').toString());
                        this.getUsers();
                    }
                    else {
                        var errorText = "";
                        response.errors.forEach(error => {
                            errorText += this.$t(error.name) + '<br>';
                        });
                        this.swalAlert('error', errorText);
                    }
                });
            }
        });
    }
    save() {
        if (this.refs.form.validate()) {
            this.errors = [];
            this.nucleusService.postOrPut('/api/users', this.createOrUpdateUserInput, this.createOrUpdateUserInput.user.id)
                .then((response) => {
                if (!response.isError) {
                    this.swalToast(2000, 'success', this.$t('Successful').toString());
                    this.dialog = false;
                    this.getUsers();
                }
                else {
                    this.errors = response.errors;
                }
            });
        }
    }
    getUsers() {
        this.loading = true;
        const { sortBy, sortDesc, page, itemsPerPage } = this.options;
        const userListInput = {
            filter: this.search,
            pageIndex: page - 1,
            pageSize: itemsPerPage
        };
        if (sortBy.length > 0 && sortBy[0]) {
            userListInput.sortBy = sortBy + ((sortDesc.length > 0 && sortDesc[0]) ? ' desc' : '');
        }
        const query = '?' + this.queryString.stringify(userListInput);
        this.nucleusService.get('/api/users' + query, false).then((response) => {
            this.pagedListOfUserListDto = response.content;
            this.loading = false;
        });
    }
    selectAllRoles() {
        this.createOrUpdateUserInput.grantedRoleIds = [];
        if (this.selectAll) {
            this.createOrUpdateUserInput.grantedRoleIds = (this.allRoles.map((roles) => roles.id));
        }
    }
};
__decorate([
    Watch('options')
], UserListComponent.prototype, "onPaginationChanged", null);
__decorate([
    Watch('search')
], UserListComponent.prototype, "onSearchChanged", null);
UserListComponent = __decorate([
    Component
], UserListComponent);
export default UserListComponent;
//# sourceMappingURL=user-list.js.map