import BaseAppService from './base-app-service';
import queryString from 'query-string';

export default class RoleAppService extends BaseAppService {
    public getRoles(input: IRoleListInput) {
        const query = '?' + queryString.stringify(input);

        return this.get<IPagedList<IRoleListOutput>>('/api/role/getRoles' + query);
    }

    public getRoleForCreateOrUpdate(input: IGetRoleForCreateOrUpdateInput) {
        const query = '?' + queryString.stringify(input);

        return this.get<IGetRoleForCreateOrUpdateOutput>('/api/role/GetRoleForCreateOrUpdate' + query);
    }

    public createOrUpdateRole(input: ICreateOrUpdateRoleInput) {
        return this.post<void>('/api/role/createOrUpdateRole', input)
            .then((response) => {
                return response;
            });
    }

    public deleteRole(id: string) {
        const query = '?id=' + id;

        return this.delete('/api/role/deleteRole' + query)
            .then((response) => {
                return response;
            });
    }
}
