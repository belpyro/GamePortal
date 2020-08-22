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
  }

}
