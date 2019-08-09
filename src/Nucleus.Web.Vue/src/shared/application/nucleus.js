import NucleusService from '@/shared/application/nucleus-service-proxy';
import AuthStore from '@/stores/auth-store';
const nucleus = {
    baseApiUrl: 'https://localhost:44313',
    baseClientUrl: 'http://localhost:8080',
    isLoading: false,
    appVersion: '1.0.0',
    auth: {
        grantedPermissions: [],
        isGranted(permissionName) {
            return this.grantedPermissions.filter((p) => p.name == permissionName).length > 0;
        },
        removeProps() {
            this.grantedPermissions = [];
        },
        fillProps() {
            const nucleusService = new NucleusService();
            nucleusService.get('/api/permissions?userNameOrEmail=' + AuthStore.getTokenData().sub).then((response) => {
                if (!response.isError) {
                    this.grantedPermissions = response.content;
                }
            });
        }
    }
};
export default nucleus;
//# sourceMappingURL=nucleus.js.map