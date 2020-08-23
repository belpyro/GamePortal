import { ShipDto } from './../models/shipDto';
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

  btlarea: BattleAreaDto;

  constructor(private gameService: BattleshipGameService) {
    this.gameService.battleshipGameGetById(1)
      .subscribe((data) => {
        this.btlarea = data;
        this.shipDto.next(this.btlarea);
      });

  }

  generateFleet(): void {
    this.generate.next();
  }

  deleteFleet(): void {
    this.delete.next();
  }

}
