import { Component, OnInit } from '@angular/core';
import { AreaService } from '../../services/areaService';
import { GameBoardService } from '../../services/game-board.service';

@Component({
  selector: 'app-enemy-area',
  templateUrl: './enemy-area.component.html',
  styleUrls: ['./enemy-area.component.scss'],
  providers: [AreaService]
})
export class EnemyAreaComponent implements OnInit {

  constructor(private gameBoardService: GameBoardService, private areaService: AreaService) { }

  ngOnInit(): void {
    this.areaService.pressedСell$.subscribe((value) => this.gameBoardService.checkHit(value));

    this.gameBoardService.enemyAffectedCell$.subscribe(
      (value) => this.areaService.cssStyleSource.next(value));
  }
}
