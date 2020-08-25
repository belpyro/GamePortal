import { environment } from './../../../environments/environment';
import { AuthConfig } from 'angular-oauth2-oidc';
import { InjectionToken } from '@angular/core';

export const oauthPassConfig: AuthConfig = {
  issuer: environment.issuerUrl,
  redirectUri: window.location.origin + '/index.html',
  clientId: 'BattleshipUserClient',
  dummyClientSecret: 'secret',
  scope: 'openid profile email',
  requireHttps: false,
  skipIssuerCheck: true,
  showDebugInformation: true,
  disablePKCE: true,
  oidc: false,
  postLogoutRedirectUri: window.location.origin + '/login',
};

export const oauthCodeConfig: AuthConfig = {
  issuer: environment.issuerUrl,
  redirectUri: window.location.origin + '/index.html',
  clientId: 'BattleshipWebClient',
  dummyClientSecret: 'secret',
  responseType: 'code',
  scope: 'openid profile email',
  requireHttps: false,
  skipIssuerCheck: true,
  showDebugInformation: true,
  disablePKCE: true,
  postLogoutRedirectUri: window.location.origin + '/login',
};

export const PASSWORD_FLOW_CONFIG = new InjectionToken<AuthConfig>('password.flow.config');
export const CODE_FLOW_CONFIG = new InjectionToken<AuthConfig>('code.flow.config');
