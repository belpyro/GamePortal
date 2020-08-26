
import { UserDto } from '../../models/UserDto';
import { LoginService } from '../../../core/services/login.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit, OnDestroy {
  message: string;
  email: string;
  user$: Observable<UserDto>;
  isLoggedOn$: Observable<boolean>;
  token;
  user;
  constructor(
    private loginService: LoginService,
  ) {
    this.token = sessionStorage.getItem('id_token_claims_obj');
    this.user  = JSON.parse(this.token);
}

  ngOnDestroy(): void {
  }

  ngOnInit(): void {
    this.user$ = this.loginService.LoggedOn$;
    this.isLoggedOn$ = this.loginService.isLoggedOn$;
  }

  logout(){
    this.loginService.logout();
  }
}
