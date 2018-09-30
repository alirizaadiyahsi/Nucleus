import BaseAppService from './base-app-service';
const queryString = require('query-string');

export default class UserAppService extends BaseAppService {
    getAll(userListInput?: IUserListInput) {
        let query = '?' + queryString.stringify(userListInput);

        return this.get<IPagedList<IUserListDto>>('/api/User/Users' + query);
    }
}