import { LoginService } from '../../services/login.service';
import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { NotificationService } from '../../services/notification.service';
import { UserDto } from '../../models/UserDto';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  message: string;
  email: string;
  user$: Observable<UserDto>;
  isLogged = false;
  // private subscription: Subscription;

  ngOnDestroy(): void {
    // this.subscription.unsubscribe();

  }

  constructor(
    private ntf: NotificationService,
    private loginService: LoginService,
    private oauth: OAuthService) { }

  ngOnInit(): void {
    this.ntf.Message$.subscribe(msg => this.message = msg);
    // this.subscription = this.loginService.LoggedOn$.subscribe();
    this.user$ = this.loginService.LoggedOn$;
  }

  logout() {
    this.loginService.logout();
  }
}
