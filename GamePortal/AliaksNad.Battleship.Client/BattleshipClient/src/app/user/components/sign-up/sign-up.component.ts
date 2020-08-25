import { LoginService } from '../../../core/services/login.service';
import { UserService } from './../../services/user.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {

  signupGroup: FormGroup;
  subscription$: Subscription;

  constructor(private fb: FormBuilder, private userService: UserService, private loginService: LoginService) {
    this.signupGroup = this.fb.group({
      userName: [''],
      userEmail: [''],
      userPassword: [''],
      userConfirmPassword: [''],
    });
  }

  ngOnInit(): void {
  }

  ngOnDestroy() {
  }

  signUp(): void {
    this.subscription$ = this.userService.userRegister({
      userName: this.signupGroup.value.userName,
      email: this.signupGroup.value.userEmail,
      password: this.signupGroup.value.userPassword,
    }).pipe(take(1)).subscribe(() => this.loginService.loginWithCode());
  }

  signUpWithGoogle(): void {

  }

}
