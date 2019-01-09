import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from "@angular/core";
import { MetricsResultModel } from "../models/metrics-result.model";
import { Observable, Subject } from 'rxjs';
import { map } from "rxjs/operators";
import { StatisticReportModel } from '../models/statistic-report.model';
import { TrackableMethodModel } from '../models/trackable-method.model';
import { IntervalSettingsModel } from '../models/interval-settings.model';
import { ErrorsResultModel } from '../models/errors-result.model';

@Injectable()
export class MetricsResultService{
    _baseUrl: string;

  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this._baseUrl = baseUrl;
    }

    async getMetricResultsByPeriod(from: Date, to: Date, methodName: string): Promise<MetricsResultModel[]> {
        let model: any = {
            PeriodFrom: from,
            PeriodTo: to,
            MethodName: methodName
        };
      return this._http.post<MetricsResultModel[]>(this._baseUrl + 'api/MetricResults/GetMetricsResults', model)
                         .toPromise<MetricsResultModel[]>();
    }

  async getMetricsByPeriod(form: any): Promise<TrackableMethodModel[]> {
      return this._http.post<TrackableMethodModel[]>(this._baseUrl + 'api/MetricResults/GetMetrics', form)
        .toPromise<TrackableMethodModel[]>();
  }


  async getAllMetrics(): Promise<TrackableMethodModel[]> {
    return this._http.get<TrackableMethodModel[]>(this._baseUrl + 'api/MetricResults/GetMetrics')
      .toPromise<TrackableMethodModel[]>();
  }

    async getMetricsRuntime(): Promise<MetricsResultModel[][]> {
      return this._http.get<MetricsResultModel[][]>(this._baseUrl + 'api/MetricResults/GetMetricsResultsRuntime')
        .toPromise<MetricsResultModel[][]>();
  }
  async getErrorsRuntime(): Promise<MetricsResultModel[][]> {
    return this._http.get<MetricsResultModel[][]>(this._baseUrl + 'api/MetricResults/GetMetricsResultsRuntime')
      .toPromise<MetricsResultModel[][]>();
  }
  async getStatisticReport(): Promise<StatisticReportModel> {
    return this._http.get<StatisticReportModel>(this._baseUrl + 'api/MetricResults/GetStatisticReport')
      .toPromise<StatisticReportModel>();
  }
  async getErrorsMetricsRuntime(): Promise<ErrorsResultModel[]> {
    return this._http.get<ErrorsResultModel[]>(this._baseUrl + 'api/MetricResults/GetErrorsMetricsResultsRuntime')
      .toPromise<ErrorsResultModel[]>();
  }

  async getTrackableMethods(): Promise<TrackableMethodModel[]> {
    return this._http.get<TrackableMethodModel[]>(this._baseUrl + 'api/MetricResults/GetTrackableMethods')
      .toPromise<TrackableMethodModel[]>();
  }
  async postSetTrackableMethod(metric: TrackableMethodModel): Promise<TrackableMethodModel> {
    return this._http.post<TrackableMethodModel>(this._baseUrl + 'api/MetricResults/SetTrackableMethod', metric)
      .toPromise<TrackableMethodModel>();
  }
  async postUpdateInterval(intervalData: IntervalSettingsModel): Promise<any> {
    return this._http.post<any>(this._baseUrl + 'api/MetricResults/UpdateInterval', intervalData)
      .toPromise<any>();
  }


  async getRuntime(): Promise<any> {
    return this._http.get<any>(this._baseUrl + 'api/MetricResults/GetRuntime')
      .toPromise<any>();
  }
}
