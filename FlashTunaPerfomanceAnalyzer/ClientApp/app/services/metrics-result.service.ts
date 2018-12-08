import { Http } from "@angular/http";
import { Injectable } from "@angular/core";

@Injectable()
export class MetricsResultService{
    baseUrl: string;

    constructor(private _http: Http) {
        this.baseUrl = "http://192.168.1.106:8080/";
    }

    getMetricResultsByPeriod(from: Date, to: Date) {
        let model: any = {
            PeriodFrom: from,
            PeriodTo: to
        };

        this._http.post(this.baseUrl + 'api/MetricResults/GetMetricsResults', model).subscribe(result => {
            return result.json();
        }, error => console.error(error));
    }

}