import AppComponentBase from '@/models/shared/app-component-base';
import { Component } from 'vue-property-decorator';

@Component
export default class CounterComponent extends AppComponentBase {
    public currentcount: number = 0;

    public incrementCounter() {
        this.currentcount++;
    }
}
