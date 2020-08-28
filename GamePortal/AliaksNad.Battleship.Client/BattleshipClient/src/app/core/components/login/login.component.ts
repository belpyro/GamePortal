import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { filter } from 'rxjs/internal/operators/filter';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { LoginService } from '../../services/externalLogin.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  submitted = false;

  constructor(
    private loginService: LoginService,
    private router: Router,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.loginService.LoggedOn$.subscribe((_) => {
      this.router.navigate(['play']);
    });

    this.loginForm = this.formBuilder.group({
      userName: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(3)]],
      remember: ['true']
    }, {});
  }

  onSubmit() {
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    // alert('SUCCESS!! :-)\n\n' + JSON.stringify(this.loginForm.value))
    this.loginWithPassword();
  }

  get formControls() { return this.loginForm.controls; }

  login(): void {
    this.loginService.loginWithCode();
  }

  loginWithPassword(): void {
    this.loginService.loginWithPass(
      this.loginForm.value.userName,
      this.loginForm.value.password);
  }

}
