import BaseAppService from './base-app-service';
import queryString from 'query-string';
export default class UserAppService extends BaseAppService {
    getAll(userListInput) {
        const query = '?' + queryString.stringify(userListInput);
        return this.get('/api/User/Users' + query);
    }
}
//# sourceMappingURL=user-app-service.js.map