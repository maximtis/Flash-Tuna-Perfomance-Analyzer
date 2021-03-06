import { Component, OnInit, Inject, AfterViewChecked, OnDestroy } from '@angular/core';
import { Chart } from 'chart.js';
import * as _ from 'underscore';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { MetricsResultService } from '../services/metrics-result.service';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@aspnet/signalr';

@Component({
  selector: 'runtime',
  templateUrl: './runtime.component.html'
})
export class RuntimeComponent implements AfterViewChecked, OnDestroy,OnInit {
    ngOnDestroy(): void {
      this._hubConnection.stop();
    }

  private _hubConnection: HubConnection;
  metricsList: string[];
  fromDate: any;
  toDate: any;
  public selectedMetricsDates: string[];
  baseUrlHost: string;
  private alive: boolean; // used to unsubscribe from the TimerObservable
  // when OnDestroy is called.
  private interval: any;
  private isRunning: boolean;
  private timerObservable: any;

  constructor(public metricsService: MetricsResultService, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrlHost = baseUrl;
    this.selectedMetricsDates = [];//["2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24"];
    this.metricsList = [];
    let hubUrl = this.baseUrlHost + '/notify';

    this._hubConnection = new HubConnectionBuilder()
      .withUrl(hubUrl)
      .configureLogging(LogLevel.Information)
      .build();

    this._hubConnection.on('MetricsUpdatedBroadcast', async () => {
      console.log('Ok Received');
      if(!this.isRunning)
        await this.updateMetricsResultAuto();
    });
  }

  ngOnInit(): void {

      this.startConnection();

    }

  private startConnection(): void {
    this._hubConnection
      .start()
      .then(() => {
        console.log('Hub connection started');
      })
      .catch(err => {
        console.log('Error while establishing connection, retrying...');
      });

  }  
  public async loadData() {
    await this.metricsService.getRuntime();
  }

  ngAfterViewChecked(): void
  {
    
    //this.selectedMetricsDates = ["2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24", "2018-12-24"];
    //this.interval = setInterval(this.updateMetricsResultAuto, 10000);

  }

  public async updateMetricsResultAuto() {
    this.isRunning = true;
    await this.updateMetricsResult();
    await this.updateErrorsResult();
    console.log("updateExecuted.");
    this.isRunning = false;
  }

  public async updateMetricsResult() {
    let metricResults = await this.metricsService.getMetricsRuntime();
    var chartsData = [];
    for (var mi = 0; mi < metricResults.length; mi++) // for acts as a foreach  
    {
      var mapResult = _.map(metricResults[mi], function (metricModel) {
        return metricModel.startPoint.toString().substring(11, 19)
      });
      console.log(mapResult);

      if (this.selectedMetricsDates.length < mapResult.length) {
        this.selectedMetricsDates.length = 0;
        for (var i = 0; i < mapResult.length; i++) // for acts as a foreach  
        {
          this.selectedMetricsDates.push(mapResult[i]);
        }
      }

      var selectedMetricsTimes = _.map(metricResults[mi], function (metricModel) {
        return metricModel.milliseconds;
      });
      chartsData.push(
        {
          data: selectedMetricsTimes,
          label: metricResults[mi][0].methodName
        });
     
    }
    this.lineChartData = chartsData;

  }

  public async updateErrorsResult() {
    let errorsResults = await this.metricsService.getErrorsMetricsRuntime();
    var pieData = [];
    var pieLabels = [];
    for (var mi = 0; mi < errorsResults.length; mi++) // for acts as a foreach  
    {
      pieData.push(errorsResults[mi].errorsCount);
      pieLabels.push(errorsResults[mi].methodName);

    }
    this.pieChartData = pieData;
    this.pieChartLabels = pieLabels;

  }
  // lineChart
  public lineChartData: Array<any> = [
    { data: [], label: ' ' },
    { data: [], label: ' ' },
    { data: [], label: ' ' },
    { data: [], label: ' ' },
    { data: [], label: ' ' },
  ];

  public pieChartLabels: string[] = [' ', ' ', ' ', ' ', ' '];
  public pieChartData: number[] = [];
  public pieChartType: string = 'pie';
  // events

  public pieChartClicked(e: any): void {
    console.log(e);
  }
  public pieChartHovered(e: any): void {
    console.log(e);
  }

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

  // events
  public chartClicked(e: any): void {
    console.log(e);
  }

  public chartHovered(e: any): void {
    console.log(e);
  }
}
