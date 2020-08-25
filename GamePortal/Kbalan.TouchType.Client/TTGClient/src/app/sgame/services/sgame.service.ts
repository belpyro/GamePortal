import { TextSetDto } from './../../text/models/textsetDto';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { UserProfileDto } from 'src/app/home/models/UserProfileDto';
import { StatisticDto } from 'src/app/home/models/StatisticDto';

@Injectable({
  providedIn: 'root'
})
export class SgameService {

  constructor(private http: HttpClient) { }
  async getnewgame(id): Promise<TextSetDto>
  {
    const response = this.http.get<TextSetDto>(`${environment.backendurl}/api/textsets/searchbylevelrand/${id}` ).toPromise();
    return response;
 }
  updateUserStatistic(model: StatisticDto)
 {
  return this.http.put(`${environment.backendurl}/api/statistic`, model);
 }
}
