import { SettingDto } from './../../models/SettingDto';
import { Component, OnInit } from '@angular/core';
import { HomeService } from '../../services/home.service';
import { UserProfileDto } from '../../models/UserProfileDto';
import { Observable } from 'rxjs';
import { UserDto } from 'src/app/core/models/UserDto';
import { LoginService } from 'src/app/core/services/login.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { url } from 'inspector';


@Component({
  selector: 'app-homecomponent',
  templateUrl: './homecomponent.component.html',
  styleUrls: ['./homecomponent.component.scss']
})
export class HomecomponentComponent implements OnInit {
  userProfile?: UserProfileDto;
  loggedUser: UserDto;
  LevelGroup: FormGroup;
  linkPicture: string;
  timeStamp;
  constructor(private homeservice: HomeService, private loginService: LoginService, private fb: FormBuilder) {
    this.LevelGroup = this.fb.group({
      textradio: ['', [Validators.required]]
    });
   }

  ngOnInit(): void {
    this.initUser();
  }
   uploadFinished = (event) => {
    this.userProfile.Setting.Avatar = event;
    this.updateSetting();
    this.setLinkPicture();
    this.createImgPath();
  }
  updateSetting(){
    const setting = {
      SettingId: this.userProfile.Setting.SettingId,
      Avatar: this.userProfile.Setting.Avatar,
      LevelOfText: this.userProfile.Setting.LevelOfText
  } as SettingDto;
    this.homeservice.updateSetting(setting).subscribe();
  }
  changeLevel(){
    this.userProfile.Setting.LevelOfText = this.LevelGroup.value.textradio;
    this.updateSetting();
  }
  disableChangeLevelBtn(){
    if (this.userProfile.Setting.LevelOfText === this.LevelGroup.value.textradio)
    {
      return true;
    }else{
      return false;
    }
  }
  async initUser(){
    await this.homeservice.getUser().subscribe(res => {
      this.userProfile = res;
      this.setLinkPicture();
      this.LevelGroup.setValue({
      textradio: this.userProfile.Setting.LevelOfText }); } );

  }
  public createImgPath = () => {
    if (this.userProfile.Setting.Avatar === null)
    {
      return './../../../../assets/No_avatar.jpg';
    }
    if (this.timeStamp) {
      return this.linkPicture + '?' + this.timeStamp;
   }
    return this.linkPicture;
  }
  public setLinkPicture() {
    this.linkPicture = `${environment.backendurl}/${this.userProfile.Setting.Avatar}`;
    this.timeStamp = (new Date()).getTime();
}
}
