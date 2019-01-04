import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MetricsResultService } from '../services/metrics-result.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public metricsList: string[];

  constructor(public metricsService: MetricsResultService,http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.metricsService.getAllMetrics().then(data => {
      this.metricsList = data;
    })
  }
}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
