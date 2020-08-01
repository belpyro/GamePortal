import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-battlefield',
  templateUrl: './battlefield.component.html',
  styleUrls: ['./battlefield.component.scss']
})
export class BattlefieldComponent implements OnInit {

  num: number[][] = [[], []];
  visibility = false;
  hit = false;
  ship = false;
  myStyle = 'battlefield-cell__hit';

  constructor() { }

  get getStyle() {
    return this.myStyle.toString;
  }
  ngOnInit(): void {
  }

  toggle(): void {
    this.visibility = !this.visibility;
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
}
