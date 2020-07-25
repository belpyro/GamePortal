import { NotFoundComponent } from './../components/main/notfound/notfound.component';
import { LoginComponent } from './../components/security/login/login.component';
import { AreaComponent } from './../components/game/area/area.component';
import { HomeComponent } from './../components/main/home/home.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { GameBoardComponent } from '../components/game/game-board/game-board.component';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'play', component: GameBoardComponent },
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class BattleshipRoutesModule { }
