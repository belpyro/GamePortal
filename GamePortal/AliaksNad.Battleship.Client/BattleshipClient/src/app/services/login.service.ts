import { UserDto } from './../models/UserDto';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private loggedOnSubject = new BehaviorSubject<boolean>(true);
  private user: UserDto;

  constructor(private router: Router) { }

  get LoggedOn$() {
    return this.loggedOnSubject.asObservable();
  }

  get LoggedOn() {
    return this.loggedOnSubject.value;
  }

  login(userName?: string, password?: string) {
    this.user = { uid: '111-123', fullName: 'Evil' };
    this.loggedOnSubject.next(true);
  }

  logout() {
    this.user = null;
    this.loggedOnSubject.next(false);
    this.router.navigate(['home']);
  }
}
