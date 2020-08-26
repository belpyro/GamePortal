import { TextSetDto } from './../../../text/models/textsetDto';
import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { HomeService } from 'src/app/home/services/home.service';
import { UserProfileDto } from 'src/app/home/models/UserProfileDto';
import { SgameService } from '../../services/sgame.service';
import { ToastrService } from 'ngx-toastr';
import {  map } from 'rxjs/operators';
import { fromEvent } from 'rxjs';
import { GoogleChartComponent } from 'angular-google-charts';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';
import { StatisticDto } from 'src/app/home/models/StatisticDto';

@Component({
  selector: 'app-sgame',
  templateUrl: './sgame.component.html',
  styleUrls: ['./sgame.component.scss']
})
export class SgameComponent implements OnInit {
  timeStart = 0.0;
  speed = 0;
  typedTextForSec = 0;
  interval;
  textToCheck = '';
  awaitedText = '';
  correctText = '';
  userProfile?: UserProfileDto;
  newGame?: TextSetDto;
  gameResult = 0;
  clicks = fromEvent(document, 'keydown');
  result = this.clicks.pipe( map((e: KeyboardEvent) => e.key));
  @ViewChild('googlechart')
  googlechart: GoogleChartComponent;
  chart = {
    title: 'Typing speed',
    type: 'Gauge',
    data: [
      ['WPM', this.speed],
    ],
    options: {
      width: 250,
      height: 250,
      greenFrom: 0,
      greenTo: 75,
      redFrom: 120,
      redTo: 150,
      yellowFrom: 75,
      yellowTo: 120,
      minorTicks: 1,
      max: 150
    }
  };
  constructor(private homeservice: HomeService, private gameservice: SgameService, private toastr: ToastrService, private router: Router) {

     }


  ngOnInit(): void {
    this.initUser();

  }

  initUser(){
     this.homeservice.getUser().subscribe(res => {
      this.userProfile = res; this.startnewgame(); }
     ); }

  async startnewgame(){
    if (this.userProfile.IsBlocked)
    {
      Swal.fire({
        icon: 'warning',
        title: 'You are blocked',
        text: 'Ask administrator for help!',
        showConfirmButton: false,
        timer: 3000
      }).then(() => {
        this.router.navigate(['home']);
        return;
    });
    }
    else{
    this.timeStart = 0.0;
    this.typedTextForSec = 0;
    this.textToCheck = '';
    this.awaitedText = '';
    this.correctText = '';
    this.newGame = await this.gameservice.getnewgame(this.userProfile.Id);
    let timerInterval;
    Swal.fire({
    title: 'GET READY!',
    html: 'Game will start in <b></b> seconds.',
    timer: 4000,
    timerProgressBar: true,
    onBeforeOpen: () => {
      Swal.showLoading();
      timerInterval = setInterval(() => {
        const content = Swal.getContent();
        if (content) {
          const b = content.querySelector('b');
          if (b) {
            b.textContent = Math.round(Swal.getTimerLeft() / 1000).toString();
          }
        }
      }, 1000);
    },
    onClose: () => {
      clearInterval(timerInterval);
    }
    }).then((result) => {
    if (result.dismiss === Swal.DismissReason.timer) {
      this.startTimer();
      this.result.subscribe(x => this.maketurn(x));
    }
    });

  }
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
      this.gameResult = Math.round((this.correctText.length) / 5 /  (this.timeStart / 60));
      this.gameFinish();
      }
    }
  }

  gameFinish()
  {
    this.pauseTimer();
    if  (this.gameResult > this.userProfile.Statistic.MaxSpeedRecord)
    {
      this.userProfile.Statistic.MaxSpeedRecord = this.gameResult;
    }
    this.userProfile.Statistic.AvarageSpeed =
    (this.userProfile.Statistic.AvarageSpeed * this.userProfile.Statistic.NumberOfGamesPlayed + this.gameResult)
    / (this.userProfile.Statistic.NumberOfGamesPlayed + 1 );
    this.userProfile.Statistic.NumberOfGamesPlayed++;
    const updateStatModel: StatisticDto  = {
      StatisticId : this.userProfile.Statistic.StatisticId ,
      AvarageSpeed : this.userProfile.Statistic.AvarageSpeed,
      NumberOfGamesPlayed : this.userProfile.Statistic.NumberOfGamesPlayed,
      MaxSpeedRecord : this.userProfile.Statistic.MaxSpeedRecord
      };
    this.gameservice.updateUserStatistic(updateStatModel).subscribe();
    Swal.fire({
      title: `Game is finished. You result is ${this.gameResult} wpm!`,
      text: 'Do you want to repeat?',
      icon: 'success',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, let\'s play again '
    }).then((playAgain) => {
    if (playAgain.isConfirmed) {
      this.changeText();
    }else{
      this.router.navigate(['/home']);
    }
  });
  }
  startTimer() {
    this.interval = setInterval(() => {
     this.timeStart += 0.5;
     this.speed = Math.round((this.correctText.length - this.typedTextForSec) / 5 / 0.00833);
     this.typedTextForSec = this.correctText.length;
     this.chart.data = [
      ['WPM', this.speed],
    ];
    }, 500);
  }

  pauseTimer() {
    clearInterval(this.interval);
  }

  changeText(){
    this.pauseTimer();
    this.startnewgame();
  }

}
