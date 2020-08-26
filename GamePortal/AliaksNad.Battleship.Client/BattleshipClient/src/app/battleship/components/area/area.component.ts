import { Component, OnInit, SkipSelf, Self, Input, ViewChild, ElementRef, Renderer2 } from '@angular/core';
import { TableShipDto } from '../../models/TableShipDto';
import { AreaService } from '../../services/areaService';
import { CoordinatesDto } from '../../models/coordinatesDto';
import { AffectedCellDto } from '../../models/affectedCell';
import { style } from '@angular/animations';

@Component({
  selector: 'app-area',
  templateUrl: './area.component.html',
  styleUrls: [
    '../battlefield/battlefield.component.scss',
    './area.component.scss',
  ],
})
export class AreaComponent implements OnInit {

  @Input() title = 'title';
  cssHit = 'battlefield-cell__hit';
  cssMiss = 'battlefield-cell__miss';
  size = 10;
  arr = new Array();
  sign = new Array();

  constructor(private renderer: Renderer2, private areaService: AreaService) { }

  ngOnInit(): void {
    this.initializeTable(this.size);

    this.areaService.delShipCalled$.subscribe(
      (model) => {
        this.deleteShip(model);
      }
    );

    this.areaService.addShipCalled$.subscribe(
      (model) => {
        this.initializeShip(model);
      }
    );

    this.areaService.cssStyleCalled$.subscribe(
      (model) => {
        this.markCell(model);
      }
    );
  }

  initializeTable(size: number): void {
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

  initializeShip(shipModel: TableShipDto): void {
    const ship = this.renderer.createElement('div');
    this.renderer.setAttribute(ship, 'id', shipModel.id);

    this.renderer.addClass(ship, 'ship-box');
    this.renderer.addClass(ship, 'ui-draggable');
    this.renderer.addClass(ship, 'ship-zero');

    this.renderer.setAttribute(ship, 'draggable', 'true');
    this.renderer.listen(ship, 'dragstart', (event) => { this.areaService.drag(event); });

    if (shipModel.isHorizontal) {
      this.renderer.setStyle(ship, 'width', `${shipModel.length * 2}em`);
    } else {
      this.renderer.setStyle(ship, 'height', `${shipModel.length * 2}em`);
    }

    const cell = this.getCellById(shipModel.StartCoordinates);
    this.renderer.appendChild(cell, ship);
  }

  deleteShip(shipModel: TableShipDto): void {
    const cell = this.getCellById(shipModel.StartCoordinates);
    const ship = document.getElementById(shipModel.id);
    this.renderer.removeChild(cell, ship);
    this.initializeTable(this.size);
  }

  getCellById(model: CoordinatesDto): HTMLElement {
    return document.getElementById(`${this.title}_${model.CoordinateX}*${model.CoordinateY}`);
  }

  markCell(model: AffectedCellDto): void {
    let cssStyle: string = this.cssMiss;

    if (model.IsHited) {
      cssStyle = this.cssHit;
    }

    this.sign[model.Coordinates.CoordinateX][model.Coordinates.CoordinateY] = cssStyle;
  }

  drop(ev): void {
    ev.preventDefault();
    const shipId = ev.dataTransfer.getData('ship');
    const ship = document.getElementById(shipId);
    this.renderer.setStyle(ship, 'border', ``);
    ev.target.append(ship);
  }

  push(tdIndex, trIndex): void {
    this.areaService.pressedСell.next({ CoordinateX: tdIndex, CoordinateY: trIndex });
  }

  allowDrop(ev): void {
    this.areaService.allowDrop(ev);
  }

}
