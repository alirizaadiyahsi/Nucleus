import NucleusComponentBase from '@/shared/application/nucleus-component-base';
import { Component, Watch } from 'vue-property-decorator';
import Guid from '@/shared/helpers/guid-helper';

@Component
export default class UserListComponent extends NucleusComponentBase {
    refs = this.$refs as any;
    loading = true;
    options = {};
    search = '';
    dialog = false;
    formTitle = '';
    errors: INameValueDto[] = [];
    allRoles: IRoleDto[] = [];
    isEdit = false;
    selectAll = false;

    get headers() {
        return [
            { text: this.$t('UserName'), value: 'userName' },
            { text: this.$t('Email'), value: 'email' },
            { text: this.$t('Actions'), value: 'action', sortable: false }
        ];
    }

    createOrUpdateUserInput = {
        grantedRoleIds: [],
        user: {} as IUserDto
    } as ICreateOrUpdateUserInput;

    pagedListOfUserListDto: IPagedList<IPagedListInput> = {
        totalCount: 0,
        items: []
    };

    @Watch('options')
    onPaginationChanged() {
        this.getUsers();
    }

    @Watch('search')
    onSearchChanged() {
        this.getUsers();
    }

    mounted() {
        this.getUsers();
    }

    editUser(id: string) {
        this.formTitle = id ? this.$t('EditUser').toString() : this.$t('NewUser').toString();
        this.isEdit = id ? true : false;
        this.errors = [];
        this.nucleusService.get<IGetUserForCreateOrUpdateOutput>('/api/users/' + (id ? id : Guid.empty))
            .then((response) => {
                const result = response.content as IGetUserForCreateOrUpdateOutput;
                this.allRoles = result.allRoles;
                this.createOrUpdateUserInput = {
                    grantedRoleIds: result.grantedRoleIds,
                    user: result.user
                };
                this.dialog = true;
            });
    }

    deleteUser(id: string) {
        this.swalConfirm(this.$t('AreYouSureToDelete').toString())
            .then((result) => {
                if (result.value) {
                    this.nucleusService.delete('/api/users?id=' + id)
                        .then((response) => {
                            debugger;
                            if (!response.isError) {
                                this.swalToast(2000, 'success', this.$t('Successful').toString());
                                this.getUsers();
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
            this.nucleusService.postOrPut<void>('/api/users',
                this.createOrUpdateUserInput as ICreateOrUpdateUserInput,
                this.createOrUpdateUserInput.user.id)
                .then((response) => {
                    if (!response.isError) {
                        this.swalToast(2000, 'success', this.$t('Successful').toString());
                        this.dialog = false;
                        this.getUsers();
                    } else {
                        this.errors = response.errors;
                    }
                });
        }
    }

    getUsers() {
        this.loading = true;
        const { sortBy, sortDesc, page, itemsPerPage }: any = this.options;
        const userListInput: IPagedListInput = {
            filter: this.search,
            pageIndex: page - 1,
            pageSize: itemsPerPage
        };

        if (sortBy.length > 0 && sortBy[0]) {
            userListInput.sortBy = sortBy + ((sortDesc.length > 0 && sortDesc[0]) ? ' desc' : '');
        }

        const query = '?' + this.queryString.stringify(userListInput);
        this.nucleusService.get<IPagedList<IPagedListInput>>('/api/users' + query, false).then((response) => {
            this.pagedListOfUserListDto = response.content as IPagedList<IPagedListInput>;
            this.loading = false;
        });
    }

    selectAllRoles() {
        this.createOrUpdateUserInput.grantedRoleIds = [];
        if (this.selectAll) {
            this.createOrUpdateUserInput.grantedRoleIds = ((this.allRoles.map((roles) => roles.id)) as string[]);
        }
    }
}