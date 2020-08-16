import { Component, OnInit } from '@angular/core';
import { LoginService } from './../../../services/login.service';
import { Router } from '@angular/router';
import { filter } from 'rxjs/internal/operators/filter';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginGroup: FormGroup;
  //   {
  //   email: new FormControl('', [Validators.required, Validators.email]),
  //   pass: new FormControl('', [Validators.required]),
  //   remember: new FormControl('true'),
  // });

  // emailControl = new FormControl('', [Validators.required, Validators.email]);
  // passControl = new FormControl('', [Validators.required]);


  constructor(
    private loginService: LoginService,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.loginGroup = this.fb.group({
      userName: [''],
      password: [''],
      remember: ['true'],
    });
  }

  ngOnInit(): void {
    this.loginService.LoggedOn$.pipe(filter(_ => _)).subscribe(_ => {
      this.router.navigate(['play']);
    });
  }

  login(): void {
    this.loginService.login();
  }

  loginWithPassword(): void {
    console.log('log');
    console.log(this.loginGroup.value);
    this.loginService.login(
      this.loginGroup.value.userName,
      this.loginGroup.value.password);
  }

}
