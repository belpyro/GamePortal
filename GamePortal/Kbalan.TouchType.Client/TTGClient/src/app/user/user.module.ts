import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsermanagerComponent } from './usermanager/usermanager.component';
import { CoreModule } from '../core/core.module';
import { FormsModule } from '@angular/forms';
import {MatToolbarModule} from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
@NgModule({
  declarations: [UsermanagerComponent],
  imports: [
    CoreModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    MatToolbarModule,
    MatIconModule

  ]
})
export class UserModule { }
