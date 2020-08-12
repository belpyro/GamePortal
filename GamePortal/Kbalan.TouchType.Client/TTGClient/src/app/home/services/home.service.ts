import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { UserProfileDto } from '../models/UserProfileDto';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(private http: HttpClient) { }

  getUser(id){
    return this.http.get<UserProfileDto>(`${environment.backendurl}/api/users/${id}`);
  }
}
