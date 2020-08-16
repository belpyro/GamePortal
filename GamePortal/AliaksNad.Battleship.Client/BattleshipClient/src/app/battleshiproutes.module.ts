import { UserModule } from './user/user.module';
import { BattleshipModule } from './battleship/battleship.module';
import { CoreModule } from './core/core.module';
import { NotFoundComponent } from './core/components/notfound/notfound.component';
import { ProfileComponent } from './user/components/profile/profile.component';
import { LoginComponent } from './core/components/login/login.component';
import { GameBoardComponent } from './battleship/components/game-board/game-board.component';
import { HomeComponent } from './core/components/home/home.component';
import { AuthGuard } from './core/guards/authGuard';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'play', component: GameBoardComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    CoreModule.forRoot(),
    BattleshipModule,
    UserModule,
    RouterModule.forRoot(routes, { enableTracing: true })
  ],
  exports: [CoreModule, RouterModule]
})
export class BattleshipRoutesModule { }
