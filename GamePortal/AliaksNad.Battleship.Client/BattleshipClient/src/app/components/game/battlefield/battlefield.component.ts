import { Component, OnInit, Inject, ViewChild, ElementRef, Renderer2 } from '@angular/core';
import { DOCUMENT } from '@angular/common';

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
    console.log(ev)
    ev.dataTransfer.setData('ship', ev.target.id);
  }

  allowDrop(ev): void {
    console.log(ev)
    ev.preventDefault();
  }

  drop(ev): void {
    ev.preventDefault();
    const data = ev.dataTransfer.getData('ship');
    ev.target.append(document.getElementById(data));
    console.log(ev)
  }

  ibitializeShip(): void {
    const Cell = document.getElementById('3*3');

    const ship = this.renderer.createElement('div');

    this.renderer.addClass(ship, 'ship-box');
    this.renderer.addClass(ship, 'ship-box__h');
    this.renderer.addClass(ship, 'ui-draggable');
    this.renderer.addClass(ship, 'ship-box__draggable');
    this.renderer.setStyle(ship, 'width', '4em');
    this.renderer.setStyle(ship, 'height', '2em');
    this.renderer.setStyle(ship, 'padding-right', '0px');
    this.renderer.setStyle(ship, 'padding-bottom', '0px');
    this.renderer.setStyle(ship, 'left', '0px');
    this.renderer.setStyle(ship, 'top', '0px');
    this.renderer.setStyle(ship, 'margin', '0px');
    this.renderer.setAttribute(ship, 'draggable', 'true');
    this.renderer.setAttribute(ship, 'id', 'drag9');
    this.renderer.listen(ship, 'dragstart', (event) => { this.drag(event); });

    this.renderer.appendChild(Cell, ship);
  }
}
