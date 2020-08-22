import { Component, OnInit, SkipSelf, Self, Input, ViewChild, ElementRef, Renderer2 } from '@angular/core';

@Component({
  selector: 'app-area',
  templateUrl: './area.component.html',
  styleUrls: [
    '../battlefield/battlefield.component.scss',
    './area.component.scss',
  ],
})
export class AreaComponent implements OnInit {

  size = 10;
  arr = new Array();
  sign = new Array();

  constructor(private renderer: Renderer2) { }

  ngOnInit(): void {
    this.initializeTable(this.size);

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
}
