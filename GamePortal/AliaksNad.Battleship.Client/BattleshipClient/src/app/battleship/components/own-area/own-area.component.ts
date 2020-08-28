import { BattleAreaDto } from './../../models/battleAreaDto';
import { AreaService } from './../../services/areaService';
import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { GameBoardService } from '../../services/game-board.service';

@Component({
  selector: 'app-own-area',
  templateUrl: './own-area.component.html',
  styleUrls: ['./own-area.component.scss'],
  providers: [AreaService]
})

export class OwnAreaComponent implements OnInit {

  subscription = new Subscription();

  constructor(private gameBoardService: GameBoardService, private areaService: AreaService) { }

  ngOnInit(): void {
    this.subscription.add(this.gameBoardService.generateFleet$
      .subscribe(() => this.areaService.ceedFleet()));

    this.subscription.add(this.gameBoardService.deleteFleet$
      .subscribe(() => this.areaService.cleanArea()));

    this.subscription.add(this.gameBoardService.loadArea$
      .subscribe((value) => { this.initializeArea(value); }));

    this.subscription.add(this.areaService.btlarea$
      .subscribe((value) => { this.gameBoardService.btlarea = value; }));

    this.subscription.add(this.gameBoardService.ownAffectedCell$
      .subscribe((value) => { this.areaService.cssStyleSource.next(value); }));
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  initializeArea(btlArea: BattleAreaDto): void {
    this.areaService.toTableShipDto(btlArea);
  }

}
