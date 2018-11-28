import NucleusComponentBase from '@/shared/application/nucleus-component-base';
import { Component, Watch } from 'vue-property-decorator';

@Component
export default class UserListComponent extends NucleusComponentBase {
    public refs = this.$refs as any;
    public loading = true;
    public pagination = {};
    public search = '';
    public dialog = false;
    public formTitle = '';
    public errors: INameValueDto[] = [];
    public allRoles: IRoleDto[] = [];
    public isEdit = false;
    public selectAll = false;

    get headers() {
        return [
            { text: this.$t('UserName'), value: 'userName' },
            { text: this.$t('Email'), value: 'email' },
            { text: this.$t('Actions'), value: '', sortable: false }
        ];
    }

    public createOrUpdateUserInput = {
        grantedRoleIds: [],
        user: {} as IUserDto
    } as ICreateOrUpdateUserInput;

    public pagedListOfUserListDto: IPagedList<IPagedListInput> = {
        totalCount: 0,
        items: []
    };

    @Watch('pagination')
    public onPaginationChanged() {
        this.getUsers();
    }

    @Watch('search')
    public onSearchChanged() {
        this.getUsers();
    }

    public mounted() {
        this.getUsers();
    }

    public editUser(id: string) {
        this.dialog = true;
        this.formTitle = id ? this.$t('EditUser').toString() : this.$t('NewUser').toString();
        this.isEdit = id ? true : false;
        this.errors = [];
        this.nucleusService.get<IGetUserForCreateOrUpdateOutput>('/api/user/GetUserForCreateOrUpdate?id=' + id)
            .then((response) => {
                const result = response.content as IGetUserForCreateOrUpdateOutput;
                this.allRoles = result.allRoles;
                this.createOrUpdateUserInput = {
                    grantedRoleIds: result.grantedRoleIds,
                    user: result.user
                };
            });
    }

    public deleteUser(id: string) {
        this.swalConfirm(this.$t('AreYouSureToDelete').toString())
            .then((result) => {
                if (result.value) {
                    const query = '?id=' + id;

                    this.nucleusService.delete('/api/user/deleteUser' + query)
                        .then((response) => {
                            if (!response.isError) {
                                this.swalToast(2000, 'success', this.$t('Successful').toString());
                                this.getUsers();
                            } else {
                                this.swalAlert('error', response.errors.join('<br>'));
                            }
                        });
                }
            });
    }

    public save() {
        if (this.refs.form.validate()) {
            this.errors = [];
            this.nucleusService.post<void>('/api/user/createOrUpdateUser',
                this.createOrUpdateUserInput as ICreateOrUpdateUserInput)
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

    public getUsers() {
        this.loading = true;
        const { sortBy, descending, page, rowsPerPage }: any = this.pagination;
        const userListInput: IPagedListInput = {
            filter: this.search,
            pageIndex: page - 1,
            pageSize: rowsPerPage
        };

        if (sortBy) {
            userListInput.sortBy = sortBy + (descending ? ' desc' : '');
        }

        const query = '?' + this.queryString.stringify(userListInput);
        this.nucleusService.get<IPagedList<IPagedListInput>>('/api/user/getUsers' + query).then((response) => {
            this.pagedListOfUserListDto = response.content as IPagedList<IPagedListInput>;
            this.loading = false;
        });
    }

    public isAdminUser(userName: string) {
        return userName.includes('admin');
    }

    public selectAllRoles() {
        this.createOrUpdateUserInput.grantedRoleIds = [];
        if (this.selectAll) {
            this.createOrUpdateUserInput.grantedRoleIds = ((this.allRoles.map(roles => roles.id)) as string[]);
        }
    }
}