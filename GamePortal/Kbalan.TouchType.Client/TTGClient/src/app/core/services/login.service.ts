import { filter, switchMap, map } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { UserDto } from '../models/UserDto';
import { Router } from '@angular/router';
import { OAuthService, AuthConfig, OAuthEvent, OAuthInfoEvent, OAuthSuccessEvent } from 'angular-oauth2-oidc';


export const oauthConfig: AuthConfig = {
  issuer: 'http://localhost:10000',
  redirectUri: window.location.origin + '/index.html',
  clientId: 'TTGUserClient',
  dummyClientSecret: 'secret',
  // responseType: 'code',
  scope: 'openid profile email api',
  requireHttps: false,
  showDebugInformation: true,
  disablePKCE: true,
  oidc: false,
  logoutUrl: 'http://localhost:10000/connect/endsession',
  postLogoutRedirectUri: window.location.origin + '/entry/login',
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
      this.oauth.initLoginFlow();
    }


    this.oauth
      .fetchTokenUsingPasswordFlowAndLoadUserProfile(userName, password)
      .then((userInfo) => {
        this.user = Object.assign({} as UserDto, userInfo);
        this.loggedOnSubject.next(this.user);
      })
      .catch((reason) => console.error(reason));
  }

  logout(){
    this.user = null;
    this.loggedOnSubject.next(null);
    this.oauth.logOut(false);
    this.router.navigate(['/entry/login']);
  }

}
