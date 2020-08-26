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
  loginGroup: FormGroup;
  registerForm: FormGroup;
  submitted = false;



  constructor(
    private loginService: LoginService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.loginGroup = this.formBuilder.group({
      userName: [''],
      password: [''],
      remember: ['true'],
    });
  }

  ngOnInit(): void {
    this.loginService.LoggedOn$.subscribe((_) => {
      this.router.navigate(['play']);
    });

    this.registerForm = this.formBuilder.group({
      name: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
    }, {
      });
  }

  onSubmit() {
    this.submitted = true;

    if (this.registerForm.invalid) {
      return;
    }

    alert('SUCCESS!! :-)\n\n' + JSON.stringify(this.registerForm.value))
    this.loginWithPassword();
  }

  get formControls() { return this.registerForm.controls; }


  login(): void {
    this.loginService.loginWithCode();
  }

  loginWithPassword(): void {
    this.loginService.loginWithPass(
      this.loginGroup.value.userName,
      this.loginGroup.value.password);
  }

}
