import { CoreModule } from './core/core.module';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { RegistrationComponent } from './core/components/registration/registration.component';
import { LoginComponent } from './core/components/login/login.component';
import { TextblockComponent } from './text/component/textblock/textblock.component';
import { NotFoundComponent } from './core/components/not-found/not-found.component';
import { EntryMenuComponent } from './core/components/entrymenu/entrymenu.component';
import { CommonModule } from '@angular/common';
import { TextModule } from './text/text.module';
import { UsermanagerComponent } from './user/usermanager/usermanager.component';
import { UserModule } from './user/user.module';


const routes: Routes = [
  {path: 'entry', component: EntryMenuComponent, children: [
    {path: 'registration', component: RegistrationComponent},
    {path: 'login', component: LoginComponent}
  ]
},
{path: 'text', component: TextblockComponent, canActivate: [AuthGuard]},
{path: 'users', component: UsermanagerComponent, canActivate: [AuthGuard]},
{path: '', redirectTo: '/entry/login', pathMatch: 'full'},
{path: '**', component: NotFoundComponent}
];

@NgModule({
  imports: [
    CommonModule,
    CoreModule,
    TextModule,
    UserModule,
    RouterModule.forRoot(routes)
  ],
  exports: [CoreModule , RouterModule]
})
export class AppRoutingModule { }
