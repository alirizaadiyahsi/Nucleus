import BaseAppService from './base-app-service';
import queryString from 'query-string';

export default class UserAppService extends BaseAppService {
    public getAll(input: IUserListInput) {
        const query = '?' + queryString.stringify(input);

        return this.get<IPagedList<IUserListInput>>('/api/user/getUsers' + query);
    }

    public addUser(input: ICreateOrEditUserInput) {
        return this.post<void>('/api/user/addUser', input)
            .then((response) => {
                return response;
            });
    }
}
