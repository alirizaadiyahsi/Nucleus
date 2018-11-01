import AppComponentBase from '@/infrastructure/core/app-component-base';
import { Component, Watch } from 'vue-property-decorator';

@Component({
    //components: {
    //    CreateOrUpdateRoleModalComponent: require('./create-or-update-role-modal.vue').default
    //}
})
export default class RoleListComponent extends AppComponentBase {
    @Watch('pagination')
    onPaginationChanged() {
        this.getRoles();
    }

    public loading = true;
    public pagination = {
        rowsPerPage: 10
    };
    public headers = [
        { text: 'Role Name', value: 'name' }
    ];

    public pagedListOfRoleListDto: IPagedList<IRoleListOutput> = {
        totalCount: 0,
        items: []
    };

    public mounted() {
        this.getRoles();
    }

    //public selectedRoleId?: string;

    //public setGetRoleForCreateOrUpdateInput(item: IRoleDto) {
    //    if (item) {
    //        this.selectedRoleId = item.id;
    //    } else {
    //        this.selectedRoleId = undefined;
    //    }
    //}

    //public changePage() {
    //    this.getRoles();
    //}

    public getRoles() {
        this.loading = true;
        const { sortBy, descending, page, rowsPerPage }: any = this.pagination;
        const roleListInput: IPagedListInput = {
            filter: '',
            pageIndex: page - 1,
            pageSize: rowsPerPage
        };

        if (sortBy) {
            roleListInput.sorting = sortBy + (descending ? ' desc' : '');
        }
        const query = '?' + this.queryString.stringify(roleListInput);
        this.appService.get<IPagedList<IRoleListOutput>>('/api/role/getRoles' + query).then((response) => {
            this.pagedListOfRoleListDto = response.content as IPagedList<IRoleListOutput>;
            this.loading = false;
        });
    }

    //public remove(id: string) {
    //    this.swalConfirm('Are you sure want to delete?')
    //        .then((result) => {
    //            if (result.value) {
    //                const query = '?id=' + id;
    //                this.appService.delete('/api/role/deleteRole' + query)
    //                    .then((response) => {
    //                        if (!response.isError) {
    //                            this.swalToast(2000, 'success', 'Successfully deleted!');
    //                            this.getRoles();
    //                        } else {
    //                            this.swalAlert('error', response.errors.join('<br>'));
    //                        }
    //                    });
    //            }
    //        });
    //}
}