import { GameBoardService } from './../../services/game-board.service';
import { Component, OnInit } from '@angular/core';
import { BattleAreaDto } from '../../models/BattleAreaDto';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.scss']
})
export class GameBoardComponent implements OnInit {

  btlarea: BattleAreaDto[];
  isOwnAreaDisabled = false;
  isEnemyAreaDisabled = true;
  subscription = new Subscription();

  constructor(private gameBoardService: GameBoardService) { }

  moveResolution(bool: boolean): void {
    if (this.isOwnAreaDisabled) {
      this.isEnemyAreaDisabled = bool;
    }
  }

  ngOnInit(): void {
    const subscription1$ = this.gameBoardService.isEnemyTurn$
      .subscribe((result) => this.moveResolution(result));
    this.subscription.add(subscription1$);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
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
