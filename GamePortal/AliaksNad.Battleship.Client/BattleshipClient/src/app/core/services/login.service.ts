import { PASSWORD_FLOW_CONFIG, CODE_FLOW_CONFIG } from '../configs/auth.config';
import { UserDto } from './../models/UserDto';
import { OAuthService, AuthConfig } from 'angular-oauth2-oidc';
import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { filter, map } from 'rxjs/operators';

@Injectable()
export class LoginService {

  private loggedOnSubject: BehaviorSubject<UserDto> = new BehaviorSubject<UserDto>(null);
  private user: UserDto;

  constructor(
    private router: Router,
    private oauth: OAuthService,
    @Inject(PASSWORD_FLOW_CONFIG) private passFlow: AuthConfig,
    @Inject(CODE_FLOW_CONFIG) private codeFlow: AuthConfig,
  ) {

    this.oauth.tryLogin();
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

  get LoggedOn(): UserDto {
    return this.loggedOnSubject.value;
  }

  async loginWithCode() {
    await this.configureOauth(this.codeFlow);
    this.oauth.initCodeFlow();
  }

  async loginWithPass(userName: string, password: string) {
    await this.configureOauth(this.passFlow);
    const userInfo = await this.oauth
      .fetchTokenUsingPasswordFlowAndLoadUserProfile(userName, password);
    this.user = Object.assign({} as UserDto, userInfo);
    this.loggedOnSubject.next(this.user);
  }

  logout(): void {
    this.user = null;
    this.loggedOnSubject.next(null);
    this.oauth.logOut(true);
    this.router.navigate(['home']);
  }

  private async configureOauth(config: AuthConfig) {
    this.oauth.configure(config);
    await this.oauth.loadDiscoveryDocument();
  }
}
