import { NotificationService } from './services/notification.service';
import { ReactiveFormsModule } from '@angular/forms';
import { NotFoundComponent } from './components/notfound/notfound.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { NgModule, ModuleWithProviders, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { OAuthModule } from 'angular-oauth2-oidc';
import { HomeComponent } from './components/home/home.component';
import { BattleshipModule } from '../battleship/battleship.module';
import { PASSWORD_FLOW_CONFIG, CODE_FLOW_CONFIG, oauthPassConfig, oauthCodeConfig } from './configs/auth.config';
import { LoginService } from './services/login.service';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [LoginComponent, NotFoundComponent, HomeComponent, NavbarComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    BattleshipModule,
    OAuthModule.forRoot(),
  ],
  providers: [
    LoginService,
    NotificationService,
    { provide: PASSWORD_FLOW_CONFIG, useValue: oauthPassConfig },
    { provide: CODE_FLOW_CONFIG, useValue: oauthCodeConfig },
  ],
  exports: [LoginComponent, NavbarComponent, NotFoundComponent, HomeComponent, OAuthModule]
})
export class CoreModule {

  constructor(@Optional() @SkipSelf() coreModule: CommonModule) {
    if (coreModule) {
      throw new Error('CoreModule already loaded.');
    }
  }

  static forRoot(): ModuleWithProviders<CoreModule> {
    return {
      ngModule: CoreModule,
      providers: [NotificationService]
    };
  }
}
