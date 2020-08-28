import { TargetResultDto } from './../models/targetResultDto';
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

  private enemyAffectedCell = new Subject<AffectedCellDto>();
  enemyAffectedCell$ = this.enemyAffectedCell.asObservable();

  private ownAffectedCell = new Subject<AffectedCellDto>();
  ownAffectedCell$ = this.ownAffectedCell.asObservable();

  private isEnemyTurn = new Subject<boolean>();
  isEnemyTurn$ = this.isEnemyTurn.asObservable();

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
        this.connection.listenFor<TargetResultDto>('TargetResult')
          .subscribe((result) => { this.implementTargetResult(result); });
        this.connection.listenFor<number>('SendAreaId')
          .subscribe((enemyAreaId) => {
            this.enemyBtlAreaId = enemyAreaId;
            this.isEnemyTurn.next(false);
            this.ntf.success('Game strted!');
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
        this.ownBtlAreaId = model.AreaId;
        this.pushMessage(+model.AreaId);
      });
  }

  checkHit(coordinates: CoordinatesDto): void {
    const target: TargetDto = { EnemyBattleAreaId: this.enemyBtlAreaId, Coordinates: coordinates };

    this.gameService.battleshipGameCheckHit(target)
      .subscribe((value) => {
        this.pushAffectedCell(coordinates, value);
        this.isEnemyTurn.next(!value);
      });
  }

  pushAffectedCell(coordinates: CoordinatesDto, bool: boolean): void {
    this.enemyAffectedCell.next({
      Coordinates: coordinates,
      IsHited: bool,
    });
  }

  implementTargetResult(result: TargetResultDto): void {
    if (result.Target.EnemyBattleAreaId === this.ownBtlAreaId) {
      this.isEnemyTurn.next(result.Result);
      this.ownAffectedCell.next({
        Coordinates: result.Target.Coordinates,
        IsHited: result.Result,
      });
    }
  }

  private pushMessage(id: number): void {
    if (this.connection) {
      this.connection.invoke('SendAreaId', id);
    }
  }

}
