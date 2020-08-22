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

  btlarea: BattleAreaDto[] = [];

  constructor(private gameService: BattleshipGameService, private areaService: AreaService) { }

  ngOnInit(): void {
    this.gameService.battleshipGameGetAll()
      .subscribe(data => this.btlarea = data);
  }

  generateFleet(): void {
    this.areaService.ceedFleet();
  }

  deleteFleet(): void {
    this.areaService.cleanArea();
  }
}
