import { GameBoardService } from './services/game-board.service';
import { GameBoardComponent } from './components/game-board/game-board.component';
import { BattlefieldComponent } from './components/battlefield/battlefield.component';
import { AreaComponent } from './components/area/area.component';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthGuard } from '../core/guards/authGuard';
import { AuthInterceptors } from '../core/interceptors/auth.interceptors';
import { BattleshipGameService } from './services/battleshipGame.service';
import { OwnAreaComponent } from './components/own-area/own-area.component';
import { EnemyAreaComponent } from './components/enemy-area/enemy-area.component';

export const routes: Routes = [
  { path: 'test', component: BattlefieldComponent, canActivate: [AuthGuard] },
  { path: '', component: GameBoardComponent, canActivate: [AuthGuard] },
];

@NgModule({
  declarations: [
    AreaComponent,
    BattlefieldComponent,
    GameBoardComponent,
    OwnAreaComponent,
    EnemyAreaComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forChild(routes),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptors, multi: true },
    BattleshipGameService,
    GameBoardService,
  ],
  exports: [AreaComponent, BattlefieldComponent, GameBoardComponent]
})
export class BattleshipModule { }
