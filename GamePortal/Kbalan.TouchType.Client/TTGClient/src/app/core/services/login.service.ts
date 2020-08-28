import { filter, switchMap, map, share } from 'rxjs/operators';
import { Injectable, InjectionToken, Inject } from '@angular/core';
import { BehaviorSubject, Observable, from } from 'rxjs';
import { UserDto } from '../models/UserDto';
import { Router } from '@angular/router';
import { OAuthService, AuthConfig, OAuthEvent, OAuthInfoEvent, OAuthSuccessEvent, NullValidationHandler, } from 'angular-oauth2-oidc';
import { PASSWORD_FLOW_CONFIG, CODE_FLOW_CONFIG } from '../configs/auth.config';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';




@Injectable({ providedIn: 'root' })
export class LoginService {
  private loggedOnSubject = new BehaviorSubject<UserDto>(null);
  private isLoggedOnSubject = new BehaviorSubject<boolean>(false);

  constructor(
    private toastr: ToastrService,
    private router: Router,
    private oauth: OAuthService,
    private http: HttpClient,
    @Inject(PASSWORD_FLOW_CONFIG) private passFlow: AuthConfig,
    @Inject(CODE_FLOW_CONFIG) private codeFlow: AuthConfig
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
        this.toastr.success(`Welcome, ${u.preferred_username}`);
        this.lastLoginDateUpdate(u.sub).subscribe();
        this.router.navigate(['home']);
      });
  }

  get LoggedOn$(): Observable<UserDto> {
    return this.loggedOnSubject.asObservable().pipe(share());
  }

  get LoggedOn() {
    return this.loggedOnSubject.value;
  }

  get isLoggedOn$() {
    return this.isLoggedOnSubject.asObservable().pipe(share());
  }

  get isLoggedOn() {
    return this.isLoggedOnSubject.value;
  }

  async loginWithCode() {
    try {
      await this.configureOauth(this.codeFlow);
      this.oauth.initCodeFlow();
    } catch (error) {
      this.toastr.error(`cannot login: ${error}`);
    }
  }

  async loginWithPass(userName: string, password: string) {
    await this.configureOauth(this.passFlow);
    try {
      await this.oauth.fetchTokenUsingPasswordFlow(userName, password);

    } catch (error) {
      this.toastr.error(`Incorrect username or password`);
    }
  }

  async logout() {
    this.loggedOnSubject.next(null);
    this.isLoggedOnSubject.next(false);
    this.oauth.logOut();
    this.router.navigate(['entry/login']);
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
      this.router.navigate(['home']);
    }
  }

  lastLoginDateUpdate(id){
    return this.http.get(`${environment.backendurl}/api/users/logdate/${id}`);
  }
}
