import NucleusComponentBase from '@/shared/application/nucleus-component-base';
import { Component, Watch } from 'vue-property-decorator';
import Guid from '@/shared/helpers/guid-helper';

@Component
export default class UserListComponent extends NucleusComponentBase {
    public refs = this.$refs as any;
    public loading = true;
    public options = {};
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
            { text: this.$t('Actions'), value: 'action', sortable: false }
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

    @Watch('options')
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
        this.nucleusService.get<IGetUserForCreateOrUpdateOutput>('/api/users/' + (id ? id : Guid.empty))
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
                    this.nucleusService.delete('/api/users?id=' + id)
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
            if (this.createOrUpdateUserInput.user.id === Guid.empty) {
                this.nucleusService.post<void>('/api/users',
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
            } else {
                this.nucleusService.put<void>('/api/users',
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
    }

    public getUsers() {
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

    public selectAllRoles() {
        this.createOrUpdateUserInput.grantedRoleIds = [];
        if (this.selectAll) {
            this.createOrUpdateUserInput.grantedRoleIds = ((this.allRoles.map((roles) => roles.id)) as string[]);
        }
    }
}