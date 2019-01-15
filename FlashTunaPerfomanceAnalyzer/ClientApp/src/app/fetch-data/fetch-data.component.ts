import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MetricsResultService } from '../services/metrics-result.service';
import { IntervalTypeModel } from '../models/interval-type.model';
import { TrackableMethodModel } from '../models/trackable-method.model';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public metricsList: TrackableMethodModel[];
  public intervalTypes: IntervalTypeModel[] = [
    { viewValue: 'Days', value: 1 },
    { viewValue: 'Hours', value: 2 },
    { viewValue: 'Minutes', value: 3 }
  ];
  public intervalNumber: number;
  public intervalType: number;
  constructor(public metricsService: MetricsResultService,http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.metricsService.getTrackableMethods().then(data => {
      this.metricsList = data;
    })
  }
  public async clearDatabase() {
    await this.metricsService.getClearDatabase();
  }
  public async updateInterval() {

  }
  public async setTracked(metric) {
    try {
      metric.selected = !metric.selected;
      let updatedMetric = await this.metricsService.postSetTrackableMethod(metric);
      metric.selected = updatedMetric.selected;
    } catch (error) {
      metric.selected = !metric.selected;
    }
  }
}
