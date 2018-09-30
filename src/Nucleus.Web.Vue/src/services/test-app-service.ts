import BaseAppService from './base-app-service';

export default class TestAppService extends BaseAppService {
    getAll() {
        return this.get<Array<IWeatherForecast>>('/api/Test/WeatherForecasts');
    }
}