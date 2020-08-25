import { AffectedCellDto } from './../models/affectedCell';
import { TargetDto } from './../models/targetDto';
import { CoordinatesDto } from './../models/coordinatesDto';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { BattleshipGameService } from './battleshipGame.service';
import { BattleAreaDto } from '../models/battleAreaDto';

@Injectable()
export class GameBoardService {

  private generate = new Subject<any>();
  generateFleet$ = this.generate.asObservable();

  private delete = new Subject<any>();
  deleteFleet$ = this.delete.asObservable();

  private shipDto = new Subject<BattleAreaDto>();
  shipDto$ = this.shipDto.asObservable();

  private affectedCell = new Subject<AffectedCellDto>();
  affectedCell$ = this.affectedCell.asObservable();

  btlarea: BattleAreaDto;
  btlAreaId = 19;
  // btlAreaId = 1;

  constructor(private gameService: BattleshipGameService) {
    this.gameService.battleshipGameGetById(this.btlAreaId)
      .subscribe((data) => { this.shipDto.next(data); });
  }

  generateFleet(): void {
    this.generate.next();
  }

  deleteFleet(): void {
    this.delete.next();
  }

  uploadArea(): void {
    const btlarea = this.btlarea;

    this.gameService.battleshipGameAdd(btlarea)
      .subscribe((data) => { console.log(data); });
  }

  checkHit(coordinates: CoordinatesDto): void {
    const target: TargetDto = { EnemyBattleAreaId: this.btlAreaId, Coordinates: coordinates };

    this.gameService.battleshipGameCheckHit(target)
      .subscribe((value) => { this.pushAffectedCell(coordinates, value); });
  }

  pushAffectedCell(coordinates: CoordinatesDto, bool: boolean): void {
    this.affectedCell.next({
      Coordinates: coordinates,
      IsHited: bool,
    });
  }

}
