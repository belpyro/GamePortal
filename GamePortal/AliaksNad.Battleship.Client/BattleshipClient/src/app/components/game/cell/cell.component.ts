import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-cell',
  templateUrl: './cell.component.html',
  styleUrls: ['./cell.component.scss']
})
export class CellComponent implements OnInit {

  @Input() id: string;
  @Output() ondrop: any;
  @Output() allow: any;

  constructor() { }

  ngOnInit(): void {
  }

  drop(increased: any): void {
    this.ondrop.emit(increased);
  }

  allowDrop(increased: any): void {
    this.allow.emit(increased);
  }
}
