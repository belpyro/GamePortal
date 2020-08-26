import { Component, OnInit, ViewChild, TemplateRef, NgModule } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { TableUserDto } from '../models/TableUserDto';
import { LoginService } from 'src/app/core/services/login.service';
import { UserDto } from 'src/app/core/models/UserDto';
import { Observable } from 'rxjs';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-usermanager',
  templateUrl: './usermanager.component.html',
  styleUrls: ['./usermanager.component.scss']
})
export class UsermanagerComponent implements OnInit {

  allIncomeUsers: TableUserDto[];
  selectedAll: any;
  allUsers =  new Array();
  user$: Observable<UserDto>;
  @ViewChild('readOnlyTemplate', {static: false}) readOnlyTemplate: TemplateRef<any>;
  constructor(
    private router: Router,
    private userservice: UserService,
    private toastr: ToastrService,
    private loginService: LoginService) { }

  ngOnInit(): void {
    this.initUsers();
    this.user$ = this.loginService.LoggedOn$;
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
    // tslint:disable-next-line: prefer-for-of
    for (let i = 0; i < this.allUsers.length; i++) {
      if (this.allUsers[i].selected === true)
      {
        if (this.allUsers[i].id !== this.loginService.LoggedOn.sub)
        {
          this.allUsers[i].blocked = true;
          this.userservice.block(this.allUsers[i].id).subscribe();
        }
        else{
          Swal.fire({
            title: 'Do you want to block yourself?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, block me!'
          }).then((willDelete) => {
          if (willDelete.isConfirmed) {
            this.allUsers[i].blocked = true;
            this.userservice.block(this.allUsers[i].id).subscribe();
          }
        });
        }
      }
    }
  }


  onUnBlock(){
    // tslint:disable-next-line: prefer-for-of
    for (let i = 0; i < this.allUsers.length; i++) {
      if (this.allUsers[i].selected === true)
      {
        this.allUsers[i].blocked = false;
        this.userservice.unblock(this.allUsers[i].id).subscribe();
      }
    }
  }

  onDelete(){
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to revert this!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((willDelete) => {
    if (willDelete.isConfirmed) {
    for (let i = 0; i < this.allUsers.length; i++) {
      if (this.allUsers[i].selected === true)
      {
        if (this.allUsers[i].id !== this.loginService.LoggedOn.sub)
        {
          this.userservice.delete(this.allUsers[i].id).subscribe();
          this.allUsers.splice(i, 1);
          i--;
        }else
        {
          Swal.fire({
            title: 'Are you sure you want to delete yourself?',
            text: 'You will not be able to revert this!',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
          }).then((deleteYourSelf) => {
          if (deleteYourSelf.isConfirmed) {
            this.userservice.delete(this.allUsers[i].id).subscribe();
            this.allUsers.splice(i, 1);
            i--;
            this.loginService.logout();
          }
          });
        }
      }
    }
  }
  });
  }

  onRoleChange(){
    // tslint:disable-next-line: prefer-for-of
    for (let i = 0; i < this.allUsers.length; i++) {
      if (this.allUsers[i].selected === true)
      {
        if (this.allUsers[i].role === 'user'){
          this.allUsers[i].role  = 'administrator';
          this.userservice.mkRoleAdmin(this.allUsers[i].id).subscribe();
        } else{
          this.allUsers[i].role  = 'user';
          this.userservice.mkRoleUser(this.allUsers[i].id).subscribe();
        }
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
