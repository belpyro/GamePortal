import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { CoreModule } from '../core/core.module';
import { GoogleChartsModule } from 'angular-google-charts';
import { SgameComponent } from './component/sgame/sgame.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [SgameComponent],
  imports: [
    CoreModule,
    CommonModule,
    HttpClientModule,
    GoogleChartsModule,
    RouterModule
  ]
})
export class SgameModule { }
