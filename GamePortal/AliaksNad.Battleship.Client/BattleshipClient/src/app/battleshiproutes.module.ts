import { LogoutResolver } from './core/resolvers/logout.resolver';
import { UserModule } from './user/user.module';
import { CoreModule } from './core/core.module';
import { NotFoundComponent } from './core/components/notfound/notfound.component';
import { ProfileComponent } from './user/components/profile/profile.component';
import { LoginComponent } from './core/components/login/login.component';
import { HomeComponent } from './core/components/home/home.component';
import { AuthGuard } from './core/guards/authGuard';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes, PreloadAllModules } from '@angular/router';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  {
    path: 'play', loadChildren: () =>
      import('./battleship/battleship.module')
        .then(d => d.BattleshipModule)
  },
  { path: 'login', component: LoginComponent },
  {
    path: 'logout',
    resolve: { data: LogoutResolver },
    component: HomeComponent,
  },
  { path: 'user', loadChildren: () => import('./user/user.module').then(u => u.UserModule) },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes, { enableTracing: false, preloadingStrategy: PreloadAllModules }),
  ],
  exports: [RouterModule]
})
export class BattleshipRoutesModule { }
