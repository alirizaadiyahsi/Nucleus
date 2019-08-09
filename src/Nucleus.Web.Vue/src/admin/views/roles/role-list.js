import * as tslib_1 from "tslib";
import NucleusComponentBase from '@/shared/application/nucleus-component-base';
import { Component, Watch } from 'vue-property-decorator';
import Guid from '@/shared/helpers/guid-helper';
let RoleListComponent = class RoleListComponent extends NucleusComponentBase {
    constructor() {
        super(...arguments);
        this.refs = this.$refs;
        this.allPermissions = [];
        this.errors = [];
        this.loading = true;
        this.dialog = false;
        this.formTitle = '';
        this.options = {};
        this.search = '';
        this.selectAll = false;
        this.createOrUpdateRoleInput = {
            grantedPermissionIds: [],
            role: {}
        };
        this.pagedListOfRoleListDto = {
            totalCount: 0,
            items: []
        };
    }
    get headers() {
        return [
            { text: this.$t('RoleName'), value: 'name' },
            { text: this.$t('Actions'), value: 'action', sortable: false }
        ];
    }
    onPaginationChanged() {
        this.getRoles();
    }
    onSearchChanged() {
        this.getRoles();
    }
    mounted() {
        this.getRoles();
    }
    editRole(id) {
        this.dialog = true;
        this.formTitle = id ? this.$t('EditRole').toString() : this.$t('NewRole').toString();
        this.errors = [];
        this.nucleusService.get('/api/roles/' + (id ? id : Guid.empty))
            .then((response) => {
            const result = response.content;
            this.allPermissions = result.allPermissions;
            this.createOrUpdateRoleInput = {
                grantedPermissionIds: result.grantedPermissionIds,
                role: result.role
            };
        });
    }
    deleteRole(id) {
        this.swalConfirm(this.$t('AreYouSureToDelete').toString())
            .then((result) => {
            if (result.value) {
                this.nucleusService.delete('/api/roles?id=' + id)
                    .then((response) => {
                    if (!response.isError) {
                        this.swalToast(2000, 'success', this.$t('Successful').toString());
                        this.getRoles();
                    }
                    else {
                        this.swalAlert('error', response.errors.join('<br>'));
                    }
                });
            }
        });
    }
    save() {
        if (this.refs.form.validate()) {
            this.errors = [];
            this.nucleusService.postOrPut('/api/roles', this.createOrUpdateRoleInput, this.createOrUpdateRoleInput.role.id)
                .then((response) => {
                if (!response.isError) {
                    this.swalToast(2000, 'success', this.$t('Successful').toString());
                    this.dialog = false;
                    this.getRoles();
                }
                else {
                    this.errors = response.errors;
                }
            });
        }
    }
    getRoles() {
        this.loading = true;
        const { sortBy, sortDesc, page, itemsPerPage } = this.options;
        const roleListInput = {
            filter: this.search,
            pageIndex: page - 1,
            pageSize: itemsPerPage
        };
        if (sortBy.length > 0 && sortBy[0]) {
            roleListInput.sortBy = sortBy + ((sortDesc.length > 0 && sortDesc[0]) ? ' desc' : '');
        }
        const query = '?' + this.queryString.stringify(roleListInput);
        this.nucleusService.get('/api/roles' + query, false).then((response) => {
            this.pagedListOfRoleListDto = response.content;
            this.loading = false;
        });
    }
    selectAllPermissions() {
        this.createOrUpdateRoleInput.grantedPermissionIds = [];
        if (this.selectAll) {
            this.createOrUpdateRoleInput.grantedPermissionIds =
                (this.allPermissions.map((permissions) => permissions.id));
        }
    }
};
tslib_1.__decorate([
    Watch('options')
], RoleListComponent.prototype, "onPaginationChanged", null);
tslib_1.__decorate([
    Watch('search')
], RoleListComponent.prototype, "onSearchChanged", null);
RoleListComponent = tslib_1.__decorate([
    Component
], RoleListComponent);
export default RoleListComponent;
//# sourceMappingURL=role-list.js.map