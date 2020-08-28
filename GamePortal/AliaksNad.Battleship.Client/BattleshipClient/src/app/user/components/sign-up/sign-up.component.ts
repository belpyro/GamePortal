import { ExternalLoginService } from './../../services/external.login.service';
import { LoginService } from '../../../core/services/login.service';
import { UserService } from './../../services/user.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {

  subscription$: Subscription;
  registerForm: FormGroup;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private loginService: LoginService,
    private extloginService: ExternalLoginService
  ) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    }, {
        validator: this.MustMatch('password', 'confirmPassword')
      });
  }

  get formControls() { return this.registerForm.controls; }

  signUp(): void {
    this.subscription$ = this.userService.userRegister({
      userName: this.registerForm.value.name,
      email: this.registerForm.value.email,
      password: this.registerForm.value.password,
    }).pipe(take(1)).subscribe(() => this.loginService.loginWithCode());
  }

  signUpWithGoogle(): void {
    this.extloginService.loginGoogleLogin()
      .subscribe(() => alert('google login'));
  }

  onSubmit() {
    this.submitted = true;

    if (this.registerForm.invalid) {
      return;
    }

    alert('SUCCESS!! :-)\n\n' + JSON.stringify(this.registerForm.value))
    this.signUp();
  }

  MustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors.mustMatch) {
        // return if another validator has already found an error on the matchingControl
        return;
      }

      // set error on matchingControl if validation fails
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true });
      } else {
        matchingControl.setErrors(null);
      }
    }
  }
}
