import { CellComponent } from './components/cell/cell.component';
import { GameBoardComponent } from './components/game-board/game-board.component';
import { BattlefieldComponent } from './components/battlefield/battlefield.component';
import { AreaComponent } from './components/area/area.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

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
    RouterModule,
  ],
  exports: [AreaComponent, BattlefieldComponent, GameBoardComponent, CellComponent]
})
export class BattleshipModule { }
