import { LoginService } from './../../../services/login.service';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import {filter} from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private toastr: ToastrService, private loginService: LoginService,
     private router: Router) { }

  ngOnInit(): void {
    this.loginService.LoggedOn$.pipe(filter(_ => _)).subscribe( _ => {
        this.router.navigate(['text']);
    });
  }


  onSubmit(){
    this.loginService.login();
  }
  showError(){
    this.toastr.error('Loggin error');
  }
  showSuccess() {
    this.toastr.success('Success');
  }
}
