import { Http } from "@angular/http";
import { Injectable, Inject } from "@angular/core";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';
import { MetricsResultModel } from "../models/metrics-result.model";

@Injectable()
export class MetricsResultService{
    _baseUrl: string;

    constructor(private _http: Http, @Inject('BASE_URL') baseUrl: string) {
        this._baseUrl = baseUrl;
    }

    async getMetricResultsByPeriod(from: Date, to: Date, methodName: string): Promise<MetricsResultModel[]> {
        let model: any = {
            PeriodFrom: from,
            PeriodTo: to,
            MethodName: methodName
        };
        debugger;
        return this._http.get(this._baseUrl + 'api/MetricResults/GetMetricsResults', model)
                         .map((response) => response.json())
                         .toPromise<MetricsResultModel[]>();
    }

    async getMetricsByPeriod(from: Date, to: Date): Promise<string[]> {
        let model: any = {
            PeriodFrom: from,
            PeriodTo: to
        };

        return this._http.get(this._baseUrl + 'api/MetricResults/GetMetrics', model)
                         .map((response) => response.json())
                         .toPromise<string[]>();
    }
}