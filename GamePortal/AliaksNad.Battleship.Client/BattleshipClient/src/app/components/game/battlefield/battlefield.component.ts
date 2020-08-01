import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-battlefield',
  templateUrl: './battlefield.component.html',
  styleUrls: ['./battlefield.component.scss']
})
export class BattlefieldComponent implements OnInit {

  num: number[][] = [[], []];

  constructor() { }

  ngOnInit(): void {
  }

  initialMap() {

    let createMap = () => {
      let a = [];
      for (let i = 0; i < 10; i++) {
        for (let e = 0; e < 10; e++) {
          a.push({ x: i, y: e, hit: Math.random() >= 0.5, hasBoat: Math.random() >= 0.5 });
        }
      }
      return a;
    }

  }

  visibility: boolean = false
  // переключаем переменную
  toggle() {
    this.visibility = !this.visibility
  }

}
