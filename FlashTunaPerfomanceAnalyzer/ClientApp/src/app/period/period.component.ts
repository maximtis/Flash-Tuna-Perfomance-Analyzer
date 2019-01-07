import { Component, OnInit, Inject } from '@angular/core';
import { Chart } from 'chart.js';
import * as _ from 'underscore';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { MetricsResultService } from '../services/metrics-result.service';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import { TimerObservable } from "rxjs/observable/TimerObservable";
import 'rxjs/add/operator/takeWhile';
import { TrackableMethodModel } from '../models/trackable-method.model';

@Component({
  selector: 'period',
  templateUrl: './period.component.html'
})
export class PeriodComponent implements OnInit {

  private _hubConnection: HubConnection;
  metricsList: TrackableMethodModel[];
  fromDate: any;
  toDate: any;
  selectedMetricsTimes: number[];
  public selectedMetricsDates: string[];
  baseUrlHost: string;
  private alive: boolean; // used to unsubscribe from the TimerObservable
  // when OnDestroy is called.
  private interval: number;
  public selectedMethod: string;
  

  constructor(public metricsService: MetricsResultService, private fb: FormBuilder, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrlHost = baseUrl;
    this.selectedMetricsDates = [];//["2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24"];
    this.metricsList= [];
    this.fromDate = '2017-10-19';
    this.toDate = '2018-12-30';
    this.alive = true;
    this.interval = 1000;
  }

  ngOnInit(): void {
    //let hubUrl = this.baseUrlHost + '/notify';
    //this._hubConnection = new HubConnectionBuilder()
    //  .withUrl(hubUrl)
    //  .configureLogging(LogLevel.Information)
    //  .build();

    //this._hubConnection.on('MetricsUpdatetdBroadcast', async (methodName: string) => {
    //  await this.updateMetricsResult(methodName);
    //});
    this.selectedMetricsDates = []; //["2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24"];

  }

  public dataRequestForm = this.fb.group({
    PeriodFrom: ['2017-10-19', [Validators.required]],
    PeriodTo: ['2018-12-30', [Validators.required]],
    MethodName: ['ShortOperation', [Validators.required]]
  });

  public async updateMetricsList() {
    try {
      this.metricsList = await this.metricsService.getMetricsByPeriod(this.dataRequestForm.value);
      this.selectedMethod = this.metricsList[2];
          TimerObservable.create(1000, this.interval)
          .takeWhile(() => this.alive)
          .subscribe(async () => {
            await this.updateMetricsResultAuto();
          }); 
    }
    catch (e) {
      console.log('there was an error');
      console.log(e);
    }
  }
  public async updateMetricsResultAuto() {
    await this.updateMetricsResult(this.selectedMethod);
    console.log("updateExecuted.");
  }
  public async updateMetricsResult(methodName: string) {
    this.selectedMethod = methodName;
    let metricResults = await this.metricsService.getMetricResultsByPeriod(this.fromDate, this.toDate, methodName);
    console.log(metricResults);
    
    var mapResult = _.map(metricResults, function (metricModel) {
      return metricModel.startPoint.toString().substring(11, 19)
    });
    console.log(mapResult);
    this.selectedMetricsDates.length = 0;

    for (var i = 0; i < mapResult.length;i++) // for acts as a foreach  
    {
      this.selectedMetricsDates.push(mapResult[i]);
    }  

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
