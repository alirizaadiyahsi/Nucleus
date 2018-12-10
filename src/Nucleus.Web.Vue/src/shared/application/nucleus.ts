﻿import NucleusService from '@/shared/application/nucleus-service-proxy';
import AuthStore from '@/stores/auth-store';

const nucleus = {
    baseApiUrl: 'https://localhost:44339',
    baseClientUrl: 'http://localhost:8080',
    isLoading: false,
    appVersion: '0.4.0',
    auth: {
        grantedPermissions: [] as IPermissionDto[],
        isGranted(permissionName: string) {
            return this.grantedPermissions.filter((p) => p.name == permissionName).length > 0;
        },
        removeProps() {
            this.grantedPermissions = [];
        },
        fillProps() {
            const nucleusService = new NucleusService();
            nucleusService.get<IPermissionDto[]>(
                '/api/account/GetGrantedPermissionsAsync?userNameOrEmail=' + AuthStore.getTokenData().sub
            )
                .then((response) => {
                    this.grantedPermissions = response.content as IPermissionDto[];
                });
        }
    }
};

export default nucleus;