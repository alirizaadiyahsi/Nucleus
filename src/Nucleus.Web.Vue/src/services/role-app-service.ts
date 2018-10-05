import BaseAppService from './base-app-service';
import queryString from 'query-string';

export default class RoleAppService extends BaseAppService {
    public getAll(roleListInput: IRoleListInput) {
        const query = '?' + queryString.stringify(roleListInput);

        return this.get<IPagedList<IRoleListDto>>('/api/Role/GetRoles' + query);
    }
}
