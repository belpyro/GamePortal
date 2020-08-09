import { ShipDto } from './../../../models/ShipsDto';
import { CoordinatesDto } from './../../../models/CoordinatesDto';
import { FleetArrPipe } from './fleetArrPpipe';
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
  busyCell = new Array();
  shipSize: number[] = [4, 3];
  // shipSize: number[] = [4, 3, 3, 2, 2, 2, 1, 1, 1, 1];
  fleetExample: TableShipDto[] = [
    { StartCoordinates: { CoordinateX: 3, CoordinateY: 3 }, isHorizontal: true, length: 2 },
    { StartCoordinates: { CoordinateX: 5, CoordinateY: 5 }, isHorizontal: false, length: 2 },
    { StartCoordinates: { CoordinateX: 7, CoordinateY: 3 }, isHorizontal: true, length: 3 },
  ];
  fleet: ShipDto;
  tblFleet: TableShipDto;

  constructor(private renderer: Renderer2) { }
  ngOnInit(): void {
    this.foo(this.size);
  }

  ngAfterViewInit(): void { }

  foo(size): void {
    this.arr = new Array();
    this.sign = new Array();
    for (let i = 0; i < size; i++) {
      this.arr.push(i);
      this.sign.push([]);
      for (let g = 0; g < size; g++) {
        this.sign[i].push('');
      }
    }
  }


  push(trIndex, tdIndex): void {
    this.sign[trIndex][tdIndex] = 'battlefield-cell__hit';
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
    const fleet = this.generateFleet();
    this.fleet = this.toShipDto(fleet[1]);
    for (let g = 0; g < fleet.length; g++) {
      this.initializeShip(fleet[g]);
    }
    console.log('start set busy cell');
    this.busyCell = this.setBusyCell(fleet[1]);
    this.pushBysu(this.busyCell);
  }

  pushBysu(cell: CoordinatesDto[]): void {
    for (let i = 0; i < cell.length; i++) {

      console.log(`Cell = ${cell[i]}`);

      let x = cell[i].CoordinateX;
      let y = cell[i].CoordinateY;

      console.log(`x= ${x}, y= ${y}`);

      this.sign[x][y] = 'battlefield-cell__miss';
    }
  }

  initializeShip(shipModel: TableShipDto): void {
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
    const result = new Array(this.shipSize.length);
    for (let index = 0; index < result.length; index++) {
      result[index] = {
        StartCoordinates: {
          CoordinateX: this.randomBtw(0, 9),
          CoordinateY: this.randomBtw(0, 9)
        },
        isHorizontal: Math.random() >= 0.5,
        length: this.shipSize[index],
      };
    }
    return result;
  }

  randomBtw(min, max): number {
    return Math.floor(Math.random() * (max - min + 1) + min);
  }

  toShipDto(value: TableShipDto): ShipDto {
    const result = new Array(value.length);

    if (value.isHorizontal) {
      for (let i = 0; i < result.length; i++) {
        result[i] = {
          CoordinateX: value.StartCoordinates.CoordinateX + i,
          CoordinateY: value.StartCoordinates.CoordinateY
        };
      }
    } else {
      for (let i = 0; i < result.length; i++) {
        result[i] = {
          CoordinateX: value.StartCoordinates.CoordinateX,
          CoordinateY: value.StartCoordinates.CoordinateY + i
        };
      }
    }
    return { Coordinates: result };
  }

  setBusyCell(value: TableShipDto): CoordinatesDto[] {
    const busyCell = new Array();
    let fromX: number = value.StartCoordinates.CoordinateX;
    fromX -= 1;

    let fromY: number = value.StartCoordinates.CoordinateY;
    fromY -= 1;

    let lengthX: number = value.length;
    let lengthY: number = value.length;

    if (value.isHorizontal) {
      lengthX += 2;
      lengthY = 3;

      for (let i = 0; i < lengthY; i++) {
        for (let g = 0; g < lengthX; g++) {
          busyCell.push({
            CoordinateX: fromX + g,
            CoordinateY: fromY + i
          });
        }
      }
    }
    if (!value.isHorizontal) {
      lengthX = 3;
      lengthY += 2;

      for (let i = 0; i < lengthY; i++) {
        for (let g = 0; g < lengthX; g++) {
          busyCell.push({
            CoordinateX: fromX + g,
            CoordinateY: fromY + i
          });
        }
      }
    }
    return busyCell;
  }
}
