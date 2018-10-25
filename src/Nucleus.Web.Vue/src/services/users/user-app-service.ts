import BaseAppService from '@/services/base-app-service';
import queryString from 'query-string';

export default class UserAppService extends BaseAppService {
    public getUsers(input: IUserListInput) {
        const query = '?' + queryString.stringify(input);

        return this.get<IPagedList<IUserListInput>>('/api/user/getUsers' + query);
    }

    public deleteUser(id: string) {
        const query = '?id=' + id;

        return this.delete('/api/user/deleteUser' + query)
            .then((response) => {
                return response;
            });
    }
}
