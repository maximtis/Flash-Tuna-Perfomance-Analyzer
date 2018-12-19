import { Component } from '@angular/core';
import { Chart } from 'chart.js';
import { MetricsResultService } from '../../services/metrics-result.service';
import * as _ from 'underscore';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    startDate = new Date(1990, 0, 1);



    metricsList: string[];
    fromDate: any;
    toDate: any;
    selectedMetricsDates: Date[];
    selectedMetricsTimes: number[];
    constructor(public metricsService: MetricsResultService, private fb: FormBuilder) {
        this.selectedMetricsDates = [];
        this.selectedMetricsTimes = [];
        this.metricsList = [];
        this.fromDate = Date();
        this.toDate = Date();
    }
    dataRequestForm = this.fb.group({
        PeriodFrom: ['2018-10-19', [Validators.required]],
        PeriodTo: ['2018-12-19', [Validators.required]],
        MethodName: ['ShortOperation', [Validators.required]]
    });
    public async updateMetricsList() {
        debugger;
        try {
            this.metricsList = await this.metricsService.getMetricsByPeriod(this.dataRequestForm.value);
        }
        catch (e) {
            console.log('there was an error');
            console.log(e);
        }
    }
    public async updateMetricsResult(methodName: string) {
        debugger;
        let metricResults = await this.metricsService.getMetricResultsByPeriod(this.fromDate, this.toDate, methodName);
        this.selectedMetricsDates = _.map(metricResults, function (metricModel) {
            return metricModel.startPoint;
        });
        debugger;
        this.selectedMetricsTimes = _.map(metricResults, function (metricModel) {
           
            return metricModel.milliseconds;
        });

        this.lineChartData = [
            {
                data: this.selectedMetricsTimes,
                label: metricResults[0].methodName
            }
        ];
    }

    // lineChart
    public lineChartData: Array<any> = [
        { data: [18, 48, 77, 9, 100, 27, 40], label: 'Series C' }
    ];

    public lineChartLabels: Array<any> = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
    public lineChartOptions: any = {
        responsive: true
    };
    public lineChartColors: Array<any> = [
        { // grey
            backgroundColor: 'rgba(148,159,177,0.2)',
            borderColor: 'rgba(148,159,177,1)',
            pointBackgroundColor: 'rgba(148,159,177,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(148,159,177,0.8)'
        },
        { // dark grey
            backgroundColor: 'rgba(77,83,96,0.2)',
            borderColor: 'rgba(77,83,96,1)',
            pointBackgroundColor: 'rgba(77,83,96,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(77,83,96,1)'
        },
        { // grey
            backgroundColor: 'rgba(148,159,177,0.2)',
            borderColor: 'rgba(148,159,177,1)',
            pointBackgroundColor: 'rgba(148,159,177,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(148,159,177,0.8)'
        }
    ];
    public lineChartLegend: boolean = true;
    public lineChartType: string = 'line';

    public randomize(): void {
        let _lineChartData: Array<any> = new Array(this.lineChartData.length);
        for (let i = 0; i < this.lineChartData.length; i++) {
            _lineChartData[i] = { data: new Array(this.lineChartData[i].data.length), label: this.lineChartData[i].label };
            for (let j = 0; j < this.lineChartData[i].data.length; j++) {
                _lineChartData[i].data[j] = Math.floor((Math.random() * 100) + 1);
            }
        }
        this.lineChartData = _lineChartData;
    }

    // events
    public chartClicked(e: any): void {
        console.log(e);
    }

    public chartHovered(e: any): void {
        console.log(e);
    }
}
