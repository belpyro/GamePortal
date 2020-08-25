import { PASSWORD_FLOW_CONFIG, CODE_FLOW_CONFIG } from '../configs/auth.config';
import { UserDto } from '../models/UserDto';
import { OAuthService, AuthConfig, OAuthSuccessEvent } from 'angular-oauth2-oidc';
import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject, Observable, from } from 'rxjs';
import { Router } from '@angular/router';
import { filter, map, switchMap, share } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class LoginService {

  private loggedOnSubject = new BehaviorSubject<UserDto>(null);
  private isLoggedOnSubject = new BehaviorSubject<boolean>(false);

  constructor(
    private router: Router,
    private oauth: OAuthService,
    @Inject(PASSWORD_FLOW_CONFIG) private passFlow: AuthConfig,
    @Inject(CODE_FLOW_CONFIG) private codeFlow: AuthConfig,
  ) {
    this.oauth.configure(codeFlow);
    this.oauth.loadDiscoveryDocumentAndTryLogin().then(() => this.tryLogin());
    this.oauth.events
      .pipe(
        filter((value) => value instanceof OAuthSuccessEvent),
        filter((value) => value.type === 'token_received'),
        switchMap((_) => from(this.oauth.loadUserProfile()))
      )
      .subscribe((u) => {
        this.loggedOnSubject.next(u);
        this.isLoggedOnSubject.next(true);
      });
  }

  get LoggedOn$(): Observable<UserDto> {
    return this.loggedOnSubject.asObservable().pipe(share());
  }

  get LoggedOn(): UserDto {
    return this.loggedOnSubject.value;
  }

  get isLoggedOn$() {
    return this.isLoggedOnSubject.asObservable().pipe(share());
  }

  get isLoggedOn() {
    return this.isLoggedOnSubject.value;
  }

  async loginWithCode() {
    await this.configureOauth(this.codeFlow);
    this.oauth.initCodeFlow();
  }

  async loginWithPass(userName: string, password: string) {
    await this.configureOauth(this.passFlow);
    await this.oauth.fetchTokenUsingPasswordFlow(userName, password);
  }

  async logout() {
    this.loggedOnSubject.next(null);
    this.isLoggedOnSubject.next(false);
    this.oauth.logOut();
    await this.router.navigate(['home']);
  }

  private async configureOauth(config: AuthConfig) {
    this.oauth.configure(config);
    await this.oauth.loadDiscoveryDocument();
  }

  private async tryLogin() {
    if (this.oauth.hasValidAccessToken()) {
      const user = await this.oauth.loadUserProfile();
      this.isLoggedOnSubject.next(true);
      this.loggedOnSubject.next(user);
    }
  }

}
