import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import TestAppService from '../../../services/test-app-service';

@Component
export default class FetchDataComponent extends Vue {
    public forecasts: IWeatherForecast[] = [];
    public testAppService = new TestAppService();

    public mounted() {
        this.testAppService.getAll().then((response) => {
            this.forecasts = response.content as IWeatherForecast[];
        });
    }
}
