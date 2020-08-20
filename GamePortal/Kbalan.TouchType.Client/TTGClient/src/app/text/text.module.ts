
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { CoreModule } from '../core/core.module';
import { ReactiveFormsModule, FormsModule} from '@angular/forms';
import { TextblockComponent } from './component/textblock/textblock.component';
import { GoogleChartsModule } from 'angular-google-charts';

@NgModule({
  declarations: [TextblockComponent],
  imports: [
    CoreModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    GoogleChartsModule,
    ToastrModule.forRoot({
      progressBar : true
    }),
  ]
})
export class TextModule { }
