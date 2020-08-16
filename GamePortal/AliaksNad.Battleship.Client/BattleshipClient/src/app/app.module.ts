import { AuthInterceptors } from './core/interceptors/auth.interceptors';
import { BattleshipRoutesModule } from './battleshiproutes.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { HTTP_INTERCEPTORS } from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    CoreModule,
    BattleshipRoutesModule,
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptors, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
