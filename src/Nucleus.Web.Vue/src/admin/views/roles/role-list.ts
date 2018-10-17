import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import RoleAppService from '../../../services/role-app-service';

@Component({
    components: {
        CreateRoleModalComponent: require('./create-role-modal.vue').default,
    },
})
export default class RoleListComponent extends Vue {
    public pagedListOfRoleListDto: IPagedList<IRoleListOutput> = {
        totalCount: 0,
        items: [],
    };
    public roleAppService = new RoleAppService();

    public mounted() {
        this.getRoles();
    }

    public getRoles() {
        const roleListInput: IRoleListInput = {
            filter: '',
        };

        this.roleAppService.getRoles(roleListInput).then((response) => {
            this.pagedListOfRoleListDto = response.content as IPagedList<IRoleListOutput>;
        });
    }

    public removeRole(id: string) {
        if (confirm('Are you sure want to delete?')) {
            this.roleAppService.deleteRole(id).then((response) => {
                if (!response.isError) {
                    this.getRoles();
                } else {
                    alert(response.errors);
                }
            });
        }
    }
}
