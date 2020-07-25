import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { TextblockComponent } from './components/text/textblock/textblock.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { TextGuard } from './text.guard';


const routes: Routes = [
  {path: 'user', component: UserComponent, children: [
    {path: 'registration', component: RegistrationComponent},
    {path: 'login', component: LoginComponent}
  ]
},
{path: 'text', component: TextblockComponent, canActivate: [TextGuard]},
{path: '', redirectTo: '/user/login', pathMatch: 'full'},
{path: '**', component: NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
