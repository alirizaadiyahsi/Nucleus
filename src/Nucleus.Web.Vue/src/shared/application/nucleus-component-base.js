import { __decorate } from "tslib";
import Vue from 'vue';
import NucleusService from '@/shared/application/nucleus-service-proxy';
import QueryString from 'query-string';
import Nucleus from '@/shared/application/nucleus';
import Swal from 'sweetalert2';
import AuthStore from '@/stores/auth-store';
import { Component } from 'vue-property-decorator';
let NucleusComponentBase = class NucleusComponentBase extends Vue {
    constructor() {
        super(...arguments);
        this.nucleusService = new NucleusService();
        this.queryString = QueryString;
        this.nucleus = Nucleus;
        this.authStore = AuthStore;
        this.requiredError = (v) => !!v || this.t('RequiredField');
        this.emailError = (v) => /.+@.+/.test(v) || this.t('EmailValidationError');
    }
    swalToast(duration, type, title) {
        Swal.fire({
            toast: true,
            position: 'bottom-end',
            showConfirmButton: false,
            timer: duration,
            type,
            title
        });
    }
    swalConfirm(title) {
        return Swal.fire({
            title,
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: this.$t('Yes').toString(),
            cancelButtonText: this.$t('No').toString()
        });
    }
    swalAlert(type, html) {
        Swal.fire({
            html,
            type,
            showConfirmButton: false
        });
    }
    passwordMatchError(password, passwordRepeat) {
        return (password == passwordRepeat)
            ? ''
            : this.$t('PasswordsMustMatch').toString();
    }
    t(key) {
        return this.$t(key).toString();
    }
};
NucleusComponentBase = __decorate([
    Component
], NucleusComponentBase);
export default NucleusComponentBase;
//# sourceMappingURL=nucleus-component-base.js.map