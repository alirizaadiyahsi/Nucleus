import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import RoleAppService from '../../../services/role-app-service';

@Component
export default class RoleListComponent extends Vue {
    public pagedListOfRoleListDto: IPagedList<IRoleListDto> = {
        totalCount: 0,
        items: [],
    };
    public roleAppService = new RoleAppService();

    public mounted() {
        const roleListInput: IRoleListInput = {
            filter: '',
        };

        this.roleAppService.getAll(roleListInput).then((response) => {
            this.pagedListOfRoleListDto = response.content as IPagedList<IRoleListDto>;
        });
    }
}
