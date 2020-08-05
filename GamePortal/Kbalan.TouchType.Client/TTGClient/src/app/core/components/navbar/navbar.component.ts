
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

  constructor(
    private loginService: LoginService,
  ) {}

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
