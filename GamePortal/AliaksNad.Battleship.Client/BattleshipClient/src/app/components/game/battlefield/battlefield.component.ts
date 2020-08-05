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
  tableData = [
    [{ id: 1, data: 'test1' }, { id: 2, data: 'test1' }, { id: 3, data: 'test1' }],
    [{ id: 4, data: 'test1' }, { id: 5, data: 'test1' }, { id: 6, data: 'test1' }],
    [{ id: 7, data: 'test1' }, { id: 8, data: 'test1' }, { id: 9, data: 'test1' }],
  ];
  todo = [
    'Get to work',
    'Pick up groceries',
    'Go home',
    'Fall asleep'
  ];

  done = [
    'Get up',
    'Brush teeth',
    'Take a shower',
    'Check e-mail',
    'Walk dog'
  ];
  fleet = [
    {
      item: 'Fleet #1',
      children: [
        { item: 'Admiral Flankson' },
        { item: 'pvt. centeras' },
        { item: 'pvt. leeft' },
        { item: 'pvt. rijks' }
      ]
    },
    {
      item: 'Fleet #2',
      children: [
        { item: 'Admiral Parkour' },
        { item: 'pvt. jumph' },
        { item: 'pvt. landts' },
        { item: 'pvt. drobs' }
      ]
    },
    {
      item: 'Fleet #3',
      children: [
        { item: 'Admiral Tombs' },
        { item: 'pvt. zomboss' },
        { item: 'pvt. digger' },
        { item: 'pvt. skaari' }
      ]
    }
  ]

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

  drop(event: CdkDragDrop<{}[]>) {
    if (event.previousContainer == event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(event.previousContainer.data, event.container.data, event.previousIndex, event.currentIndex);
    }

  }

}
