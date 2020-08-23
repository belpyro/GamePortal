import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegistrationService } from '../../services/registration.service';
import { NewUserDto } from '../../models/NewUserDto';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  registrationGroup = new FormGroup({});
  constructor(
    private toastr: ToastrService,
    private fb: FormBuilder,
    private  registrationService: RegistrationService)
  {
    this.registrationGroup = this.fb.group({
      UserName : ['', Validators.required],
      Email : ['', [Validators.required, Validators.email]],
      Passwords: this.fb.group({
        Password : ['', Validators.required],
        ConfirmPassword : ['', Validators.required]
      }, {validators : this.comparePasswords})
    });
  }

  comparePasswords(fb: FormGroup)
  {
    const confirmPswdCtrl = fb.get('ConfirmPassword');
    if (confirmPswdCtrl.errors == null || 'passwordMismatch' in confirmPswdCtrl.errors)
    {
      if (fb.get('Password').value !== confirmPswdCtrl.value)
      {
        confirmPswdCtrl.setErrors({passwordMismatch: true});
      }
      else
      {
        confirmPswdCtrl.setErrors(null);
      }
    }
  }



  ngOnInit(): void {
  }
  onSubmit(){
    const body: NewUserDto  = {
      username: this.registrationGroup.value.UserName,
      email: this.registrationGroup.value.Email,
      password: this.registrationGroup.value.Passwords.Password
    };
    this.registrationService.register(body).subscribe(
      (res: any) => {
        if (res != null)
        {
          this.toastr.success(`User ${body.username} successfully registered `);
          this.registrationGroup.reset();
        }
      },
      err => {
        if ( err.status === 400 )
        {
          this.toastr.error(err.error.Message);
        }
        if ( err.status === 500)
        {
          this.toastr.error('Something gone wrong. Try again later');
        }
      });
  }
}

