import { Component } from 'vue-property-decorator';
import AppComponentBase from '@/infrastructure/core/app-component-base';

@Component
export default class SideMenuComponent extends AppComponentBase {

    public mainMenuItems = [
        { icon: 'home', text: 'Home', link: '/admin/home' },
        { icon: 'apps', text: 'Counter', link: '/admin/counter' }
    ];

    public adminMenuItems = [
        { icon: 'people', text: 'Users', link: '/admin/user-list' },
        { icon: 'work', text: 'Roles', link: '/admin/role-list' }
    ];
}