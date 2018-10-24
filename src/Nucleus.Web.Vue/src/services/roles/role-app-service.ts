import BaseAppService from '../base-app-service';
import queryString from 'query-string';

export default class RoleAppService extends BaseAppService {
    public getRoles(input: IRoleListInput) {
        const query = '?' + queryString.stringify(input);

        return this.get<IPagedList<IRoleListOutput>>('/api/role/getRoles' + query);
    }

    public getRoleForCreateOrUpdate(id: string) {
        return this.get<IGetRoleForCreateOrUpdateOutput>('/api/role/GetRoleForCreateOrUpdate?id=' + id);
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
