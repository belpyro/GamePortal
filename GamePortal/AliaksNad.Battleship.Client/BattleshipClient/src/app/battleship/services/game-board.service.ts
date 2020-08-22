import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable()
export class GameBoardService {

  private generate = new Subject<any>();
  generateFleet$ = this.generate.asObservable();
  private delete = new Subject<any>();
  deleteFleet$ = this.delete.asObservable();

  constructor(private gameService: BattleshipGameService) { }

  ngOnInit(): void {
    this.gameService.battleshipGameGetAll()
      .subscribe(data => this.btlarea = data);
  }

  generateFleet(): void {
    this.generate.next();
  }

  deleteFleet(): void {
    this.delete.next();
  }
}
