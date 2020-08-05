import { Component, OnInit } from '@angular/core';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-battlefield',
  templateUrl: './battlefield.component.html',
  styleUrls: ['./battlefield.component.scss']
})

export class BattlefieldComponent implements OnInit {

  myStyle = 'battlefield-cell__hit';
  red = 'battlefield-cell__miss';
  size = 10;
  arr = new Array();
  sign = new Array();
  artists = [
    'Artist I - Davido',
    'Artist II - Wizkid',
    'Artist III - Burna Boy'
  ];
  alteArtists = [
    'Artist 1 — Odunsi',
    'Artist 2 — Nonso',
    'Artist 3 — Wavy the creator'
  ];

  constructor() {
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

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer !== event.container) {
      transferArrayItem(event.previousContainer.data, event.container.data,
        event.previousIndex, event.currentIndex)
    } else {
      moveItemInArray(this.artists, event.previousIndex, event.currentIndex);
    }
  }

}
