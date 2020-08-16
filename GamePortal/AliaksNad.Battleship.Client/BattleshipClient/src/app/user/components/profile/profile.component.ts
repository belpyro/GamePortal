import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  profileGroup: FormGroup;
  addressArray: FormArray;

  constructor(private fb: FormBuilder) {
    this.addressArray = this.fb.array([]);
    this.profileGroup = this.fb.group({
      email: [],
      pass: [],
      address: this.addressArray,
      // fb.group({
      //   address1: [''],
      //   address2: [''],
      //   city: [''],
      //   state: [''],
      //   zip: [''],
      // }),
    });
  }

  ngOnInit(): void {
  }

  addAddress() {
    this.addressArray.push(this.fb.control(''));
  }

}
