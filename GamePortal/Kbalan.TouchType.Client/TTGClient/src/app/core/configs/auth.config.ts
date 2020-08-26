import { AuthConfig } from 'angular-oauth2-oidc';
import { InjectionToken } from '@angular/core';

export const oauthPassConfig: AuthConfig = {
  issuer: 'http://localhost:10000',
  redirectUri: window.location.origin + '/index.html',
  clientId: 'TTGUserClient',
  dummyClientSecret: 'secret',
  scope: 'openid profile email Role ',
  requireHttps: false,
  showDebugInformation: true,
  disablePKCE: true,
  oidc: false,
  logoutUrl: 'http://localhost:10000/connect/endsession',
  postLogoutRedirectUri: window.location.origin + '/entry/login',
};

export const oauthCodeConfig: AuthConfig = {
  issuer: 'http://localhost:10000',
  redirectUri: window.location.origin + '/index.html',
  clientId: 'TTGWebClient',
  dummyClientSecret: 'secret',
  responseType: 'code',
  scope: 'openid profile email Role',
  requireHttps: false,
  showDebugInformation: true,
  disablePKCE: true,
  logoutUrl: 'http://localhost:10000/connect/endsession',
  postLogoutRedirectUri: window.location.origin + '/entry/login',
};

export const PASSWORD_FLOW_CONFIG = new InjectionToken<AuthConfig>('password.flow.config');
export const CODE_FLOW_CONFIG = new InjectionToken<AuthConfig>('code.flow.config');
