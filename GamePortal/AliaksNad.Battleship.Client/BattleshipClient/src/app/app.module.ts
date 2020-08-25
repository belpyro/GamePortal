import { AuthInterceptors } from './core/interceptors/auth.interceptors';
import { BattleshipRoutesModule } from './battleshiproutes.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { SignalRModule, SignalRConfiguration, ConnectionTransport, ConnectionTransports } from 'ng2-signalr';

export function initConfig(): SignalRConfiguration {
  const cfg = new SignalRConfiguration();

  cfg.hubName = 'gameHub';
  cfg.url = 'https://aliaksnad-battleship.azurewebsites.net';
  cfg.transport = [
    ConnectionTransports.webSockets,
    ConnectionTransports.longPolling,
  ];

  return cfg;
}
@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    CoreModule,
    NoopAnimationsModule,
    SimpleNotificationsModule.forRoot(),
    CoreModule.forRoot(),
    SignalRModule.forRoot(initConfig),
    BattleshipRoutesModule,
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
