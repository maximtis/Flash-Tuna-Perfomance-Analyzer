import { Component, OnInit, Inject } from '@angular/core';
import { Chart } from 'chart.js';
import * as _ from 'underscore';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { MetricsResultService } from '../services/metrics-result.service';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import { StatisticReportModel } from '../models/statistic-report.model';

@Component({
  selector: 'home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {

  private _hubConnection: HubConnection;
  metricsList: string[];
  fromDate: any;
  toDate: any;
  selectedMetricsTimes: number[];
  public selectedMetricsDates: string[];
  baseUrlHost: string;
  private alive: boolean; // used to unsubscribe from the TimerObservable
  // when OnDestroy is called.
  private interval: number;
  public loading: boolean;
  public selectedMethod: string;
  public statistic: StatisticReportModel = new StatisticReportModel(0,0,0);
  

  constructor(public metricsService: MetricsResultService, private fb: FormBuilder, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrlHost = baseUrl;
    this.updateMetricsList().then(null);
    this.statistic = JSON.parse(localStorage.getItem('stat'));
  }

  ngOnInit(): void {
  }


  public async updateMetricsList() {
    try {
      this.loading = true;
      this.statistic = await this.metricsService.getStatisticReport();
      this.loading = false;
      localStorage.setItem('stat', JSON.stringify(this.statistic));
    }
    catch (e) {
      console.log('there was an error');
      console.log(e);
    }
  }
}
