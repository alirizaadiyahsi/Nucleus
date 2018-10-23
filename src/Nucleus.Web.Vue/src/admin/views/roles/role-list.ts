import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import RoleAppService from '../../../services/role-app-service';
import swal from 'sweetalert2';

@Component({
    components: {
        CreateOrUpdateRoleModalComponent: require('./create-or-update-role-modal.vue').default,
    },
})
export default class RoleListComponent extends Vue {
    public pagedListOfRoleListDto: IPagedList<IRoleListOutput> = {
        totalCount: 0,
        items: [],
    };
    public selectedRoleId?: string;
    public roleAppService = new RoleAppService();

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

    public getRoles() {
        const roleListInput: IRoleListInput = {
            filter: '',
        };

        this.roleAppService.getRoles(roleListInput).then((response) => {
            this.pagedListOfRoleListDto = response.content as IPagedList<IRoleListOutput>;
        });
    }

    public remove(id: string) {
        swal({
            title: 'Are you sure want to delete?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.value) {
                this.roleAppService.deleteRole(id).then((response) => {
                    if (!response.isError) {
                        swal({
                            toast: true,
                            position: 'bottom-end',
                            showConfirmButton: false,
                            timer: 3000,
                            type: 'success',
                            title: 'Successfully deleted!'
                        });
                        this.getRoles();
                    } else {
                        swal({
                            title: response.errors.join('<br>'),
                            type: 'error',
                            showConfirmButton: false,
                        });
                    }
                });
            }
        });
    }
}
