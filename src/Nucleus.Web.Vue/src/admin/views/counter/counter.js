import * as tslib_1 from "tslib";
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
let CounterComponent = class CounterComponent extends Vue {
    constructor() {
        super(...arguments);
        this.currentcount = 0;
    }
    incrementCounter() {
        this.currentcount++;
    }
};
CounterComponent = tslib_1.__decorate([
    Component
], CounterComponent);
export default CounterComponent;
//# sourceMappingURL=counter.js.map