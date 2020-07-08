import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  constructor(private toastr: ToastrService) { }

  ngOnInit(): void {
  }
  onSubmit(){
    this.showError();
  }
  showError(){
    this.toastr.error('Loggin error');
  }
  showSuccess() {
    this.toastr.success('Success');
  }
}

