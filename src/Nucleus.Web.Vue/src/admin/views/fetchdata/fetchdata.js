import * as tslib_1 from "tslib";
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import TestAppService from '../../../services/test-app-service';
let FetchDataComponent = class FetchDataComponent extends Vue {
    constructor() {
        super(...arguments);
        this.forecasts = [];
        this.testAppService = new TestAppService();
    }
    mounted() {
        this.testAppService.getAll().then((response) => {
            this.forecasts = response.content;
        });
    }
};
FetchDataComponent = tslib_1.__decorate([
    Component
], FetchDataComponent);
export default FetchDataComponent;
//# sourceMappingURL=fetchdata.js.map