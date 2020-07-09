import { TextsetService } from './../../../services/textset.service';
import { Component, OnInit } from '@angular/core';
import { TextSetDto } from 'src/app/models/textsetDto';

@Component({
  selector: 'app-textblock',
  templateUrl: './textblock.component.html',
  styleUrls: ['./textblock.component.scss']
})
export class TextblockComponent implements OnInit {

  textset: TextSetDto;
  constructor(public textsetService: TextsetService) { }

  ngOnInit(): void {
    this.textsetService.getTextSetById(22).subscribe(data => this.textset = data);
  }

}
