import NucleusService from '@/shared/application/nucleus-service-proxy';
import QueryString from 'query-string';

const nucleus = {
    baseApiUrl: 'https://localhost:44339',
    baseClientUrl: 'http://localhost:8080',
    auth: {
        grantedPermissions: [] as string[],
        isGranted(permissionName: string) {
            return this.grantedPermissions.includes(permissionName);
        },
        removeProps() {
            this.grantedPermissions = [];
        },
        fillProps() {
            const roleListInput: IPagedListInput = {
                filter: ''
            };

            const query = '?' + QueryString.stringify(roleListInput);
            let nucleusService = new NucleusService();
            nucleusService.get<IPagedList<IRoleListOutput>>('/api/configurations/getAll' + query).then((response) => {
                let result = response.content as IPagedList<IRoleListOutput>;
                this.grantedPermissions = result.items as any;
            });
        }
    }
};

export default nucleus;