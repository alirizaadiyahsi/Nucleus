import BaseAppService from './base-app-service';
import queryString from 'query-string';

export default class RoleAppService extends BaseAppService {
    public getAll(input: IRoleListInput) {
        const query = '?' + queryString.stringify(input);

        return this.get<IPagedList<IRoleListOutput>>('/api/role/getRoles' + query);
    }

    public addRole(input: ICreateOrEditRoleInput) {
        return this.post<void>('/api/role/addRole', input)
            .then((response) => {
                return response;
            });
    }
}
