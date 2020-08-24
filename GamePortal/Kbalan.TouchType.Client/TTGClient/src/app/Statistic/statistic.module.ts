import { StatisticComponent } from './component/statistic/statistic.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreModule } from '../core/core.module';
import { HttpClientModule } from '@angular/common/http';
import { GoogleChartsModule } from 'angular-google-charts';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [StatisticComponent],
  imports: [
    CoreModule,
    CommonModule,
    HttpClientModule,
    GoogleChartsModule,
    RouterModule
  ]
})
export class StatisticModule { }
