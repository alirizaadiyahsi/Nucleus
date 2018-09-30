import BaseAppService from './base-app-service';

export default class TestAppService extends BaseAppService {
    public getAll() {
        return this.get<IWeatherForecast[]>('/api/Test/WeatherForecasts');
    }
}
