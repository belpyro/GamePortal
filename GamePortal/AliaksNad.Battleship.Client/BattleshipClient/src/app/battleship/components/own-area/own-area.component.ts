import { filter } from 'rxjs/operators';
import { BattleAreaDto } from './../../models/battleAreaDto';
import { AreaService } from './../../services/areaService';
import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { GameBoardService } from '../../services/game-board.service';

@Component({
  selector: 'app-own-area',
  templateUrl: './own-area.component.html',
  styleUrls: ['./own-area.component.scss'],
  providers: [AreaService]
})

export class OwnAreaComponent implements OnInit {

  constructor(private gameBoardService: GameBoardService, private areaService: AreaService) { }

  ngOnInit(): void {
    this.gameBoardService.generateFleet$.subscribe(
      () => this.areaService.ceedFleet());

    this.gameBoardService.deleteFleet$.subscribe(
      () => this.areaService.cleanArea());

    this.gameBoardService.loadArea$.subscribe(
      (value) => { this.initializeArea(value); });

    this.areaService.btlarea$.subscribe(
      (value) => { this.gameBoardService.btlarea = value; });

    this.gameBoardService.ownAffectedCell$.subscribe(
      (value) => {
        this.areaService.cssStyleSource.next(value)
      });
  }

  initializeArea(btlArea: BattleAreaDto): void {
    this.areaService.toTableShipDto(btlArea);
  }

}
