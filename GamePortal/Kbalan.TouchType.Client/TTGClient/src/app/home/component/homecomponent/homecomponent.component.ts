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
  userProfile?: UserProfileDto;
  loggedUser: UserDto;
  LevelGroup: FormGroup;
  constructor(private homeservice: HomeService, private loginService: LoginService, private fb: FormBuilder) {
    this.LevelGroup = this.fb.group({
      textradio: ['', [Validators.required]]
    });
   }

  ngOnInit(): void {
    this.initUser();
  }
   uploadFinished = (event) => {
    this.userProfile.Avatar = event;
    console.log(this.userProfile.Avatar);
  }
  async initUser(){
    await this.homeservice.getUser().subscribe(res => {
      this.userProfile = res;
      this.LevelGroup.setValue({
      textradio: this.userProfile.Setting.LevelOfText }); } );

  }
}
