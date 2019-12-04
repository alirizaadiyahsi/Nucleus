import NucleusComponentBase from '@/shared/application/nucleus-component-base';
import { Component, Watch } from 'vue-property-decorator';
import Guid from '@/shared/helpers/guid-helper';

@Component
export default class RoleListComponent extends NucleusComponentBase {
    refs = this.$refs as any;
    allPermissions: IPermissionDto[] = [];
    errors: INameValueDto[] = [];
    loading = true;
    dialog = false;
    formTitle = '';
    options = {};
    search = '';
    selectAll = false;

    get headers() {
        return [
            { text: this.$t('RoleName'), value: 'name' },
            { text: this.$t('Actions'), value: 'action', sortable: false }
        ];
    }

    createOrUpdateRoleInput = {
        grantedPermissionIds: [],
        role: {} as IRoleDto
    } as ICreateOrUpdateRoleInput;

    pagedListOfRoleListDto: IPagedList<IRoleListOutput> = {
        totalCount: 0,
        items: []
    };

    @Watch('options')
    onPaginationChanged() {
        this.getRoles();
    }

    @Watch('search')
    onSearchChanged() {
        this.getRoles();
    }

    mounted() {
        this.getRoles();
    }

    editRole(id: string) {
        this.formTitle = id ? this.$t('EditRole').toString() : this.$t('NewRole').toString();
        this.errors = [];
        this.nucleusService.get<IGetRoleForCreateOrUpdateOutput>('/api/roles/' + (id ? id : Guid.empty))
            .then((response) => {
                const result = response.content as IGetRoleForCreateOrUpdateOutput;
                this.allPermissions = result.allPermissions;
                this.createOrUpdateRoleInput = {
                    grantedPermissionIds: result.grantedPermissionIds,
                    role: result.role
                };
                this.dialog = true;
            });
    }

    deleteRole(id: string) {
        this.swalConfirm(this.$t('AreYouSureToDelete').toString())
            .then((result) => {
                if (result.value) {
                    this.nucleusService.delete('/api/roles?id=' + id)
                        .then((response) => {
                            if (!response.isError) {
                                this.swalToast(2000, 'success', this.$t('Successful').toString());
                                this.getRoles();
                            } else {
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
            this.nucleusService.postOrPut<void>('/api/roles',
                this.createOrUpdateRoleInput as ICreateOrUpdateRoleInput,
                this.createOrUpdateRoleInput.role.id)
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

    getRoles() {
        this.loading = true;
        const { sortBy, sortDesc, page, itemsPerPage }: any = this.options;
        const roleListInput: IPagedListInput = {
            filter: this.search,
            pageIndex: page - 1,
            pageSize: itemsPerPage
        };

        if (sortBy.length > 0 && sortBy[0]) {
            roleListInput.sortBy = sortBy + ((sortDesc.length > 0 && sortDesc[0]) ? ' desc' : '');
        }

        const query = '?' + this.queryString.stringify(roleListInput);
        this.nucleusService.get<IPagedList<IRoleListOutput>>('/api/roles' + query, false).then((response) => {
            this.pagedListOfRoleListDto = response.content as IPagedList<IRoleListOutput>;
            this.loading = false;
        });
    }

    selectAllPermissions() {
        this.createOrUpdateRoleInput.grantedPermissionIds = [];
        if (this.selectAll) {
            this.createOrUpdateRoleInput.grantedPermissionIds =
                ((this.allPermissions.map((permissions) => permissions.id)) as string[]);
        }
    }
}