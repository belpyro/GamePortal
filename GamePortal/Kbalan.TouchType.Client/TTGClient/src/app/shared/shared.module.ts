import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UploadComponent } from './upload/upload.component';

import { RouterModule } from '@angular/router';
import { AppModule } from '../app.module';

@NgModule({
  declarations: [UploadComponent],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [UploadComponent]
})
export class SharedModule { }
