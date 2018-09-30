import BaseAppService from './base-app-service';
import queryString from 'query-string';

export default class UserAppService extends BaseAppService {
    public getAll(userListInput?: IUserListInput) {
        const query = '?' + queryString.stringify(userListInput);

        return this.get<IPagedList<IUserListDto>>('/api/User/Users' + query);
    }
}
