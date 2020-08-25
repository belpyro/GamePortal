import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { UserProfileDto } from '../models/UserProfileDto';


@Injectable({
  providedIn: 'root'
})
export class HomeService {
  token;
  user;
  constructor(private http: HttpClient) {
  this.token = sessionStorage.getItem('id_token_claims_obj');
  this.user  = JSON.parse(this.token);
}

  getUser(){
    return this.http.get<UserProfileDto>(`${environment.backendurl}/api/users/${this.user.sub}`);
  }
  updateSetting(model){
    return this.http.put(`${environment.backendurl}/api/settings`, model);
  }
}
