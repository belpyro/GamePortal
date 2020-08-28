import { Component, OnInit } from '@angular/core';
import { AreaService } from '../../services/areaService';
import { GameBoardService } from '../../services/game-board.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-enemy-area',
  templateUrl: './enemy-area.component.html',
  styleUrls: ['./enemy-area.component.scss'],
  providers: [AreaService]
})
export class EnemyAreaComponent implements OnInit {

  subscription = new Subscription();

  constructor(private gameBoardService: GameBoardService, private areaService: AreaService) { }

  ngOnInit(): void {
    this.subscription.add(this.areaService.pressedСell$
      .subscribe((value) => this.gameBoardService.checkHit(value)));

    this.subscription.add(this.gameBoardService.enemyAffectedCell$
      .subscribe((value) => this.areaService.cssStyleSource.next(value)));
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
