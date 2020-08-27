import { CoordinatesDto } from './../models/coordinatesDto';
import { BattleAreaDto } from './../models/battleAreaDto';
import { AffectedCellDto } from './../models/affectedCell';
import { TableShipDto } from './../models/TableShipDto';
import { ShipDto } from './../models/shipDto';
import { Injectable, Renderer2 } from '@angular/core';
import { Subject } from 'rxjs';
import { EmptyCellDto } from '../models/emptyCellDto';

@Injectable()
export class AreaService {

  busyCell = new Array();
  fleetDto: ShipDto[] = new Array();
  shipSize: number[] = [4, 3, 3, 2, 2, 2, 1, 1, 1, 1];
  tableFleet: TableShipDto[] = new Array();
  sign = new Array();
  dirtyId: string;


  private addShipSource = new Subject<TableShipDto>();
  addShipCalled$ = this.addShipSource.asObservable();

  private delShipSource = new Subject<TableShipDto>();
  delShipCalled$ = this.delShipSource.asObservable();

  cssStyleSource = new Subject<AffectedCellDto>();
  cssStyleCalled$ = this.cssStyleSource.asObservable();

  private btlarea = new Subject<BattleAreaDto>();
  btlarea$ = this.btlarea.asObservable();

  pressedСell = new Subject<CoordinatesDto>();
  pressedСell$ = this.pressedСell.asObservable();

  constructor() { }

  ceedFleet(): void {
    this.cleanArea();
    const shipSize = this.shipSize;
    this.tableFleet = new Array();
    this.fleetDto = new Array();

    for (const size of shipSize) {
      const ship = this.generateShip(size);
      const busyCell = this.setBusyCell(ship);

      this.busyCell = busyCell.concat(this.busyCell);
      // this.pushBusy(busyCell);

      this.fleetDto.push(this.toShipDto(ship));
      this.tableFleet.push(ship);
      this.addShipSource.next(ship);
    }

    this.pushBtlArea();
  }

  pushBtlArea(): void {
    const empCell: EmptyCellDto[] = [{ Coordinates: this.busyCell }];

    this.btlarea.next({
      AreaId: 0,
      Ships: this.fleetDto,
      EmptyCells: empCell,
    });

  }

  cleanArea(): void {
    for (const ship of this.tableFleet) {
      this.delShipSource.next(ship);
    }

    this.busyCell = [];
    this.tableFleet = [];
  }

  drag(ev): void {
    ev.dataTransfer.setData('ship', ev.target.id);
    this.dirtyId = ev.target.id;
    ev.target.style.border = '2px solid green';
    this.resetBysuCell(ev.target.id);
  }

  allowDrop(ev): void {
    const evId = this.dirtyId;
    const fleet = this.tableFleet;

    for (const ship of fleet) {
      if (evId === ship.id) {
        ship.StartCoordinates.CoordinateX = ev.target.getAttribute('Coordinate-x');
        ship.StartCoordinates.CoordinateY = ev.target.getAttribute('Coordinate-y');
        if (!this.validationCheck(ship)) {
          ev.preventDefault();
        }
      }
    }
  }

  resetBysuCell(shipId: string): void {
    this.busyCell = [];

    const tableFleet = this.tableFleet;
    for (const ship of tableFleet) {
      if (shipId !== ship.id) {
        const busyCell = this.setBusyCell(ship);
        this.busyCell = busyCell.concat(this.busyCell);
      }
    }
    // this.pushBusy(this.busyCell);
  }

  pushBusy(area: CoordinatesDto[]): void {
    for (const cell of area) {
      this.cssStyleSource.next({ Coordinates: cell, IsHited: true });
    }
  }

  toShipDtoNEW(value: TableShipDto): ShipDto {
    const result = new Array(value.length);

    let xIncrease = 0;
    let yIncrease = 0;

    if (value.isHorizontal) {
      xIncrease = 1;
    } else {
      yIncrease = 1;
    }

    for (let i = 0; i < result.length; i++) {
      result[i] = {
        CoordinateX: +value.StartCoordinates.CoordinateX + +xIncrease,
        CoordinateY: +value.StartCoordinates.CoordinateY + +yIncrease,
      };
    }

    return { Coordinates: result };
  }

