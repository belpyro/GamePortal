import { CellComponent } from './components/cell/cell.component';
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

export const routes: Routes = [
  { path: '', component: BattlefieldComponent, canActivate: [AuthGuard] },
];

@NgModule({
  declarations: [
    AreaComponent,
    BattlefieldComponent,
    GameBoardComponent,
    CellComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forChild(routes),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptors, multi: true },
  ],
  exports: [AreaComponent, BattlefieldComponent, GameBoardComponent, CellComponent]
})
export class BattleshipModule { }
