import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { UserProfileDto } from '../models/UserProfileDto';
import { Session } from 'protractor';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(private http: HttpClient) { }

  getUser(){
    const token = sessionStorage.getItem('id_token_claims_obj');
    const user = JSON.parse(token);
    return this.http.get<UserProfileDto>(`${environment.backendurl}/api/users/${user.sub}`);
  }
  updateSetting(model){
    return this.http.put(`${environment.backendurl}/api/settings`, model);
  }
}
