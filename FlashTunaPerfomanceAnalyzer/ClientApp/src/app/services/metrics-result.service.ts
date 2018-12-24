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
}
