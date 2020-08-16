import { LoginService } from '../../services/login.service';
import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { map } from 'rxjs/operators';
import { NotificationService } from '../../services/notification.service';
import { UserDto } from '../../models/UserDto';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  message: string;
  isLogged = false;
  email: string;

  constructor(
    private ntf: NotificationService,
    public loginService: LoginService,
    private oauth: OAuthService) { }

  ngOnInit(): void {
    this.ntf.Message$.subscribe(msg => this.message = msg);
    this.loginService.LoggedOn$.pipe(
      map((_) => this.oauth.getIdentityClaims())
    ).subscribe((obj) => {
      this.isLogged = (obj != null);
      const user = Object.assign({} as UserDto, obj);
      this.email = user.email;
    });
  }
}
