
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { SignalRModule, SignalRConfiguration, ConnectionTransports, ConnectionTransport} from 'ng2-signalr';
import { environment } from 'src/environments/environment';


export function initConfig(): SignalRConfiguration{
  const cfg = new SignalRConfiguration();

  cfg.hubName = 'GameHub';
  cfg.url = `${environment.backendurl}`;
  cfg.transport = [ConnectionTransports.webSockets, ConnectionTransports.longPolling];
  return cfg;
}



@NgModule({
  declarations: [AppComponent ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NoopAnimationsModule,
    SignalRModule.forRoot(initConfig),
  ],

  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
   ],
  bootstrap: [AppComponent]
})
export class AppModule { }
