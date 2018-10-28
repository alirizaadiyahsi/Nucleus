import AppComponentBase from '@/infrastructure/core/app-component-base';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        CreateOrUpdateRoleModalComponent: require('./create-or-update-role-modal.vue').default,
    },
})
export default class RoleListComponent extends AppComponentBase {
    public currentPage = 1;
    public selectedRoleId?: string;

    public pagedListOfRoleListDto: IPagedList<IRoleListOutput> = {
        totalCount: 0,
        items: [],
    };

    public mounted() {
        this.getRoles();
    }

    public setGetRoleForCreateOrUpdateInput(item: IRoleDto) {
        if (item) {
            this.selectedRoleId = item.id;
        } else {
            this.selectedRoleId = undefined;
        }
    }

    public changePage() {
        this.getRoles();
    }

    public getRoles() {
        const roleListInput: IRoleListInput = {
            filter: '',
            pageIndex: this.currentPage - 1,
            pageSize: 10,
        };
        const query = '?' + this.queryString.stringify(roleListInput);
        this.appService.get<IPagedList<IRoleListOutput>>('/api/role/getRoles' + query).then((response) => {
            this.pagedListOfRoleListDto = response.content as IPagedList<IRoleListOutput>;
        });
    }

    public remove(id: string) {
        this.swalConfirm('Are you sure want to delete?')
            .then((result) => {
                if (result.value) {
                    const query = '?id=' + id;
                    this.appService.delete('/api/role/deleteRole' + query)
                        .then((response) => {
                            if (!response.isError) {
                                this.swalToast(2000, 'success', 'Successfully deleted!');
                                this.getRoles();
                            } else {
                                this.swalAlert('error', response.errors.join('<br>'));
                            }
                        });
                }
            });
    }
}
