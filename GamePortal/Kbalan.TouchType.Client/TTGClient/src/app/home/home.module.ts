import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomecomponentComponent } from './component/homecomponent/homecomponent.component';
import { CoreModule } from '../core/core.module';
import { ReactiveFormsModule, FormsModule} from '@angular/forms';


@NgModule({
  declarations: [HomecomponentComponent],
  imports: [
    CoreModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class HomeModule { }
