import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { UserDto } from '../models/UserDto';
import { Router } from '@angular/router';

@Injectable({providedIn: 'root'})
export class LoginService {

  private loggenOnSubject = new BehaviorSubject<boolean>(false);
  private user: UserDto;
  constructor(private router: Router) { }

  get LoggedOn$(){
    return this.loggenOnSubject.asObservable();
  }

  get LoggedOn(){
    return this.loggenOnSubject.value;
  }

  login(userName?: string, password?: string){
      setTimeout(() => {
this.user = { uid: '111', fullName: 'Kirill Balanovich' },
this.loggenOnSubject.next(true);

      }, 500);
  }
  logout(){
    this.user = null;
    this.loggenOnSubject.next(false);
    this.router.navigate(['user/login']);
  }
}
