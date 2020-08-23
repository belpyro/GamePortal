import { GameService } from './../../../services/game.service';
import { Component, OnInit, Input } from '@angular/core';
import { BattleAreaDto } from '../../../models/BattleAreaDto';

@Component({
  selector: 'app-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.scss']
})
export class GameBoardComponent implements OnInit {

  btlarea: BattleAreaDto[] = [];

  constructor(private gameService: GameService) { }

  ngOnInit(): void {
    this.gameService.getAll()
      .subscribe(data => this.btlarea = data);
  }
}
