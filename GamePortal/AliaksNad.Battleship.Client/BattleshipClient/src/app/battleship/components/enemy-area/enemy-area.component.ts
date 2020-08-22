import { Component, OnInit } from '@angular/core';
import { AreaService } from '../../services/areaService';

@Component({
  selector: 'app-enemy-area',
  templateUrl: './enemy-area.component.html',
  styleUrls: ['./enemy-area.component.scss'],
  providers: [AreaService]
})
export class EnemyAreaComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
