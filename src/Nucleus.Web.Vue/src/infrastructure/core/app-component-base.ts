import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import swal from 'sweetalert2';
import AppService from '@/infrastructure/core/app-service';
import QueryString from 'query-string';

@Component
export default class AppComponentBase extends Vue {
    protected appService: AppService = new AppService();
    protected queryString = QueryString;

    protected swalToast(duration: number, type: string, title: string) {
        swal({
            toast: true,
            position: 'bottom-end',
            showConfirmButton: false,
            timer: duration,
            type,
            title,
        } as any);
    }

    protected swalConfirm(title: string) {
        return swal({
            title,
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes',
            cancelButtonText: 'No',
        } as any);
    }

    protected swalAlert(type: string, html: string) {
        swal({
            html,
            type,
            showConfirmButton: false,
        } as any);
    }
}
