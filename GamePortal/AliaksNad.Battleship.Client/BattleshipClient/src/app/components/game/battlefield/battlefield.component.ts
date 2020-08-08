import { Component, OnInit, ViewChild, ElementRef, Renderer2, ɵAPP_ID_RANDOM_PROVIDER } from '@angular/core';
import { TableShipDto } from './TableShipDto';

@Component({
  selector: 'app-battlefield',
  templateUrl: './battlefield.component.html',
  styleUrls: ['./battlefield.component.scss']
})
export class BattlefieldComponent implements OnInit {

  @ViewChild('elem') _elem: ElementRef;
  cssOfHit = 'battlefield-cell__hit';
  cssOfMiss = 'battlefield-cell__miss';
  tdClass = 'battlefield-cell battlefield-cell__busy battlefield-cell__done';
  divClass = 'battlefield-cell-content';
  shipClass = 'ship-box ship-box__h ui-draggable ship-box__draggable';
  spanClass = 'z';
  size = 10;
  arr = new Array();
  sign = new Array();
  fleet: TableShipDto[] = [
    { StartCoordinates: { CoordinateX: 2, CoordinateY: 2 }, isHorizontal: true, length: 2 },
    { StartCoordinates: { CoordinateX: 5, CoordinateY: 5 }, isHorizontal: false, length: 2 },
    { StartCoordinates: { CoordinateX: 7, CoordinateY: 3 }, isHorizontal: true, length: 2 },
  ];

  constructor(private renderer: Renderer2) { }
  ngOnInit(): void {
    this.foo(this.size);
  }

  ngAfterViewInit(): void { }

  foo(size): void {
    this.arr = [];
    this.sign = [];
    for (let i = 0; i < size; i++) {
      this.arr.push(i);
      this.sign.push([]);
      for (let g = 0; g < size; g++) {
        this.sign[i].push('');
      }
    }
  }

  push(trIndex, tdIndex): void {
    this.sign[trIndex][tdIndex] = 'battlefield-cell__miss';
  }

  drag(ev): void {
    ev.dataTransfer.setData('ship', ev.target.id);
  }

  allowDrop(ev): void {
    ev.preventDefault();
  }

  drop(ev): void {
    ev.preventDefault();
    const data = ev.dataTransfer.getData('ship');
    ev.target.append(document.getElementById(data));
  }

  ceedFleet(): void {
    // const fleet = this.fleet;
    const fleet = this.generateFleet();
    for (let g = 0; g < fleet.length; g++) {
      this.initializeShip(fleet[g], g);
    }
  }

  initializeShip(shipModel: TableShipDto, id: number): void {
    console.log(shipModel);
    const ship = this.renderer.createElement('div');
    this.renderer.setAttribute(ship, 'id', `${Math.random().toString(36).substring(7)}`);

    this.renderer.addClass(ship, 'ship-box');
    this.renderer.addClass(ship, 'ui-draggable');
    this.renderer.addClass(ship, 'ship-zero');

    this.renderer.setAttribute(ship, 'draggable', 'true');
    this.renderer.listen(ship, 'dragstart', (event) => { this.drag(event); });

    if (shipModel.isHorizontal) {
      this.renderer.setStyle(ship, 'height', `${shipModel.length * 2}em`);
    } else {
      this.renderer.setStyle(ship, 'width', `${shipModel.length * 2}em`);
    }

    const Cell = document.getElementById
      (`${shipModel.StartCoordinates.CoordinateX}*${shipModel.StartCoordinates.CoordinateY}`);
    this.renderer.appendChild(Cell, ship);
  }

  generateFleet(): TableShipDto[] {
    const result = new Array(10);
    for (let index = 0; index < result.length; index++) {
      result[index] = {
        StartCoordinates: {
          CoordinateX: this.randomBtw(0, 9),
          CoordinateY: this.randomBtw(0, 9)
        },
        isHorizontal: Math.random() >= 0.5,
        length: this.randomBtw(1, 4)
      };
    }
    return result;
  }

  randomBtw(min, max): number {
    return Math.floor(Math.random() * (max - min + 1) + min);
  }
}
