import { StatisticModule } from './Statistic/statistic.module';
import { HomecomponentComponent } from './home/component/homecomponent/homecomponent.component';
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
import { HomeModule } from './home/home.module';
import { SgameComponent } from './sgame/component/sgame/sgame.component';
import { SgameModule } from './sgame/sgame.module';
import { StatisticComponent } from './Statistic/component/statistic/statistic.component';


const routes: Routes = [
  {path: 'entry', component: EntryMenuComponent, children: [
    {path: 'registration', component: RegistrationComponent},
    {path: 'login', component: LoginComponent}
  ]
},
{path: 'sgame', component: SgameComponent, canActivate: [AuthGuard]},
{path: 'text', component: TextblockComponent, canActivate: [AuthGuard],
data: {
  role: 'administrator'
}
},
{path: 'users', component: UsermanagerComponent, canActivate: [AuthGuard],
data: {
  role: 'administrator'
}
},
{path: 'statistic', component: StatisticComponent, canActivate: [AuthGuard]},
{path: 'home', component: HomecomponentComponent, canActivate: [AuthGuard]},
{path: '', redirectTo: '/entry/login', pathMatch: 'full'},
{path: '**', component: NotFoundComponent}
];

@NgModule({
  imports: [
    HomeModule,
    CommonModule,
    CoreModule,
    TextModule,
    UserModule,
    SgameModule,
    StatisticModule,
    RouterModule.forRoot(routes)
  ],
  exports: [CoreModule , RouterModule]
})
export class AppRoutingModule { }
