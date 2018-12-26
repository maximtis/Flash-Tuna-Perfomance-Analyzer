import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { MetricsResultService } from './services/metrics-result.service';
import { ChartsModule } from 'ng2-charts';
import { FilterPipe } from './pipes/filter.pipe';
import { RuntimeComponent } from './runtime/runtime.component';
import { PeriodComponent } from './period/period.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    RuntimeComponent,
    PeriodComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    FilterPipe
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ChartsModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'runtime', component: RuntimeComponent },
      { path: 'period', component: PeriodComponent },
    ])
  ],
  providers: [MetricsResultService],
  bootstrap: [AppComponent]
})
export class AppModule { }
