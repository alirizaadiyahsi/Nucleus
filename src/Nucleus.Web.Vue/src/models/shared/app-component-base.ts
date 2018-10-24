import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import swal from 'sweetalert2';

@Component
export default class AppComponentBase extends Vue {
    protected swalToast(duration: number, type: string, title: string) {
        swal({
            toast: true,
            position: 'bottom-end',
            showConfirmButton: false,
            timer: duration,
            type: type,
            title: title,
        } as any);
    }

    protected swalConfirm(title: string) {
        return swal({
            title: title,
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes',
            cancelButtonText: 'No',
        } as any);
    }

    protected swalAlert(type: string, html: string) {
        swal({
            html: html,
            type: type,
            showConfirmButton: false,
        } as any);
    }
}