  toShipDto(value: TableShipDto): ShipDto {
    const result = new Array(value.length);


    if (value.isHorizontal) {
      for (let i = 0; i < result.length; i++) {
        result[i] = {
          CoordinateX: +value.StartCoordinates.CoordinateX + +i,
          CoordinateY: +value.StartCoordinates.CoordinateY
        };
      }
    } else {
      for (let i = 0; i < result.length; i++) {
        result[i] = {
          CoordinateX: +value.StartCoordinates.CoordinateX,
          CoordinateY: +value.StartCoordinates.CoordinateY + +i
        };
      }
    }

    return { Coordinates: result };
  }

  toTableShipDto(btlArea: BattleAreaDto): void {
    const value: ShipDto[] = btlArea.Ships;

    let tblShip: TableShipDto;
    this.tableFleet = [];

    for (const ship of value) {
      tblShip = {
        id: this.generateId(),
        StartCoordinates: {
          CoordinateX: ship.Coordinates[0].CoordinateX,
          CoordinateY: ship.Coordinates[0].CoordinateY,
        },
        isHorizontal: this.isHorizontal(ship),
        length: ship.Coordinates.length
      };
      this.addShipSource.next(tblShip);
      this.tableFleet.push(tblShip);
    }
  }

  isHorizontal(ship: ShipDto): boolean {
    if (ship.Coordinates.length > 1 && ship.Coordinates[0].CoordinateX === ship.Coordinates[1].CoordinateX) {
      return false;
    }
    return true;
  }

  generateShip(shipSize: number): TableShipDto {
    let ship: TableShipDto;
    do {
      ship = {
        StartCoordinates: {
          CoordinateX: this.randomBtw(0, 9),
          CoordinateY: this.randomBtw(0, 9)
        },
        isHorizontal: Math.random() >= 0.5,
        length: shipSize,
        id: this.generateId(),
      };
    } while (this.validationCheck(ship));
    return ship;
  }

  generateId(): string {
    return `${Math.random().toString(36).substring(4)}`;
  }

  validationCheck(ship: TableShipDto): boolean {
    return this.areaCheck(ship) || this.employmentСheck(ship);
  }
  areaCheck(ship: TableShipDto): boolean {
    if (ship.isHorizontal) {
      const maxX = +ship.StartCoordinates.CoordinateX + +ship.length;
      if (maxX > 10) {
        return true;
      }
      return false;
    } else {
      const maxY = +ship.StartCoordinates.CoordinateY + +ship.length;
      if (maxY > 10) {
        return true;
      }
      return false;
    }
  }

  employmentСheck(tableShip: TableShipDto): boolean {
    const newShip = this.toShipDto(tableShip);
    const busyArea = this.busyCell as CoordinatesDto[];

    for (const newCell of newShip.Coordinates) {
      for (const busyCell of busyArea) {
        if (newCell.CoordinateX === busyCell.CoordinateX &&
          newCell.CoordinateY === busyCell.CoordinateY) {
          return true;
        }
      }
    }
    return false;
  }

  randomBtw(min, max): number {
    return Math.floor(Math.random() * (max - min + 1) + min);
  }

  setBusyCell(value: TableShipDto): CoordinatesDto[] {
    const busyCell = new Array();

    const fromX: number = (value.StartCoordinates.CoordinateX) - 1;
    const fromY: number = (value.StartCoordinates.CoordinateY) - 1;

    let lengthX: number = (value.length) + 2;
    let lengthY: number = (value.length) + 2;

    if (value.isHorizontal) {
      lengthY = 3;
    } else {
      lengthX = 3;
    }

    for (let i = 0; i < lengthY; i++) {
      for (let g = 0; g < lengthX; g++) {
        busyCell.push({
          CoordinateX: this.getBeteen(fromX + g),
          CoordinateY: this.getBeteen(fromY + i)
        });
      }
    }

    return busyCell;
  }

  getBeteen(int: number, min = 0, max = 9): number {
    if (int < min) {
      int = min;
    }
    if (int > max) {
      int = max;
    }
    return int;
  }



}
