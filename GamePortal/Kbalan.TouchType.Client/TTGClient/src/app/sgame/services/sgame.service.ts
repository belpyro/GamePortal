import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { SingleGameResultDto } from '../models/singlegameresult';
import { NewGameDto } from '../models/newgame';

@Injectable({
  providedIn: 'root'
})
export class SgameService {

  constructor(private http: HttpClient) { }
  async getnewgame(id): Promise<NewGameDto>
  {
    const response = this.http.get(`${environment.backendurl}/api/singlegame/${id}` ).toPromise();
    return response;
 }
 async maketurn(model): Promise<SingleGameResultDto>
  {
    const response = this.http.put(`${environment.backendurl}/api/singlegame`, model).toPromise();
    return response;
  }
}
