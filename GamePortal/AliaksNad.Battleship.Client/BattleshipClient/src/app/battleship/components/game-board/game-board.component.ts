import { GameBoardService } from './../../services/game-board.service';
import { AreaService } from './../../services/areaService';
import { BattleshipGameService } from './../../services/battleshipGame.service';
import { Component, OnInit, Input } from '@angular/core';
import { BattleAreaDto } from '../../models/BattleAreaDto';
import { SignalR, ISignalRConnection } from 'ng2-signalr';
import { NotificationsService } from 'angular2-notifications';

@Component({
  selector: 'app-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.scss']
})
export class GameBoardComponent implements OnInit {

  btlarea: BattleAreaDto[];
  isOwnAreaDisabled = false;
  isEnemyAreaDisabled = true;

  constructor(
    private gameBoardService: GameBoardService,
    private gameService: BattleshipGameService,
  ) {
    this.gameBoardService.isEnemyTurn$.subscribe(
      (result) => this.moveResolution(result));
  }

  moveResolution(bool: boolean): void {
    if (this.isOwnAreaDisabled) {
      this.isEnemyAreaDisabled = bool;
    }
  }

  ngOnInit(): void {
    // this.gameService.battleshipGameGetAll()
    //   .subscribe((data) => { this.btlarea = data; });
  }

  generateFleet(): void {
    this.gameBoardService.generateFleet();
  }

  deleteFleet(): void {
    this.gameBoardService.deleteFleet();
  }

  uploadFleet(): void {
    this.isOwnAreaDisabled = !this.isOwnAreaDisabled;

    this.gameBoardService.uploadArea();
  }

  doSmt(): void {
    this.isOwnAreaDisabled = !this.isOwnAreaDisabled;
    this.isEnemyAreaDisabled = !this.isEnemyAreaDisabled;
  }
}
