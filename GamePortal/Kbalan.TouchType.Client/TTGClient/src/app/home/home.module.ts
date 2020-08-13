import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomecomponentComponent } from './component/homecomponent/homecomponent.component';
import { CoreModule } from '../core/core.module';
import { ReactiveFormsModule, FormsModule} from '@angular/forms';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [HomecomponentComponent],
  imports: [
    CoreModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
  ]
})
export class HomeModule { }
