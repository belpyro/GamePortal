import { NotificationService } from './services/notification.service';
import { BattleAreaDto } from './models/BattleAreaDto';
import { GameService } from './services/game.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'BattleshipClient';
  btlAreas: BattleAreaDto[] = [];

  /**
   *
   */
  constructor(private gms: GameService, private ntf: NotificationService) {}

  ngOnInit(): void {
    this.gms.getAll().subscribe((data) => {
      this.btlAreas = data;
      this.ntf.notify('Loaded');
    });
  }
}
