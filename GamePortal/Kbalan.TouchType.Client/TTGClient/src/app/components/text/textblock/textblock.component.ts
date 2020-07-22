import { TextsetService } from './../../../services/textset.service';
import { Component, OnInit } from '@angular/core';
import { TextSetDto } from 'src/app/models/textsetDto';
import { TextSetDtomin } from 'src/app/models/textsetDtomin';


@Component({
  selector: 'app-textblock',
  templateUrl: './textblock.component.html',
  styleUrls: ['./textblock.component.scss']
})
export class TextblockComponent implements OnInit {

selectedtext: TextSetDto;
alltextset: TextSetDtomin[];
easytextset: TextSetDtomin[];
middletextset: TextSetDtomin[];
hardtextset: TextSetDtomin[];

  constructor(public textsetService: TextsetService) { }

  ngOnInit(): void {
    this.textsetService.getAllTextSet().subscribe(data => this.alltextset = data);
    this.textsetService.getTextSetByLevel(0).subscribe(data => this.easytextset = data);
    this.textsetService.getTextSetByLevel(1).subscribe(data => this.middletextset = data);
    this.textsetService.getTextSetByLevel(2).subscribe(data => this.hardtextset = data);
  }

  SelectText(id: number){
    this.textsetService.getTextSetById(id).subscribe(data => this.selectedtext = data);
  }

}
