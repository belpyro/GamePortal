import { LoginService } from '../../../core/services/login.service';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import {filter} from 'rxjs/operators';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginGroup = new FormGroup({});
  constructor(
    private toastr: ToastrService,
    private loginService: LoginService,
    private router: Router,
    private fb: FormBuilder
    ) {
      this.loginGroup = this.fb.group({
        username: ['', [Validators.required]],
        password: ['', [Validators.required]],
        remember: [true],
      });
    }

  ngOnInit(): void {
    this.loginService.LoggedOn$.subscribe((_) => {
        this.router.navigate(['text']);
    });
  }


  login(){
    this.loginService.loginWithCode();
  }

  loginWithPassword() {
    this.loginService.loginWithPass(
      this.loginGroup.value.username,
      this.loginGroup.value.password
    );
  }
  showError(){
    this.toastr.error('Loggin error');
  }
  showSuccess() {
    this.toastr.success('Success');
  }
}
