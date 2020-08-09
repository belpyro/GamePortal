import { Component, OnInit, ViewChild, TemplateRef, NgModule } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { TableUserDto } from '../models/TableUserDto';

@Component({
  selector: 'app-usermanager',
  templateUrl: './usermanager.component.html',
  styleUrls: ['./usermanager.component.scss']
})
export class UsermanagerComponent implements OnInit {

  allIncomeUsers: TableUserDto[];
  selectedAll: any;
  allUsers =  new Array();
  @ViewChild('readOnlyTemplate', {static: false}) readOnlyTemplate: TemplateRef<any>;
  constructor(private router: Router, private userservice: UserService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.userservice.getAllUsers().subscribe(
      res => {
        this.allIncomeUsers = res;
        for (const num of this.allIncomeUsers) {
          this.allUsers.push(  {
            id: num.Id,
            username: num.UserName,
            regdate: num.RegistrationDate,
            logdate: num.LastLoginDate,
            blocked: num.IsBlocked,
            role: num.UserRole,
            selected: false,
           });
       }
      });
  }

  loadTemplate(allIncomeUsers) {
    return this.readOnlyTemplate;
}
  initUsers(){
    this.userservice.getAllUsers().subscribe(
      res => {
        this.allIncomeUsers = res;
        for (const num of this.allIncomeUsers) {
          this.allUsers.push(  {
            id: num.Id,
            username: num.UserName,
            regdate: num.RegistrationDate,
            logdate: num.LastLoginDate,
            blocked: num.IsBlocked,
            role: num.UserRole,
            selected: false,
           });
       }
      });
  }

  selectAll() {
    for ( const user of this.allUsers) {
      user.selected = this.selectedAll;
    }
  }

  onBlock(){
    for (let i = 0; i < this.allUsers.length; i++) {
      if (this.allUsers[i].selected === true)
      {
        this.allUsers[i].blocked = true;
        this.userservice.block(this.allUsers[i].id).subscribe();
      }
    }
  }

  onUnBlock(){
    for (let i = 0; i < this.allUsers.length; i++) {
      if (this.allUsers[i].selected === true)
      {
        this.allUsers[i].blocked = false;
        this.userservice.unblock(this.allUsers[i].id).subscribe();
      }
    }
  }

  onDelete(){
    for (let i = 0; i < this.allUsers.length; i++) {
      if (this.allUsers[i].selected === true)
      {
        this.userservice.delete(this.allUsers[i].id).subscribe();
        this.allUsers.splice(i, i);
        i--;
      }
    }
  }

  checkIfAnySelected() {
    for (const user of this.allUsers) {
      if (user.selected === true)
      {
        return true;
      }
    }
    return false;
  }
}
