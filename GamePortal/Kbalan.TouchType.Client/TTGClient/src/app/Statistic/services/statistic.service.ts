import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { UserStatisticDto } from '../models/UserStatisticDto';
import { UserProfileDto } from 'src/app/home/models/UserProfileDto';

@Injectable({
  providedIn: 'root'
})
export class StatisticService {


  constructor(private http: HttpClient) { }
  getAllUsers(){
    return this.http.get<UserStatisticDto[]>(`${environment.backendurl}/api/statistic/`);
  }
  async getUser(id: string): Promise<UserProfileDto>{
    const response = this.http.get<UserProfileDto>(`${environment.backendurl}/api/users/${id}`).toPromise();
    return response;
  }
}
