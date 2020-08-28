
import { UserDto } from '../../models/UserDto';
import { LoginService } from '../../../core/services/login.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { SignalR, ISignalRConnection } from 'ng2-signalr';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit, OnDestroy {
  private connection: ISignalRConnection;
  message: string;
  email: string;
  user$: Observable<UserDto>;
  isLoggedOn$: Observable<boolean>;
  token;
  user;
  constructor(
    private loginService: LoginService, private toastr: ToastrService,  private hub: SignalR, private router: Router,
  ) {
    this.token = sessionStorage.getItem('id_token_claims_obj');
    this.user  = JSON.parse(this.token);
}

  ngOnDestroy(): void {
  }

  ngOnInit(): void {
    this.hub
    .connect()
    .then((c) => {
      this.connection = c;
      this.connection
      .listenFor<string>('DeleteUser')
      .subscribe((dto) => {
        if (this.user.sub === dto)
        {
          this.toastr.warning('You were deleted by administrator');
          this.router.navigate(['entry/login']);
        }

      } );
    })
    .catch((reason) =>
      console.error(`Cannot connect to hub sample ${reason}`)
    );
    this.user$ = this.loginService.LoggedOn$;
    this.isLoggedOn$ = this.loginService.isLoggedOn$;

  }

  logout(){
    this.loginService.logout();
  }
}
