import { TextsetService } from './../../../services/textset.service';
import { Component, OnInit } from '@angular/core';
import { TextSetDto } from 'src/app/models/textsetDto';
import { TextSetDtomin } from 'src/app/models/textsetDtomin';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-textblock',
  templateUrl: './textblock.component.html',
  styleUrls: ['./textblock.component.scss']
})
export class TextblockComponent implements OnInit {

isTextSelected = false;
addNewText = false;
selectedtext: TextSetDto;
alltextset: TextSetDtomin[];
easytextset: TextSetDtomin[];
middletextset: TextSetDtomin[];
hardtextset: TextSetDtomin[];
textGroup: FormGroup;

  constructor(private toastr: ToastrService, public textsetService: TextsetService, private fb: FormBuilder) {
    this.textGroup = this.fb.group({
      textname: ['' , [Validators.required, Validators.minLength(5)]],
      textarea: ['', [Validators.required, Validators.minLength(5)]],
      textradio: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.textsetService.getAllTextSet().subscribe(data => this.alltextset = data);
    this.textsetService.getTextSetByLevel(0).subscribe(data => this.easytextset = data);
    this.textsetService.getTextSetByLevel(1).subscribe(data => this.middletextset = data);
    this.textsetService.getTextSetByLevel(2).subscribe(data => this.hardtextset = data);
  }

  SelectText(id: number){
    this.textsetService.getTextSetById(id).subscribe(data => this.selectedtext = data);
    this.isTextSelected = true;
    this.addNewText = false;
  }

  addNewTextSet(){
    this.isTextSelected = false;
    this.addNewText = true;
  }

  sendTextToServer(){
  const newText: TextSetDto  = {
  Name : this.textGroup.value.textname,
  Id : 0,
  TextForTyping : this.textGroup.value.textarea,
  LevelOfText : this.textGroup.value.textradio
  };
  this.textsetService.addTextSetToDb(newText).subscribe(  (res: any ) => {
    console.log(res);
    if (res != null)
    {
      this.toastr.success(`text ${newText.Name} successfully added to collection `);
    }
  },
  err => {
    if ( err.status === 400 )
    {
      this.toastr.error(err.error.Message);
    }
  });
  }
}
