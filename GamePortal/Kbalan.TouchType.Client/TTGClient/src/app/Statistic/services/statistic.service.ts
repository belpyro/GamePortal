import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { UserStatisticDto } from '../models/UserStatisticDto';

@Injectable({
  providedIn: 'root'
})
export class StatisticService {


  constructor(private http: HttpClient) { }
  getAllUsers(){
    return this.http.get<UserStatisticDto[]>(`${environment.backendurl}/api/statistic/`);
  }
}
