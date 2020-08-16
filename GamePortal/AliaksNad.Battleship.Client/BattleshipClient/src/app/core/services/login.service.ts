import { UserDto } from './../models/UserDto';
import { OAuthModule, OAuthService, AuthConfig } from 'angular-oauth2-oidc';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { filter, map } from 'rxjs/operators';

export const oauthConfig: AuthConfig = {
  issuer: environment.issuerUrl,
  redirectUri: window.location.origin + '/index.html ',
  // clientId: 'BattleshipWebClient',
  clientId: 'BattleshipUserClient',
  dummyClientSecret: 'secret',
  // responseType: 'code',
  scope: 'openid profile email',
  requireHttps: false,
  skipIssuerCheck: true,
  // skipSubjectCheck: true,
  // strictDiscoveryDocumentValidation: false,
  showDebugInformation: true,
  disablePKCE: true,
  oidc: false,
  postLogoutRedirectUri: window.location.origin + '/login',
};

@Injectable()
export class LoginService {

  private loggedOnSubject: BehaviorSubject<UserDto> = new BehaviorSubject<UserDto>(null);
  private user: UserDto;

  constructor(private router: Router, private oauth: OAuthService) {
    this.oauth.configure(oauthConfig);
    this.oauth.loadDiscoveryDocumentAndTryLogin();
    this.oauth.events
      .pipe(
        filter((value) => value.type === 'token_received'),
        map((_) => Object.assign({} as UserDto, this.oauth.getIdentityClaims()))
      )
      .subscribe((u) => this.loggedOnSubject.next(u));
  }

  get LoggedOn$(): Observable<UserDto> {
    return this.loggedOnSubject.asObservable();
  }

  get LoggedOn() {
    return this.loggedOnSubject.value;
  }

  login(userName?: string, password?: string) {
    if (!userName || !password) {
      this.oauth.initCodeFlow();
    }

    this.oauth
      .fetchTokenUsingPasswordFlowAndLoadUserProfile(userName, password)
      .then((userInfo) => {
        this.user = Object.assign({} as UserDto, userInfo);
        this.loggedOnSubject.next(this.user);
      })
      .catch((reason) => console.error(reason));
  }

  logout() {
    this.user = null;
    this.loggedOnSubject.next(null);
    this.oauth.logOut(true);
    this.router.navigate(['home']);
  }
}
