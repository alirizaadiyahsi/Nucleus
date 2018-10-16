import BaseAppService from './base-app-service';

export default class PermissionAppService extends BaseAppService {
    public getAllPermissions() {
        return this.get<IPermissionDto[]>('/api/role/getAllPermissions');
    }
}
