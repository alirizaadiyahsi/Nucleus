import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import AppService from '@/shared/application/app-service';
import QueryString from 'query-string';
import AppConsts from '@/shared/application/app-consts';
import swal from 'sweetalert2';
import AuthStore from '@/stores/auth-store';

@Component
export default class AppComponentBase extends Vue {
    protected appService: AppService = new AppService();
    protected queryString = QueryString;
    protected appConsts = AppConsts;
    protected authStore = AuthStore;

    protected swalToast(duration: number, type: string, title: string) {
        swal({
            toast: true,
            position: 'bottom-end',
            showConfirmButton: false,
            timer: duration,
            type,
            title
        } as any);
    }

    protected swalConfirm(title: string) {
        return swal({
            title,
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: this.$t('Yes').toString(),
            cancelButtonText: this.$t('No').toString()
        } as any);
    }

    protected swalAlert(type: string, html: string) {
        swal({
            html,
            type,
            showConfirmButton: false
        } as any);
    }

    protected passwordMatchError(password: string, passwordRepeat: string) {
        return (password == passwordRepeat)
            ? ''
            : this.$t('PasswordsMustMatch').toString();
    }
}