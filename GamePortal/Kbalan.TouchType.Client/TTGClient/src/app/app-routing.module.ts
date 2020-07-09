import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { TextblockComponent } from './components/text/textblock/textblock.component';


const routes: Routes = [
  {path: '', redirectTo: '/user/login', pathMatch: 'full'},
  {path: 'user', component: UserComponent, children: [
    {path: 'registration', component: RegistrationComponent},
    {path: 'login', component: LoginComponent}
  ]
},
{path: 'text', component: TextblockComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
