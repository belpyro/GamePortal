import { Component, OnInit } from '@angular/core';
import { HomeService } from '../../services/home.service';
import { UserProfileDto } from '../../models/UserProfileDto';
import { Observable } from 'rxjs';
import { UserDto } from 'src/app/core/models/UserDto';
import { LoginService } from 'src/app/core/services/login.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';


@Component({
  selector: 'app-homecomponent',
  templateUrl: './homecomponent.component.html',
  styleUrls: ['./homecomponent.component.scss']
})
export class HomecomponentComponent implements OnInit {
  incomeUser: UserProfileDto;
  user$: Observable<UserDto>;
  isLoggedOn$: Observable<boolean>;
  loggedUser: UserDto;
  LevelGroup: FormGroup;
  constructor(private homeservice: HomeService, private loginService: LoginService, private fb: FormBuilder) {
    this.LevelGroup = this.fb.group({
      textradio: ['', [Validators.required]]
    });
   }

  ngOnInit(): void {
    this.user$ = this.loginService.LoggedOn$;
    this.isLoggedOn$ = this.loginService.isLoggedOn$;
    this.user$.subscribe(event => this.loggedUser = event);
    this.initUser();
  }
  async initUser(){
    await this.homeservice.getUser(this.loggedUser.sub).subscribe(res => {
      this.incomeUser = res;
      this.LevelGroup.setValue({
      textradio: this.incomeUser.Setting.LevelOfText }); } );

  }
}
