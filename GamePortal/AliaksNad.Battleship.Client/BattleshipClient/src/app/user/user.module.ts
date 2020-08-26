import { ExternalLoginService } from './services/external.login.service';
import { UserService } from './services/user.service';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from './components/profile/profile.component';
import { AuthGuard } from '../core/guards/authGuard';
import { SignUpComponent } from './components/sign-up/sign-up.component';

export const routes: Routes = [
  { path: '', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'signup', component: SignUpComponent },
];

@NgModule({
  declarations: [ProfileComponent, SignUpComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
  ],
  providers: [UserService, ExternalLoginService],
  exports: [ProfileComponent, SignUpComponent]
})
export class UserModule { }
