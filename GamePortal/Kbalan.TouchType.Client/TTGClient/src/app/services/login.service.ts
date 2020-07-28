import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { UserDto } from '../models/UserDto';
import { Router } from '@angular/router';
import { OAuthService, AuthConfig } from 'angular-oauth2-oidc';

export const oauthConfig: AuthConfig = {
  issuer: 'http://localhost:10000',
  redirectUri: window.location.origin + '/index.html',
  clientId: 'TTGWebClient',
  dummyClientSecret: 'secret',
  responseType: 'code',
  scope: 'openid api',
  requireHttps: false,
  skipIssuerCheck: true,
  showDebugInformation: true,
  disablePKCE: true,
  postLogoutRedirectUri: window.location.origin + '/login',
};

@Injectable({providedIn: 'root'})
export class LoginService {

  private loggedOnSubject = new BehaviorSubject<boolean>(false);
  private user: UserDto;
  constructor(private router: Router, private oauth: OAuthService) {
    this.oauth.configure(oauthConfig);
    this.oauth.loadDiscoveryDocumentAndTryLogin();
   }

  get LoggedOn$(){
    return this.loggedOnSubject.asObservable();
  }

  get LoggedOn(){
    return this.loggedOnSubject.value;
  }

  login(userName?: string, password?: string) {
    if (!userName || !password) {
      this.oauth.initLoginFlow();
    }

    this.oauth
      .fetchTokenUsingPasswordFlowAndLoadUserProfile(userName, password)
      .then((userInfo) => {
        this.user = { uid: userInfo.sub, fullName: 'Ivan Ivanov' };
        this.loggedOnSubject.next(true);
      })
      .catch((reason) => console.error(reason));
  }

  logout(){
    this.user = null;
    this.loggedOnSubject.next(false);
    this.router.navigate(['user/login']);
  }

}
