import { BattleAreaDto } from './../../../models/BattleAreaDto';
import { GameService } from './../../../services/game.service';
import { Component, OnInit, SkipSelf, Self } from '@angular/core';

@Component({
  selector: 'app-area',
  templateUrl: './area.component.html',
  styleUrls: ['./area.component.scss'],
  providers: [GameService]
})
export class AreaComponent implements OnInit {

  battleArea: BattleAreaDto;

  constructor(public gameService: GameService) { }

  ngOnInit(): void {
    this.gameService.getBattleAreaById(1).subscribe(data => this.battleArea = data);
  }

}
