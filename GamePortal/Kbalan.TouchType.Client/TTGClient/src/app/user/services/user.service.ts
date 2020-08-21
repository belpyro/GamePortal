import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TableUserDto } from '../models/TableUserDto';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getAllUsers(){
    return this.http.get<TableUserDto[]>(`${environment.backendurl}/api/users/`);
  }

  block(id){
    return this.http.get(`${environment.backendurl}/api/users/block/${id}` );
 }
  unblock(id){
    return this.http.get(`${environment.backendurl}/api/users/unblock/${id}` );
}
delete(id){
  return this.http.delete(`${environment.backendurl}/api/users/${id}`);
}
mkRoleAdmin(id){
  return this.http.get(`${environment.backendurl}/api/users/mkadmin/${id}`);
}
mkRoleUser(id){
  return this.http.get(`${environment.backendurl}/api/users/mkuser/${id}`);
}
}
