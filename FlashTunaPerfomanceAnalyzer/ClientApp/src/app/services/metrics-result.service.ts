import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from "@angular/core";
import { MetricsResultModel } from "../models/metrics-result.model";
import { Observable, Subject } from 'rxjs';
import { map } from "rxjs/operators";

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

    async getMetricsByPeriod(form: any): Promise<string[]> {
      return this._http.post<string[]>(this._baseUrl + 'api/MetricResults/GetMetrics', form)
                         .toPromise<string[]>();
  }


  async getAllMetrics(): Promise<string[]> {
    return this._http.get<string[]>(this._baseUrl + 'api/MetricResults/GetMetrics')
      .toPromise<string[]>();
  }

    async getMetricsRuntime(): Promise<MetricsResultModel[][]> {
      return this._http.get<MetricsResultModel[][]>(this._baseUrl + 'api/MetricResults/GetMetricsResultsRuntime')
        .toPromise<MetricsResultModel[][]>();
  }
  async getErrorsMetricsRuntime(): Promise<MetricsResultModel[]> {
    return this._http.get<MetricsResultModel[]>(this._baseUrl + 'api/MetricResults/GetErrorsMetricsResultsRuntime')
      .toPromise<MetricsResultModel[]>();
  }

    async getRuntime(): Promise<any> {
        return this._http.get<any>(this._baseUrl + 'api/MetricResults/GetRuntime')
          .toPromise<any>();
    }
}
