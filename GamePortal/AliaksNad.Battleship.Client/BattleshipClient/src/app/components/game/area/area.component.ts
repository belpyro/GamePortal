import { NotificationService } from './../../../services/notification.service';
import { BattleAreaDto } from './../../../models/BattleAreaDto';
import { GameService } from './../../../services/game.service';
import { Component, OnInit, SkipSelf, Self, Input } from '@angular/core';

@Component({
  selector: 'app-area',
  templateUrl: './area.component.html',
  styleUrls: ['./area.component.scss'],
  providers: [GameService]
})
export class AreaComponent implements OnInit {

  @Input() battleArea: BattleAreaDto;

  constructor(public gameService: GameService, private ntf: NotificationService) { }

  ngOnInit(): void {}

}
