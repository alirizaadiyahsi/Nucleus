import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component
export default class CounterComponent extends Vue {
    public currentcount: number = 0;

    public incrementCounter() {
        this.currentcount++;
    }
}
