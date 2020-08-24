import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { UserProfileDto } from 'src/app/home/models/UserProfileDto';
import { StatisticService } from '../../services/statistic.service';
import { UserStatisticDto } from '../../models/UserStatisticDto';

@Component({
  selector: 'app-statistic',
  templateUrl: './statistic.component.html',
  styleUrls: ['./statistic.component.scss']
})
export class StatisticComponent implements OnInit {
  allUsers: UserStatisticDto[];

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
      this.allUsers = res;  this.sortbyMaxSpeed();
    });

}
sortbyMaxSpeed(){
  this.allUsers = this.allUsers.sort(((n1, n2) => {
    if (n1.Statistic.MaxSpeedRecord < n2.Statistic.MaxSpeedRecord) {
        return 1;
    }

    if (n1.Statistic.MaxSpeedRecord > n2.Statistic.MaxSpeedRecord) {
        return -1;
    }

    return 0;
}));
}
}
