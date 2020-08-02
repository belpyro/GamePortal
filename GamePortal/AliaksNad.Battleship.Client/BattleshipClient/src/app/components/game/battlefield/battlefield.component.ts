import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-battlefield',
  templateUrl: './battlefield.component.html',
  styleUrls: ['./battlefield.component.scss']
})

export class BattlefieldComponent implements OnInit {

  tableColumns = new Array(10);
  visibility = false;
  hit = false;
  ship = false;
  myStyle = 'battlefield-cell__hit';
  red = 'battlefield-cell__miss';
  size = 10;
  arr = new Array();
  sign = new Array();
  name: number;

  constructor() {
  }

  get getStyle() {
    return this.myStyle.toString;
  }
  ngOnInit(): void {
    this.foo(this.size);
    console.log('Component "ngOn" initialised!');
  }

  foo(size): void {
    console.log(`Component "foo start" ${size} initialised!`);
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
    this.sign[trIndex][tdIndex] = 'battlefield-cell__hit';
  }

  toggle(): void {
    this.visibility = !this.visibility;

    if (this.red == 'battlefield-cell__hit') {
      this.red = 'battlefield-cell__miss';
    } else {
      this.red = 'battlefield-cell__hit';
    }
  }

  checkHit(): void {
    this.hit = !this.hit;
  }

  checkShip(): void {
    this.ship = !this.ship;
  }

  calculateClasses() {
    let x = this.ship;
    let y = this.hit;
    if (y) {
      if (x) {
        return { 'battlefield-cell__hit': true };
      }
      return { 'battlefield-cell__miss': true };
    }
  }

  getCss() {
    let x = this.ship;
    if (x) {
      return 'battlefield-cell__hit';
    }
    return 'battlefield-cell__miss';
  }

  details(elem: HTMLElement) {
    elem.className = this.getCss();
  }
}
