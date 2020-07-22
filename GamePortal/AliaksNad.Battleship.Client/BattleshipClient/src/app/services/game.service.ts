import { BattleAreaDto } from './../models/BattleAreaDto';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  serviceId: string;

  constructor(private http: HttpClient) {
    this.serviceId = Date.now().toString();
  }


    getBattleAreaById(id: number){
      return this.http.get<BattleAreaDto>(`Url${id}`);
    }
}
