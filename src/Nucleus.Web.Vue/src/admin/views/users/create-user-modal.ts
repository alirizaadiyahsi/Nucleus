import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import UserAppService from '../../../services/user-app-service';

@Component
export default class CreateUserModalComponent extends Vue {

    public userName = '';
    public email = '';
    public errors: IErrorResponse[] = [];

    public onSubmit() {
        const userAppService = new UserAppService();
        const createOrEditUserInput: ICreateOrEditUserInput = { userName: this.userName, email: this.email, permissions: [] };

        userAppService.addUser(createOrEditUserInput).then((response) => {
            if (!response.isError) {
                this.$refs.modalCreateUser.hide();
                this.$parent.getUsers();
            } else {
                this.errors = response.errors;
            }
        });
    }
}