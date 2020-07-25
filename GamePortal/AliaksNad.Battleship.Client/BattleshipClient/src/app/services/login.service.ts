import { UserDto } from './../models/UserDto';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private loggedOnSubject = new BehaviorSubject<boolean>(false);
  private user: UserDto;

  constructor() { }

  get LoggedOn$() {
    return this.loggedOnSubject.asObservable();
  }

  login(userName?: string, password?: string) {
    this.user = { uid: '111-123', fullName: 'Evil' };
    this.loggedOnSubject.next(true);
  }

  logout() {
    this.user = null;
    this.loggedOnSubject.next(false);
  }
}
