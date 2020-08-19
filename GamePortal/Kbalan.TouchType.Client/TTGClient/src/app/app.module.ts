import { NavbarComponent } from './core/components/navbar/navbar.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { SgameComponent } from './sgame/component/sgame/sgame.component';



@NgModule({
  declarations: [AppComponent,  SgameComponent ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NoopAnimationsModule,

  ],

  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
   ],
  bootstrap: [AppComponent]
})
export class AppModule { }
