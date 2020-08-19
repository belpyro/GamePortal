import { Component, OnInit } from '@angular/core';
import { HomeService } from 'src/app/home/services/home.service';
import { UserProfileDto } from 'src/app/home/models/UserProfileDto';
import { NewGameDto } from '../../models/newgame';
import { SgameService } from '../../services/sgame.service';
import { ToastrService } from 'ngx-toastr';
import { HostListener } from '@angular/core';
import { SingleGameResultDto } from '../../models/singlegameresult';
import { debounceTime, map } from 'rxjs/operators';
import { fromEvent } from 'rxjs';
@Component({
  selector: 'app-sgame',
  templateUrl: './sgame.component.html',
  styleUrls: ['./sgame.component.scss']
})
export class SgameComponent implements OnInit {

  key;
  textToSend = '';
  correctText = '';
  userProfile?: UserProfileDto;
  newGame?: NewGameDto;
  userTurn?: NewGameDto ;
  resultOfTurn?: SingleGameResultDto;
  isGameInProcess = false;
  clicks = fromEvent(document, 'keydown');
  result = this.clicks.pipe( map((e: KeyboardEvent) => e.key), debounceTime(150));

  constructor(private homeservice: HomeService, private gameservice: SgameService, private toastr: ToastrService) {
    this.result.subscribe(x => this.maketurn(x));
     }

  ngOnInit(): void {
    this.initUser();
  }
  initUser(){
     this.homeservice.getUser().subscribe(res => {
      this.userProfile = res; this.startnewgame(); }
     ); }

  async startnewgame(){
    this.newGame = await this.gameservice.getnewgame(this.userProfile.Id);
    this.isGameInProcess = true;
    this.userTurn = { Id: this.newGame.Id, TextForTyping: ''};


  }
  async maketurn(key)
  {
    this.textToSend = this.correctText + key;
    this.userTurn.TextForTyping = this.textToSend;
    this.toastr.success(this.textToSend);
    this.resultOfTurn = await this.gameservice.maketurn(this.userTurn);
    if (this.resultOfTurn.TurnResult === 1) {
      this.correctText = this.userTurn.TextForTyping;
      this.newGame.TextForTyping = this.newGame.TextForTyping.slice(1, this.newGame.TextForTyping.length );
    }
  }

}
