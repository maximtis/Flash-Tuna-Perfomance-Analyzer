import { Component } from '@angular/core';
import { Chart } from 'chart.js';
import * as _ from 'underscore';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { MetricsResultService } from '../services/metrics-result.service';

@Component({
  selector: 'home',
  templateUrl: './home.component.html'
})
export class HomeComponent {

  metricsList: string[];
  fromDate: any;
  toDate: any;
  selectedMetricsTimes: number[];
  selectedMetricsDates: string[];
  constructor(public metricsService: MetricsResultService, private fb: FormBuilder) {

    this.selectedMetricsDates = ["2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24"];
    this.metricsList = [];
    this.fromDate = '2017-10-19';
    this.toDate = '2018-12-30';
  }
  public dataRequestForm = this.fb.group({
    PeriodFrom: ['2017-10-19', [Validators.required]],
    PeriodTo: ['2018-12-30', [Validators.required]],
    MethodName: ['ShortOperation', [Validators.required]]
  });
  public async updateMetricsList() {
    try {
      this.metricsList = await this.metricsService.getMetricsByPeriod(this.dataRequestForm.value);
    }
    catch (e) {
      console.log('there was an error');
      console.log(e);
    }
  }
  public async updateMetricsResult(methodName: string) {
    let metricResults = await this.metricsService.getMetricResultsByPeriod(this.fromDate, this.toDate, methodName);
    console.log(metricResults);
    
    var mapResult = _.map(metricResults, function (metricModel) {
      return metricModel.startPoint.toString().substring(0, 10)
    });
    console.log(mapResult);
    this.selectedMetricsDates = mapResult;

    this.selectedMetricsTimes = _.map(metricResults, function (metricModel) {
      return metricModel.milliseconds;
    });

    debugger;
    this.lineChartData = [
      {
        data: this.selectedMetricsTimes,
        label: metricResults[0].methodName
      }
    ];
  }

  // lineChart
  public lineChartData: Array<any> = [
    { data: [], label: ' ' }
  ];

  public lineChartLabels: Array<any> = [''];
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
