import { GameBoardService } from './../../services/game-board.service';
import { AreaService } from './../../services/areaService';
import { BattleshipGameService } from './../../services/battleshipGame.service';
import { Component, OnInit, Input } from '@angular/core';
import { BattleAreaDto } from '../../models/BattleAreaDto';

@Component({
  selector: 'app-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.scss']
})
export class GameBoardComponent implements OnInit {

  constructor(private gameBoardService: GameBoardService) { }

  ngOnInit(): void {}

  generateFleet(): void {
    this.gameBoardService.generateFleet();
  }

  deleteFleet(): void {
    this.gameBoardService.deleteFleet();
  }
}