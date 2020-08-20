import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from './components/profile/profile.component';
import { AuthGuard } from '../core/guards/authGuard';

export const routes: Routes = [{ path: '', component: ProfileComponent, canActivate: [AuthGuard] }];

@NgModule({
  declarations: [ProfileComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
  ],
  exports: [ProfileComponent]
})
export class UserModule { }
