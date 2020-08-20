import { TextSetDto } from './../../../text/models/textsetDto';
import { Component, OnInit, ViewChild } from '@angular/core';
import { HomeService } from 'src/app/home/services/home.service';
import { UserProfileDto } from 'src/app/home/models/UserProfileDto';
import { SgameService } from '../../services/sgame.service';
import { ToastrService } from 'ngx-toastr';
import {  map } from 'rxjs/operators';
import { fromEvent } from 'rxjs';
import { GoogleChartComponent } from 'angular-google-charts';
@Component({
  selector: 'app-sgame',
  templateUrl: './sgame.component.html',
  styleUrls: ['./sgame.component.scss']
})
export class SgameComponent implements OnInit {
  timeStart = 0;
  speed = 0;
  typedTextForSec = 0;
  interval;
  textToCheck = '';
  awaitedText = '';
  correctText = '';
  userProfile?: UserProfileDto;
  newGame?: TextSetDto;
  isGameInProcess = false;
  clicks = fromEvent(document, 'keydown');
  result = this.clicks.pipe( map((e: KeyboardEvent) => e.key));
  @ViewChild('googlechart')
  googlechart: GoogleChartComponent;
  chart = {
    type: 'Gauge',
    data: [
      ['WPM', this.speed],
    ],
    options: {
      width: 400,
      height: 400,
      greenFrom: 0,
      greenTo: 75,
      redFrom: 120,
      redTo: 150,
      yellowFrom: 75,
      yellowTo: 120,
      minorTicks: 1
    }
  };
  constructor(private homeservice: HomeService, private gameservice: SgameService, private toastr: ToastrService) {

     }

  ngOnInit(): void {
    this.initUser();

  }

  initUser(){
     this.homeservice.getUser().subscribe(res => {
      this.userProfile = res; this.startnewgame(); }
     ); }

  async startnewgame(){
    this.startTimer();
    this.newGame = await this.gameservice.getnewgame(this.userProfile.Id);
    this.isGameInProcess = true;
    this.result.subscribe(x => this.maketurn(x));


  }
   maketurn(key)
  {
    this.awaitedText = this.correctText + this.newGame.TextForTyping[0];
    this.textToCheck = this.correctText + key;

    if (this.textToCheck === this.awaitedText) {
      this.correctText = this.awaitedText;
      this.newGame.TextForTyping = this.newGame.TextForTyping.slice(1, this.newGame.TextForTyping.length );
      if ( this.newGame.TextForTyping.length === 0)
      {
        this.isGameInProcess = false;
      }
    }
  }

  startTimer() {
    this.interval = setInterval(() => {
     this.timeStart += 0.5;
     this.speed = Math.round((this.correctText.length - this.typedTextForSec) / 5 / 0.00833);
     this.typedTextForSec = this.correctText.length;
     this.chart.data = [
      ['WPM', this.speed],
    ]
    }, 500);
  }

  pauseTimer() {
    clearInterval(this.interval);
  }


}
