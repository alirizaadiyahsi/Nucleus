import BaseAppService from './base-app-service';
const queryString = require('query-string');
export default class UserAppService extends BaseAppService {
    getAll(userListInput) {
        let query = '?' + queryString.stringify(userListInput);
        return this.get('/api/User/Users' + query);
    }
}
//# sourceMappingURL=user-app-service.js.map