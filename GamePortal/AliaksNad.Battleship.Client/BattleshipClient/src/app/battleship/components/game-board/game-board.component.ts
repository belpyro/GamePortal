import { GameService } from './../../services/game.service';
import { BattleAreaDto } from './../../models/BattleAreaDto';
import { Component, OnInit, Input } from '@angular/core';

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
