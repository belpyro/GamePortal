import { OAuthModule, OAuthService, AuthConfig } from 'angular-oauth2-oidc';
import { UserDto } from './../models/UserDto';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

export const oauthConfig: AuthConfig = {
  issuer: 'https://localhost:44313',
  redirectUri: window.location.origin + '/index.html ',
  clientId: 'BattleshipUserClient',
  dummyClientSecret: 'secret',
  // responseType: 'code',
  scope: 'openid profile email',
  requireHttps: false,
  skipIssuerCheck: true,
  skipSubjectCheck: true,
  strictDiscoveryDocumentValidation: false,
  showDebugInformation: true,
  oidc: false,
};

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private loggedOnSubject = new BehaviorSubject<boolean>(true);
  private user: UserDto;

  constructor(private router: Router, private oauth: OAuthService) {
    this.oauth.configure(oauthConfig);
    this.oauth.loadDiscoveryDocumentAndTryLogin();
  }

  get LoggedOn$() {
    return this.loggedOnSubject.asObservable();
  }

  get LoggedOn() {
    return this.loggedOnSubject.value;
  }

  login(userName?: string, password?: string) {
    this.oauth.fetchTokenUsingPasswordFlowAndLoadUserProfile(userName, password).then(
      (userInfo) => {
        this.user = { uid: userInfo.sub, fullName: 'Trest' };
        this.loggedOnSubject.next(true);
      }
    );
    // this.oauth.initCodeFlow();
  }

  logout() {
    this.user = null;
    this.loggedOnSubject.next(false);
    this.router.navigate(['home']);
  }

}
