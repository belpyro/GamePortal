import { Component, OnInit, SkipSelf, Self, Input, ViewChild, ElementRef, Renderer2 } from '@angular/core';
import { GameService } from '../../services/game.service';
import { BattleAreaDto } from '../../models/BattleAreaDto';

@Component({
  selector: 'app-area',
  templateUrl: './area.component.html',
  styleUrls: ['./area.component.scss'],
  providers: [GameService]
})
export class AreaComponent implements OnInit {

  @Input() battleArea: BattleAreaDto;
  @ViewChild('table') table: ElementRef;
  size = 10;

  constructor(private renderer: Renderer2) { }

  ngOnInit(): void { }

  tableInitialize(): void {
    const size = this.size;
    for (let i = 0; i < size; i++) {
      for (let j = 0; j < size; j++) {
        // const cell = this.renderer.createComponent('div');

      }
    }
  }

}
