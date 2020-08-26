import { TextsetService } from '../../services/textset.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { TextSetDto } from 'src/app/text/models/textsetDto';
import { TextSetDtomin } from 'src/app/text/models/textsetDtomin';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import Swal from 'sweetalert2';
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-textblock',
  templateUrl: './textblock.component.html',
  styleUrls: ['./textblock.component.scss']
})
export class TextblockComponent implements OnInit {

isTextSelected = false;
addNewText = false;
editTextbuttonEnable = false;
selectedtext: TextSetDto;
alltextset: TextSetDtomin[];
easytextset: TextSetDtomin[];
middletextset: TextSetDtomin[];
hardtextset: TextSetDtomin[];
textGroup: FormGroup;
  constructor(private toastr: ToastrService, public textsetService: TextsetService, private fb: FormBuilder) {
    this.textGroup = this.fb.group({
      textname: [ '', [Validators.required, Validators.minLength(5)]],
      textarea: ['', [Validators.required, Validators.minLength(5)]],
      textradio: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.initTextSet();
  }

  initTextSet(){
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
    this.editTextbuttonEnable = false;
    this.textGroup.setValue({
      textname: '',
      textarea: '',
      textradio: 'Easy' });
    this.isTextSelected = false;
    this.addNewText = true;
  }

  deleteText(id: number){
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to revert this!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((willDelete) => {
    if (willDelete.isConfirmed) {
  ( this.textsetService.deleteTextfromDb(id).subscribe(  (res: any ) => {
    if (res != null)
    {
      this.toastr.success(res);
      this.initTextSet();
      this.isTextSelected = false;
    }
  },
  err => {
    if ( err != null )
    {
      this.toastr.error(err.error.Message);
    }
  }));
}
});
}

editText()
{
  this.editTextbuttonEnable = true;
  console.log(this.selectedtext.LevelOfText);
  this.textGroup.setValue({
    textname: this.selectedtext.Name,
    textarea: this.selectedtext.TextForTyping,
    textradio: this.selectedtext.LevelOfText });
  this.isTextSelected = false;
  this.addNewText = true;
}

sendEditedTexttoServer()
{
  const newText: TextSetDto  = {
    Name : this.textGroup.value.textname,
    Id : this.selectedtext.Id,
    TextForTyping : this.textGroup.value.textarea,
    LevelOfText : this.textGroup.value.textradio
    };
  this.textsetService.updateTextfromDb(newText).subscribe(  (res: any ) => {
      if (res != null)
      {
        this.toastr.success(`text ${newText.Name} successfully added to collection `);
        this.initTextSet();
        this.textGroup.reset();
      }
    },
    err => {
      if ( err.status === 400 )
      {
        this.toastr.error(err.error.Message);
      }
    });

}

sendTextToServer(){
  const newText: TextSetDto  = {
  Name : this.textGroup.value.textname,
  Id : 0,
  TextForTyping : this.textGroup.value.textarea,
  LevelOfText : this.textGroup.value.textradio
  };
  this.textsetService.addTextSetToDb(newText).subscribe(  (res: any ) => {
    if (res != null)
    {
      this.toastr.success(`text ${newText.Name} successfully added to collection `);
      this.initTextSet();
      this.textGroup.reset();
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
