import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { UserProfileDto } from 'src/app/home/models/UserProfileDto';
import { StatisticService } from '../../services/statistic.service';
import { UserStatisticDto } from '../../models/UserStatisticDto';
import { SgameService } from 'src/app/sgame/services/sgame.service';
import Swal from 'sweetalert2';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-statistic',
  templateUrl: './statistic.component.html',
  styleUrls: ['./statistic.component.scss']
})
export class StatisticComponent implements OnInit {
  userProfile?: UserProfileDto;
  allUsers: UserStatisticDto[];
  descent = true;
  linkPicture: string;
  timeStamp;
  @ViewChild('readOnlyTemplate', {static: false}) readOnlyTemplate: TemplateRef<any>;
  constructor(private statisticservice: StatisticService) { }

  ngOnInit(): void {
    this.initUsers();
  }

  loadTemplate(allUsers) {
    return this.readOnlyTemplate;
}
initUsers(){
  this.statisticservice.getAllUsers().subscribe(
    res => {
      this.allUsers = res;
      for (const user of this.allUsers) {
           user.Statistic.AvarageSpeed =  Math.round((user.Statistic.AvarageSpeed + Number.EPSILON) * 100) / 100;
      }
      this.sortbyNickName();
    });

}
sortbyMaxSpeed(){
  this.descent = !this.descent;
  this.allUsers = this.allUsers.sort(((n1, n2) => {
    if (n1.Statistic.MaxSpeedRecord < n2.Statistic.MaxSpeedRecord) {
      if (!this.descent)
      {
        return 1;
      }else{
        return -1;
      }

    }
    if (n1.Statistic.MaxSpeedRecord > n2.Statistic.MaxSpeedRecord) {
      if (!this.descent)
      {
        return -1;
      }else{
        return 1;
      }
    }
    return 0;
}));
}
sortbyAvarageSpeed(){
  this.descent = !this.descent;
  this.allUsers = this.allUsers.sort(((n1, n2) => {
    if (n1.Statistic.AvarageSpeed < n2.Statistic.AvarageSpeed) {
      if (!this.descent)
      {
        return 1;
      }else{
        return -1;
      }
    }
    if (n1.Statistic.AvarageSpeed > n2.Statistic.AvarageSpeed) {
      if (!this.descent)
      {
        return -1;
      }else{
        return 1;
      }
    }
    return 0;
}));
}
sortbyNumberOfGamesPlayed(){
  this.descent = !this.descent;
  this.allUsers = this.allUsers.sort(((n1, n2) => {
    if (n1.Statistic.NumberOfGamesPlayed < n2.Statistic.NumberOfGamesPlayed) {
      if (!this.descent)
      {
        return 1;
      }else{
        return -1;
      }
    }
    if (n1.Statistic.NumberOfGamesPlayed > n2.Statistic.NumberOfGamesPlayed) {
      if (!this.descent)
      {
        return -1;
      }else{
        return 1;
      }
    }
    return 0;
}));
}
sortbyNickName(){
  this.descent = !this.descent;
  this.allUsers = this.allUsers.sort(((n1, n2) => {
    if (n1.Username > n2.Username) {
      if (!this.descent)
      {
        return 1;
      }else{
        return -1;
      }
    }
    if (n1.Username < n2.Username) {
      if (!this.descent)
      {
        return -1;
      }else{
        return 1;
      }
    }

    return 0;
}));
}
  async showUserInfo(userId){
  this.userProfile = await this.statisticservice.getUser(userId);
  this.setLinkPicture();
  Swal.fire({
    title: `${this.userProfile.Username}`,
    text: `${this.userProfile.Email}`,
    imageUrl: this.createImgPath(),
    imageHeight: 300,
    confirmButtonColor: '#3085d6',
    confirmButtonText: 'Back'
  });
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
