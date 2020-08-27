import { AffectedCellDto } from './../models/affectedCell';
import { TargetDto } from './../models/targetDto';
import { CoordinatesDto } from './../models/coordinatesDto';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { BattleshipGameService } from './battleshipGame.service';
import { BattleAreaDto } from '../models/battleAreaDto';
import { SignalR, ISignalRConnection } from 'ng2-signalr';
import { NotificationsService } from 'angular2-notifications';

@Injectable()
export class GameBoardService {

  private generate = new Subject<any>();
  generateFleet$ = this.generate.asObservable();

  private delete = new Subject<any>();
  deleteFleet$ = this.delete.asObservable();

  private loadArea = new Subject<BattleAreaDto>();
  loadArea$ = this.loadArea.asObservable();

  private affectedCell = new Subject<AffectedCellDto>();
  affectedCell$ = this.affectedCell.asObservable();

  private connection: ISignalRConnection;

  btlarea: BattleAreaDto;
  ownBtlAreaId: number;
  enemyBtlAreaId: number;

  constructor(
    private gameService: BattleshipGameService,
    private hub: SignalR,
    private ntf: NotificationsService
  ) {
    this.hub.connect()
      .then((c) => {
        this.connection = c;
        this.connection.listenFor<number>('GameStart')
          .subscribe((areaId) => {
            this.ntf.info(`Battlearea id = ${areaId}`);
            this.enemyBtlAreaId = areaId;
            console.log(`game start id = ${areaId}`);
            this.loadAreaById(+areaId);
          });
        this.connection.listenFor<number>('SendAreaId')
          .subscribe((enemyAreaId) => {
            this.enemyBtlAreaId = enemyAreaId;
            console.log(`enemy id = ${enemyAreaId}`);
          });
      })
      .catch(reason =>
        console.error(`Cannot connect to hub sample ${reason}`));
  }

  generateFleet(): void {
    this.generate.next();
  }

  loadAreaById(id: number) {
    this.deleteFleet();

    this.gameService.battleshipGameGetById(id)
      .subscribe((data) => { this.loadArea.next(data); });
  }

  deleteFleet(): void {
    this.delete.next();
  }

  uploadArea(): void {
    this.gameService.battleshipGameAdd(this.btlarea)
      .subscribe((model) => {
        console.log(`ownAreaaId = ${+model.AreaId}`);
        this.pushMessage(+model.AreaId);
      });
  }

  checkHit(coordinates: CoordinatesDto): void {
    const target: TargetDto = { EnemyBattleAreaId: this.enemyBtlAreaId, Coordinates: coordinates };

    this.gameService.battleshipGameCheckHit(target)
      .subscribe((value) => { this.pushAffectedCell(coordinates, value); });
  }

  pushAffectedCell(coordinates: CoordinatesDto, bool: boolean): void {
    this.affectedCell.next({
      Coordinates: coordinates,
      IsHited: bool,
    });
  }

  private pushMessage(id: number): void {
    if (this.connection) {
      this.connection.invoke('SendAreaId', id);
    }
  }

}
