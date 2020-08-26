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
  isDisabled = true;
  private connection: ISignalRConnection;

  constructor(
    private gameBoardService: GameBoardService,
    private gameService: BattleshipGameService,
    private hub: SignalR,
    private ntf: NotificationsService
  ) {
    this.hub.connect()
      .then((c) => {
        this.connection = c;
        this.connection.listenFor<string>('SendMessage')
          .subscribe(msg => this.ntf.warn('Message', msg));
        this.connection.listenFor<string>('GameStart')
          .subscribe(msg => this.ntf.warn('Message', msg));
      })
      .catch(reason =>
        console.error(`Cannot connect to hub sample ${reason}`));
  }

  ngOnInit(): void {
    this.gameService.battleshipGameGetAll()
      .subscribe((data) => { this.btlarea = data; });
  }

  generateFleet(): void {
    this.gameBoardService.generateFleet();
  }

  deleteFleet(): void {
    this.gameBoardService.deleteFleet();
  }

  uploadFleet(): void {
    this.gameBoardService.uploadArea();
  }

  doSmt(): void {
    this.isDisabled = !this.isDisabled;
  }
}
