import BaseAppService from './base-app-service';
export default class TestAppService extends BaseAppService {
    getAll() {
        return this.get('/api/Test/WeatherForecasts');
    }
}
//# sourceMappingURL=test-app-service.js.map