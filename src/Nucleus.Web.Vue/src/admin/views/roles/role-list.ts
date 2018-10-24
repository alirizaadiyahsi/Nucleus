import AppComponentBase from '@/models/shared/app-component-base';
import { Component } from 'vue-property-decorator';
import RoleAppService from '@/services/roles/role-app-service';

@Component({
    components: {
        CreateOrUpdateRoleModalComponent: require('./create-or-update-role-modal.vue').default,
    },
})
export default class RoleListComponent extends AppComponentBase {
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
        this.swalConfirm('Are you sure want to delete?')
            .then((result) => {
                if (result.value) {
                    this.roleAppService.deleteRole(id).then((response) => {
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
